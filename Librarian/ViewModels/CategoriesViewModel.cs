using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Services;
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

        private CollectionViewSource _categoriesViewSource;

        #region Properties

        #region CategoriesView
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

        public CategoriesViewModel() : this(new DebugCategoriesRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public CategoriesViewModel(IRepository<Category> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;

            _categoriesViewSource = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Category.Name), ListSortDirection.Ascending)
                }
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
