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
    public class OrderDetailsEditorViewModel : ViewModel
    {
        private readonly IRepository<Product> _productsRepository;

        private CollectionViewSource _productsViewSource;

        #region Properties

        #region OrderDetailsProduct
        private Product? _OrderDetailsProduct;

        /// <summary>
        /// Product
        /// </summary>
        public Product? OrderDetailsProduct 
        { 
            get => _OrderDetailsProduct; 
            set
            {
                if (Set(ref _OrderDetailsProduct, value) && value != null)
                    OrderDetailsUnitPrice = value.UnitPrice;
            }
        }
        #endregion

        #region OrderDetailsUnitPrice
        private decimal _OrderDetailsUnitPrice;

        /// <summary>
        /// Unit price
        /// </summary>
        public decimal OrderDetailsUnitPrice { get => _OrderDetailsUnitPrice; set => Set(ref _OrderDetailsUnitPrice, value); }
        #endregion

        #region OrderDetailsQuantity
        private int _OrderDetailsQuantity;

        /// <summary>
        /// Units quantity
        /// </summary>
        public int OrderDetailsQuantity { get => _OrderDetailsQuantity; set => Set(ref _OrderDetailsQuantity, value); }
        #endregion

        #region OrderDetailsDiscount
        private decimal _OrderDetailsDiscount;

        /// <summary>
        /// Discount
        /// </summary>
        public decimal OrderDetailsDiscount { get => _OrderDetailsDiscount; set => Set(ref _OrderDetailsDiscount, value); }
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

        public OrderDetailsEditorViewModel() : this(
            new OrderDetails { Id = 1, Product = new Product { Name = "Test Product" } },
            new DebugProductsRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public OrderDetailsEditorViewModel(
            OrderDetails orderDetails,
            IRepository<Product> products)
        {
            _productsRepository = products;

            _productsViewSource = new CollectionViewSource();

            _productsViewSource.Filter += OnProductsFilter;

            OrderDetailsProduct = orderDetails.Product;
            OrderDetailsQuantity = orderDetails.Quantity;
            OrderDetailsUnitPrice = orderDetails.UnitPrice;
            OrderDetailsDiscount = orderDetails.Discount;
        }

        private void OnProductsFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Product product) || string.IsNullOrWhiteSpace(ProductsFilter)) return;

            if (!product.Name?.Contains(ProductsFilter) ?? true)
                e.Accepted = false;
        }
    }
}
