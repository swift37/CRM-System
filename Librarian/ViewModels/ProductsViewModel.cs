using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Services;
using Librarian.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class ProductsViewModel : ViewModel
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Supplier> _suppliersRepository;
        private readonly IUserDialogService _dialogService;

        private CollectionViewSource _productsViewSource;
        private CollectionViewSource _archivedProductsViewSource;
        private CollectionViewSource _categoriesViewSource;

        #region Properties

        #region ProductsView
        public ICollectionView ProductsView => _productsViewSource.View;
        #endregion

        #region ProductsFilter
        private string? _ProductsFilter;

        /// <summary>
        /// Filter products by name and category
        /// </summary>
        public string? ProductsFilter
        {
            get => _ProductsFilter;
            set
            {
                if (Set(ref _ProductsFilter, value))
                    _productsViewSource.View.Refresh();
            }
        }
        #endregion

        #region Products
        private ObservableCollection<Product>? _Products;

        /// <summary>
        /// Products collection
        /// </summary>
        public ObservableCollection<Product>? Products 
        { 
            get => _Products;
            set 
            {
                if(Set(ref _Products, value))
                    _productsViewSource.Source = value;
                OnPropertyChanged(nameof(ProductsView));
            }
        }
        #endregion

        #region SelectedProduct
        private Product? _SelectedProduct;

        /// <summary>
        /// Selected product
        /// </summary>
        public Product? SelectedProduct { get => _SelectedProduct; set => Set(ref _SelectedProduct, value); }
        #endregion


        #region ArchivedProductsView
        public ICollectionView ArchivedProductsView => _archivedProductsViewSource.View;
        #endregion

        #region ArchivedProducts
        private ObservableCollection<Product>? _ArchivedProducts;

        /// <summary>
        /// Archived Products collection
        /// </summary>
        public ObservableCollection<Product>? ArchivedProducts
        {
            get => _ArchivedProducts;
            set
            {
                if (Set(ref _ArchivedProducts, value))
                    _archivedProductsViewSource.Source = value;
                OnPropertyChanged(nameof(ArchivedProductsView));
            }
        }
        #endregion

        #region ArchivedProductsFilter
        private string? _ArchivedProductsFilter;

        /// <summary>
        /// Filter archived products by name and category
        /// </summary>
        public string? ArchivedProductsFilter
        {
            get => _ArchivedProductsFilter;
            set
            {
                if (Set(ref _ArchivedProductsFilter, value))
                    _archivedProductsViewSource.View.Refresh();
            }
        }
        #endregion


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
            if (_productsRepository.Entities is null || _categoriesRepository.Entities is null) return;

            Products = (await _productsRepository.Entities.Where(p => p.IsActual).ToArrayAsync()).ToObservableCollection();

            ArchivedProducts = (await _productsRepository.Entities.Where(p => !p.IsActual).ToArrayAsync()).ToObservableCollection();

            Categories = (await _categoriesRepository.Entities.ToArrayAsync()).ToObservableCollection();
        }
        #endregion

        #region AddProductCommand
        private ICommand? _AddProductCommand;

        /// <summary>
        /// Add new product command 
        /// </summary>
        public ICommand? AddProductCommand => _AddProductCommand ??= new LambdaCommand<Category>(OnAddProductCommandExecuted, CanAddProductCommandnExecute);

        private bool CanAddProductCommandnExecute(Category? category = null) => true;

        private void OnAddProductCommandExecuted(Category? category = null)
        {
            var newProduct = new Product();
            newProduct.Category = category;

            if (!_dialogService.EditProduct(newProduct, _categoriesRepository, _suppliersRepository)) 
                return;

            var product = _productsRepository.Add(newProduct);
            if (product is null) throw new ArgumentNullException(nameof(product));
            _Products?.Add(product);

            SelectedProduct = product;
        }
        #endregion

        #region EditProductCommand
        private ICommand? _EditProductCommand;

        /// <summary>
        /// Edit product command 
        /// </summary>
        public ICommand? EditProductCommand => _EditProductCommand ??= new LambdaCommand<Product>(OnEditProductCommandExecuted, CanEditProductCommandnExecute);

        private bool CanEditProductCommandnExecute(Product? product) => product != null || SelectedProduct != null;

        private void OnEditProductCommandExecuted(Product? product)
        {
            var editableProduct = product ?? SelectedProduct;
            if (editableProduct is null) return;

            if (!_dialogService.EditProduct(editableProduct, _categoriesRepository, _suppliersRepository))
                return;

            _productsRepository.Update(editableProduct);
            _productsViewSource.View.Refresh();
        }
        #endregion

        #region ArchiveProductCommand
        private ICommand? _ArchiveProductCommand;

        /// <summary>
        /// Archive selected product command 
        /// </summary>
        public ICommand? ArchiveProductCommand => _ArchiveProductCommand
            ??= new LambdaCommand<Product>(OnArchiveProductCommandExecuted, CanArchiveProductCommandnExecute);

        private bool CanArchiveProductCommandnExecute(Product? product) => product != null || SelectedProduct != null;

        private void OnArchiveProductCommandExecuted(Product? product)
        {
            var archivableProduct = product ?? SelectedProduct;
            if (archivableProduct is null) return;

            if (!_dialogService.Confirmation(
                $"Are you sure you want to archive {archivableProduct.Name} product?",
                "Product archiving")) return;

            if (_productsRepository.Entities != null && _productsRepository.Entities.Any(p => p == product || p == SelectedProduct))
                _productsRepository.Archive(archivableProduct);

            Products?.Remove(archivableProduct);
            ArchivedProducts?.Add(archivableProduct);

            if (ReferenceEquals(SelectedProduct, archivableProduct))
                SelectedProduct = null;
        }
        #endregion

        #region UnArchiveProductCommand
        private ICommand? _UnArchiveProductCommand;

        /// <summary>
        /// Unarchive selected product command 
        /// </summary>
        public ICommand? UnArchiveProductCommand => _UnArchiveProductCommand
            ??= new LambdaCommand<Product>(OnUnArchiveProductCommandExecuted, CanUnArchiveProductCommandnExecute);

        private bool CanUnArchiveProductCommandnExecute(Product? product) => product != null || SelectedProduct != null;

        private void OnUnArchiveProductCommandExecuted(Product? product)
        {
            var archivableProduct = product ?? SelectedProduct;
            if (archivableProduct is null) return;

            if (!_dialogService.Confirmation(
                $"Are you sure you want to unarchive {archivableProduct.Name} product?",
                "Product unarchiving")) return;

            if (_productsRepository.Entities != null && _productsRepository.Entities.Any(p => p == product || p == SelectedProduct))
                _productsRepository.UnArchive(archivableProduct);

            ArchivedProducts?.Remove(archivableProduct);
            Products?.Add(archivableProduct);

            if (ReferenceEquals(SelectedProduct, archivableProduct))
                SelectedProduct = null;
        }
        #endregion

        #region RemoveProductCommand
        private ICommand? _RemoveProductCommand;

        /// <summary>
        /// Remove selected product command 
        /// </summary>
        public ICommand? RemoveProductCommand => _RemoveProductCommand 
            ??= new LambdaCommand<Product>(OnRemoveProductCommandExecuted, CanRemoveProductCommandnExecute);

        private bool CanRemoveProductCommandnExecute(Product? product) => product != null || SelectedProduct != null;

        private void OnRemoveProductCommandExecuted(Product? product)
        {
            var removableProduct = product ?? SelectedProduct;
            if (removableProduct is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the product {removableProduct.Name}?",
                "Product deleting")) return;

            if (_productsRepository.Entities != null && _productsRepository.Entities.Any(p => p == product || p == SelectedProduct))
            _productsRepository.Remove(removableProduct.Id);


            ArchivedProducts?.Remove(removableProduct);
            if (ReferenceEquals(SelectedProduct, removableProduct)) 
                SelectedProduct = null;
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

        #region ArchiveCategoryCommand
        private ICommand? _ArchiveCategoryCommand;

        /// <summary>
        /// Archive category command
        /// </summary>
        public ICommand? ArchiveCategoryCommand => _ArchiveCategoryCommand
            ??= new LambdaCommand<Category>(OnArchiveCategoryCommandExecuted, CanArchiveCategoryCommandExecute);

        private bool CanArchiveCategoryCommandExecute(Category? category) => category != null || SelectedCategory != null;

        private void OnArchiveCategoryCommandExecuted(Category? category)
        {
            var archivableCategory = category ?? SelectedCategory;
            if (archivableCategory is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the category \"{archivableCategory.Name}\"?",
                "Category deleting")) return;

            if (_categoriesRepository.Entities != null
                && _categoriesRepository.Entities.Any(c => c == archivableCategory))
                _categoriesRepository.Archive(archivableCategory);

            Categories?.Remove(archivableCategory);
            if (ReferenceEquals(SelectedCategory, archivableCategory))
                SelectedCategory = null;
        }
        #endregion

        #endregion

        public ProductsViewModel() : this(
            new DebugProductsRepository(), 
            new DebugCategoriesRepository(), 
            new DebugSuppliersRepository(), 
            new UserDialogService())
        {
            if(!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public ProductsViewModel(
            IRepository<Product> productsRepository, 
            IRepository<Category> categoriesRepository, 
            IRepository<Supplier> suppliersRepository,
            IUserDialogService dialogService)
        {
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
            _suppliersRepository = suppliersRepository;
            _dialogService = dialogService;

            _productsViewSource = new CollectionViewSource
            {
                GroupDescriptions =
                {
                    new PropertyGroupDescription(nameof(Product.Category))
                }
            };

            _archivedProductsViewSource = new CollectionViewSource
            {
                GroupDescriptions =
                {
                    new PropertyGroupDescription(nameof(Product.Category))
                }
            };

            _categoriesViewSource = new CollectionViewSource();

            _productsViewSource.Filter += OnProductsFilter;
            _archivedProductsViewSource.Filter += OnArchivedProductsFilter;
            _categoriesViewSource.Filter += OnCategoriesFilter;
        }

        private void OnProductsFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Product product) || string.IsNullOrWhiteSpace(ProductsFilter)) return;

            if ((!product.Name?.Contains(ProductsFilter) ?? true) && 
                (!product.Category?.Name?.Contains(ProductsFilter) ?? true) &&
                (!product.Supplier?.Name?.Contains(ProductsFilter) ?? true))
                e.Accepted = false;
        }

        private void OnArchivedProductsFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Product product) || string.IsNullOrWhiteSpace(ArchivedProductsFilter)) return;

            if ((!product.Name?.Contains(ArchivedProductsFilter) ?? true) &&
                (!product.Category?.Name?.Contains(ArchivedProductsFilter) ?? true) &&
                (!product.Supplier?.Name?.Contains(ArchivedProductsFilter) ?? true))
                e.Accepted = false;
        }

        private void OnCategoriesFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Category category) || string.IsNullOrWhiteSpace(CategoriesNameFilter)) return;

            if (!category.Name?.Contains(CategoriesNameFilter) ?? true)
                e.Accepted = false;
        }
    }
}
