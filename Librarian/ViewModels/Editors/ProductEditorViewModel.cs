using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class ProductEditorViewModel : ViewModel
    {
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Supplier> _suppliersRepository;

        private CollectionViewSource _categoriesViewSource;
        private CollectionViewSource _suppliersViewSource;

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

        #region SuppliersView
        /// <summary>
        /// Suppliers collection view.
        /// </summary>
        public ICollectionView SuppliersView => _suppliersViewSource.View;
        #endregion

        #region SuppliersNameFilter
        private string? _SuppliersNameFilter;

        /// <summary>
        /// Filter suppliers by name
        /// </summary>
        public string? SuppliersNameFilter
        {
            get => _SuppliersNameFilter;
            set
            {
                if (Set(ref _SuppliersNameFilter, value))
                    _suppliersViewSource.View.Refresh();
            }
        }
        #endregion

        #region Suppliers
        private ObservableCollection<Supplier>? _Suppliers;

        /// <summary>
        /// Suppliers collection
        /// </summary>
        public ObservableCollection<Supplier>? Suppliers
        {
            get => _Suppliers;
            set
            {
                if (Set(ref _Suppliers, value))
                    _suppliersViewSource.Source = value;
                OnPropertyChanged(nameof(SuppliersView));
            }
        }
        #endregion

        #region Tilte
        private string? _Title = "Product Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region ProductId
        /// <summary>
        /// Product id
        /// </summary>
        public int ProductId { get; }
        #endregion

        #region ProductName
        private string? _ProductName;

        /// <summary>
        /// Product name
        /// </summary>
        public string? ProductName { get => _ProductName; set => Set(ref _ProductName, value); }
        #endregion

        #region ProductCategory
        private Category? _ProductCategory;

        /// <summary>
        /// Product category
        /// </summary>
        public Category? ProductCategory { get => _ProductCategory; set => Set(ref _ProductCategory, value); }
        #endregion

        #region ProductSupplier
        private Supplier? _ProductSupplier;

        /// <summary>
        /// Product supplier
        /// </summary>
        public Supplier? ProductSupplier { get => _ProductSupplier; set => Set(ref _ProductSupplier, value); }
        #endregion

        #region ProductUnitPrice
        private decimal _ProductUnitPrice;

        /// <summary>
        /// Product unit price
        /// </summary>
        public decimal ProductUnitPrice { get => _ProductUnitPrice; set => Set(ref _ProductUnitPrice, value); }
        #endregion

        #region ProductUnitsInStock
        private int _ProductUnitsInStock;

        /// <summary>
        /// Product units in stock
        /// </summary>
        public int ProductUnitsInStock { get => _ProductUnitsInStock; set => Set(ref _ProductUnitsInStock, value); }
        #endregion

        #region ProductUnitsInEnterprise
        private int _ProductUnitsInEnterprise;

        /// <summary>
        /// Product units in enterprise
        /// </summary>
        public int ProductUnitsInEnterprise { get => _ProductUnitsInEnterprise; set => Set(ref _ProductUnitsInEnterprise, value); }
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
            if (_categoriesRepository.Entities is null) throw new ArgumentNullException("Category list is empty or failed to load");
            if (_suppliersRepository.Entities is null) throw new ArgumentNullException("Suppliers list is empty or failed to load");

            Categories = (await _categoriesRepository.Entities.ToArrayAsync()).ToObservableCollection();

            Suppliers = (await _suppliersRepository.Entities.ToArrayAsync()).ToObservableCollection();
        }
        #endregion

        public ProductEditorViewModel() : this (
            new Product { Id = 1, Name = "Test Product" }, 
            new DebugCategoriesRepository(),
            new DebugSuppliersRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public ProductEditorViewModel(
            Product product, 
            IRepository<Category> categoriesRepository,
            IRepository<Supplier> suppliersRepository)
        {
            _categoriesRepository = categoriesRepository;
            _suppliersRepository = suppliersRepository;

            _categoriesViewSource = new CollectionViewSource();
            _suppliersViewSource = new CollectionViewSource();

            ProductId = product.Id;
            ProductName = product.Name;
            ProductCategory = product.Category;
            ProductSupplier = product.Supplier;
            ProductUnitPrice = product.UnitPrice;
            ProductUnitsInStock = product.UnitsInStock;
            ProductUnitsInEnterprise = product.UnitsInEnterprise;

            _categoriesViewSource.Filter += OnCategoriesNameFilter;
            _suppliersViewSource.Filter += OnSuppliersNameFilter;
        }

        private void OnCategoriesNameFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Category category) || string.IsNullOrWhiteSpace(CategoriesNameFilter)) return;

            if (category.Name is null || !category.Name.Contains(CategoriesNameFilter))
                e.Accepted = false;
        }

        private void OnSuppliersNameFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Supplier supplier) || string.IsNullOrWhiteSpace(SuppliersNameFilter)) return;

            if (supplier.Name is null || !supplier.Name.Contains(SuppliersNameFilter))
                e.Accepted = false;
        }
    }
}
