using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class SupplyDetailsEditorViewModel : ViewModel
    {
        private readonly IRepository<Product> _productsRepository;

        private CollectionViewSource _productsViewSource;

        #region Properties

        #region SupplyDetailsProduct
        private Product? _SupplyDetailsProduct;

        /// <summary>
        /// Product
        /// </summary>
        public Product? SupplyDetailsProduct
        {
            get => _SupplyDetailsProduct;
            set
            {
                if (Set(ref _SupplyDetailsProduct, value) && value != null)
                    SupplyDetailsUnitPrice = value.UnitPrice;
            }
        }
        #endregion

        #region SupplyDetailsUnitPrice
        private decimal _SupplyDetailsUnitPrice;

        /// <summary>
        /// Unit price
        /// </summary>
        public decimal SupplyDetailsUnitPrice { get => _SupplyDetailsUnitPrice; set => Set(ref _SupplyDetailsUnitPrice, value); }
        #endregion

        #region SupplyDetailsQuantity
        private int _SupplyDetailsQuantity;

        /// <summary>
        /// Units quantity
        /// </summary>
        public int SupplyDetailsQuantity { get => _SupplyDetailsQuantity; set => Set(ref _SupplyDetailsQuantity, value); }
        #endregion


        #region ProductsView
        /// <summary>
        /// Products collection view.
        /// </summary>
        public ICollectionView ProductsView => _productsViewSource.View;
        #endregion 

        #region Products
        private IEnumerable<Product>? _Products;

        /// <summary>
        /// Products collection
        /// </summary>
        public IEnumerable<Product>? Products
        {
            get => _Products;
            set
            {
                if (Set(ref _Products, value))
                    _productsViewSource.Source = value;
                OnPropertyChanged(nameof(ProductsView));
            }
        }
        #endregion 

        #region ProductsFilter
        private string? _ProductsFilter;

        /// <summary>
        /// Filter products by name
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

        #endregion

        #region LoadProductsRepositoryCommand
        private ICommand? _LoadProductsRepositoryCommand;

        /// <summary>
        /// Load products repository command 
        /// </summary>
        public ICommand? LoadProductsRepositoryCommand => _LoadProductsRepositoryCommand ??= new LambdaCommandAsync(OnLoadProductsRepositoryCommandExecuted, CanLoadProductsRepositoryCommandExecute);

        private bool CanLoadProductsRepositoryCommandExecute() => true;

        private async Task OnLoadProductsRepositoryCommandExecuted()
        {
            if (_productsRepository.Entities is null)
                throw new ArgumentNullException("Products list is empty or failed to load", nameof(_productsRepository.Entities));

            Products = await _productsRepository.Entities.ToArrayAsync();
        }
        #endregion

        public SupplyDetailsEditorViewModel() : this(
            new SupplyDetails { Id = 1, Product = new Product { Name = "Test Product" } },
            new DebugProductsRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public SupplyDetailsEditorViewModel(
            SupplyDetails orderDetails,
            IRepository<Product> products)
        {
            _productsRepository = products;

            _productsViewSource = new CollectionViewSource();

            _productsViewSource.Filter += OnProductsFilter;

            SupplyDetailsProduct = orderDetails.Product;
            SupplyDetailsQuantity = orderDetails.Quantity;
            SupplyDetailsUnitPrice = orderDetails.UnitPrice;
        }

        private void OnProductsFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Product product) || string.IsNullOrWhiteSpace(ProductsFilter)) return;

            if (!product.Name?.Contains(ProductsFilter) ?? true)
                e.Accepted = false;
        }
    }
}
