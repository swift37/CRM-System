using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class BookEditorViewModel : ViewModel
    {
        private readonly IRepository<Category> _categoriesRepository;

        #region Properties

        #region Tilte
        private string? _Title = "Book Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region BookId
        /// <summary>
        /// Book id
        /// </summary>
        public int BookId { get; }
        #endregion

        #region BookTitle
        private string? _BookTitle;

        /// <summary>
        /// Book title
        /// </summary>
        public string? BookTitle { get => _BookTitle; set => Set(ref _BookTitle, value); }
        #endregion

        #region BookCategory
        private Category? _BookCategory;

        /// <summary>
        /// Book category
        /// </summary>
        public Category? BookCategory { get => _BookCategory; set => Set(ref _BookCategory, value); }
        #endregion

        #region BookPrice
        private decimal _BookPrice;

        /// <summary>
        /// Book price
        /// </summary>
        public decimal BookPrice { get => _BookPrice; set => Set(ref _BookPrice, value); }
        #endregion

        #region Categories

        private IEnumerable<Category>? _Categories;

        /// <summary>
        /// Categories collection
        /// </summary>
        public IEnumerable<Category>? Categories { get => _Categories; set => Set(ref _Categories, value); }
        #endregion 

        #endregion

        #region LoadCategoriesCommand
        private ICommand? _LoadCategoriesCommand;

        /// <summary>
        /// Load data command 
        /// </summary>
        public ICommand? LoadCategoriesCommand => _LoadCategoriesCommand ??= new LambdaCommandAsync(OnLoadCategoriesCommandExecuted, CanLoadCategoriesCommandExecute);

        private bool CanLoadCategoriesCommandExecute() => true;

        private async Task OnLoadCategoriesCommandExecuted()
        {
            if (_categoriesRepository.Entities is null) throw new ArgumentNullException("Category list is empty or failed to load");

            Categories = await _categoriesRepository.Entities.ToArrayAsync();
        }
        #endregion

        public BookEditorViewModel() : this (new Book { Id = 1, Name = "Sherlock Holmes" }, new DebugCategoriesRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadCategoriesCommandExecuted();
        }

        public BookEditorViewModel(Book book, IRepository<Category> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;

            BookId = book.Id;
            BookTitle = book.Name;
            BookCategory = book.Category;
            BookPrice = book.Price;
        }
    }
}
