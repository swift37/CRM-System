using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class CategoriesViewModel : ViewModel
    {
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IUserDialogService _dialogService;
        private CollectionViewSource _categoriesViewSource;

        #region Properties

        #region CategoriesView
        /// <summary>
        /// Categories collection view.
        /// </summary>
        public ICollectionView CategoriesView => _categoriesViewSource.View;
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
        /// Buyers collection
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

        #region LoadDataCommand
        private ICommand? _LoadDataCommand;

        /// <summary>
        /// Load data command
        /// </summary>
        public ICommand? LoadDataCommand => _LoadDataCommand ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute() => true;

        private async Task OnLoadDataCommandExecuted()
        {
            if (_categoriesRepository.Entities is null) return;

            Categories = (await _categoriesRepository.Entities.ToArrayAsync()).ToObservableCollection();
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

            if(!_dialogService.EditCategory(category)) return;

            _categoriesRepository.Add(category);
            Categories?.Add(category);

            SelectedCategory = category;
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

        public CategoriesViewModel() : this(new DebugCategoriesRepository(), new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public CategoriesViewModel(IRepository<Category> categoriesRepository, IUserDialogService dialogService)
        {
            _categoriesRepository = categoriesRepository;
            _dialogService = dialogService;

            _categoriesViewSource = new CollectionViewSource
            {
                //SortDescriptions =
                //{
                //    new SortDescription(nameof(Category.Name), ListSortDirection.Ascending)
                //}
            };

            _categoriesViewSource.Filter += OnCategoriesNameFilter;
        }

        private void OnCategoriesNameFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Category category) || string.IsNullOrWhiteSpace(CategoriesNameFilter)) return;

            if (category.Name is null || !category.Name.Contains(CategoriesNameFilter)) 
                e.Accepted = false;
        }
    }
}
