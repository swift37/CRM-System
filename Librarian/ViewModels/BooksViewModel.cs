using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services;
using Librarian.Services.Interfaces;
using Librarian.Views;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IUserDialogService _dialogService;

        private CollectionViewSource _booksViewSource;
        private CollectionViewSource _categoriesViewSource;

        #region Properties

        #region BooksView
        public ICollectionView BooksView => _booksViewSource.View;
        #endregion

        #region BooksCount
        private int _BooksCount;

        /// <summary>
        /// Books count
        /// </summary>
        public int BooksCount { get => _BooksCount; set => Set(ref _BooksCount, value); }
        #endregion

        #region BooksFilter
        private string? _BooksFilter;

        /// <summary>
        /// Filter books by name and category
        /// </summary>
        public string? BooksFilter
        {
            get => _BooksFilter;
            set
            {
                if (Set(ref _BooksFilter, value))
                    _booksViewSource.View.Refresh();
            }
        }
        #endregion

        #region Books
        private ObservableCollection<Book>? _Books;

        /// <summary>
        /// Books collection
        /// </summary>
        public ObservableCollection<Book>? Books 
        { 
            get => _Books;
            set 
            {
                if(Set(ref _Books, value))
                    _booksViewSource.Source = value;
                OnPropertyChanged(nameof(BooksView));
            }
        }
        #endregion

        #region SelectedBook
        private Book? _SelectedBook;

        /// <summary>
        /// Selected book
        /// </summary>
        public Book? SelectedBook { get => _SelectedBook; set => Set(ref _SelectedBook, value); }
        #endregion



        #region CategoriesView
        /// <summary>
        /// Categories collection view.
        /// </summary>
        public ICollectionView CategoriesView => _categoriesViewSource.View;
        #endregion

        #region CategoriesCount
        private int _CategoriesCount;

        /// <summary>
        /// Categories count
        /// </summary>
        public int CategoriesCount { get => _CategoriesCount; set => Set(ref _CategoriesCount, value); }
        #endregion

        #region CategoriesNameFilter
        private string? _CategoriesNameFilter;

        /// <summary>
        /// Filter categories by name
        /// </summary>
        public string? CategoriesNameFilter
        {
            get => _CategoriesNameFilter;
            set
            {
                if (Set(ref _CategoriesNameFilter, value))
                    _categoriesViewSource.View.Refresh();
            }
        }
        #endregion

        #region Categories
        private ObservableCollection<Category>? _Categories;

        /// <summary>
        /// Categories collection
        /// </summary>
        public ObservableCollection<Category>? Categories
        {
            get => _Categories;
            set
            {
                if (Set(ref _Categories, value))
                    _categoriesViewSource.Source = value;
                OnPropertyChanged(nameof(CategoriesView));
            }
        }
        #endregion

        #region SelectedCategory
        private Category? _SelectedCategory;

        /// <summary>
        /// Selected category
        /// </summary>
        public Category? SelectedCategory { get => _SelectedCategory; set => Set(ref _SelectedCategory, value); }
        #endregion

        #endregion

        #region Commands

        #region LoadDataCommand
        private ICommand? _LoadDataCommand;

        /// <summary>
        /// Load data command 
        /// </summary>
        public ICommand? LoadDataCommand => _LoadDataCommand ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute() => true;

        private async Task OnLoadDataCommandExecuted()
        {
            if (_booksRepository.Entities is null || _categoriesRepository.Entities is null) return;

            Books = (await _booksRepository.Entities.ToArrayAsync()).ToObservableCollection();

            BooksCount = await _booksRepository.Entities.CountAsync();

            Categories = (await _categoriesRepository.Entities.ToArrayAsync()).ToObservableCollection();

            CategoriesCount = await _categoriesRepository.Entities.CountAsync();
        }
        #endregion

        #region AddBookCommand
        private ICommand? _AddBookCommand;

        /// <summary>
        /// Add new book command 
        /// </summary>
        public ICommand? AddBookCommand => _AddBookCommand ??= new LambdaCommand(OnAddBookCommandExecuted, CanAddBookCommandnExecute);

        private bool CanAddBookCommandnExecute() => true;

        private void OnAddBookCommandExecuted()
        {
            var newBook = new Book();

            if (!_dialogService.EditBook(newBook, _categoriesRepository)) 
                return;

            var book = _booksRepository.Add(newBook);
            if (book is null) throw new ArgumentNullException(nameof(book));
            _Books?.Add(book);

            SelectedBook = book;
        }
        #endregion

        #region EditBookCommand
        private ICommand? _EditBookCommand;

        /// <summary>
        /// Edit book command 
        /// </summary>
        public ICommand? EditBookCommand => _EditBookCommand ??= new LambdaCommand<Book>(OnEditBookCommandExecuted, CanEditBookCommandnExecute);

        private bool CanEditBookCommandnExecute(Book? book) => book != null || SelectedBook != null;

        private void OnEditBookCommandExecuted(Book? book)
        {
            var editableBook = book ?? SelectedBook;
            if (editableBook is null) return;

            if (!_dialogService.EditBook(editableBook, _categoriesRepository))
                return;

            _booksRepository.Update(editableBook);
            _booksViewSource.View.Refresh();
        }
        #endregion

        #region RemoveBookCommand
        private ICommand? _RemoveBookCommand;

        /// <summary>
        /// Remove selected book command 
        /// </summary>
        public ICommand? RemoveBookCommand => _RemoveBookCommand 
            ??= new LambdaCommand<Book>(OnRemoveBookCommandExecuted, CanRemoveBookCommandnExecute);

        private bool CanRemoveBookCommandnExecute(Book? book) => book != null || SelectedBook != null;

        private void OnRemoveBookCommandExecuted(Book? book)
        {
            var removableBook = book ?? SelectedBook;
            if (removableBook is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the book {removableBook.Name}?",
                "Book deleting")) return;

            if (_booksRepository.Entities != null && _booksRepository.Entities.Any(b => b == book || b == SelectedBook))
            _booksRepository.Remove(removableBook.Id);


            Books?.Remove(removableBook);
            if (ReferenceEquals(SelectedBook, removableBook)) 
                SelectedBook = null;
        }
        #endregion

        #region AddCategoryCommand
        private ICommand? _AddCategoryCommand;

        /// <summary>
        /// AddCategory command
        /// </summary>
        public ICommand? AddCategoryCommand => _AddCategoryCommand ??= new LambdaCommand(OnAddCategoryCommandExecuted, CanAddCategoryCommandExecute);

        private bool CanAddCategoryCommandExecute() => true;

        private void OnAddCategoryCommandExecuted()
        {
            var category = new Category();

            if (!_dialogService.EditCategory(category)) return;

            _categoriesRepository.Add(category);
            Categories?.Add(category);

            SelectedCategory = category;
        }
        #endregion

        #region EditCategoryCommand
        private ICommand? _EditCategoryCommand;

        /// <summary>
        /// Edit category command 
        /// </summary>
        public ICommand? EditCategoryCommand => _EditCategoryCommand ??= new LambdaCommand<Category>(OnEditCategoryCommandExecuted, CanEditCategoryCommandnExecute);

        private bool CanEditCategoryCommandnExecute(Category? category) => category != null || SelectedCategory != null;

        private void OnEditCategoryCommandExecuted(Category? category)
        {
            var editableCategory = category ?? SelectedCategory;
            if (editableCategory is null) return;

            if (!_dialogService.EditCategory(editableCategory))
                return;

            _categoriesRepository.Update(editableCategory);
            _categoriesViewSource.View.Refresh();
        }
        #endregion

        #region RemoveCategoryCommand
        private ICommand? _RemoveCategoryCommand;

        /// <summary>
        /// RemoveCategory command
        /// </summary>
        public ICommand? RemoveCategoryCommand => _RemoveCategoryCommand
            ??= new LambdaCommand<Category>(OnRemoveCategoryCommandExecuted, CanRemoveCategoryCommandExecute);

        private bool CanRemoveCategoryCommandExecute(Category? category) => category != null || SelectedCategory != null;

        private void OnRemoveCategoryCommandExecuted(Category? category)
        {
            var removableCategory = category ?? SelectedCategory;
            if (removableCategory is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the category \"{removableCategory.Name}\"?",
                "Category deleting")) return;

            if (_categoriesRepository.Entities != null
                && _categoriesRepository.Entities.Any(c => c == category || c == SelectedCategory))
                _categoriesRepository.Remove(removableCategory.Id);


            Categories?.Remove(removableCategory);
            if (ReferenceEquals(SelectedCategory, removableCategory))
                SelectedCategory = null;
        }
        #endregion

        #endregion

        public BooksViewModel() : this(new DebugBooksRepository(), new DebugCategoriesRepository(), new UserDialogService())
        {
            if(!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public BooksViewModel(IRepository<Book> booksRepository, IRepository<Category> categoriesRepository, IUserDialogService dialogService)
        {
            _booksRepository = booksRepository;
            _categoriesRepository = categoriesRepository;
            _dialogService = dialogService;

            //todo: Перенести всё связанное с сортировкой и фильтрами в разметку окна
            _booksViewSource = new CollectionViewSource
            {
                GroupDescriptions =
                {
                    new PropertyGroupDescription(nameof(Book.Category))
                }

                //SortDescriptions =
                //{
                //    new SortDescription(nameof(Book.Name), ListSortDirection.Ascending)
                //}
            };

            _categoriesViewSource = new CollectionViewSource();

            _booksViewSource.Filter += OnBooksNameFilter;
            _categoriesViewSource.Filter += OnCategoriesNameFilter;
        }

        private void OnBooksNameFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Book book) || string.IsNullOrWhiteSpace(BooksFilter)) return;

            if ((book.Name is null || !book.Name.Contains(BooksFilter)) && 
                (book.Category is null || book.Category.Name is null || !book.Category.Name.Contains(BooksFilter)))
                e.Accepted = false;
        }

        private void OnCategoriesNameFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Category category) || string.IsNullOrWhiteSpace(CategoriesNameFilter)) return;

            if (category.Name is null || !category.Name.Contains(CategoriesNameFilter))
                e.Accepted = false;
        }
    }
}
