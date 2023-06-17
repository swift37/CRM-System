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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class OrderEditorViewModel : ViewModel
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<Customer> _customersRepository;
        private readonly IRepository<Shipper> _shippersRepository;

        private CollectionViewSource _productsViewSource;
        private CollectionViewSource _customersViewSource;
        private CollectionViewSource _shippersViewSource;

        #region Properties

        #region Tilte
        private string? _Title = "Transaction Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region CurrentOrder
        private Order? _CurrentOrder;

        /// <summary>
        /// Current Order
        /// </summary>
        public Order? CurrentOrder { get => _CurrentOrder; set => Set(ref _CurrentOrder, value); }
        #endregion

        #region OrderId
        /// <summary>
        /// Order id
        /// </summary>
        public int OrderId { get; }
        #endregion

        #region OrderDate
        private DateTime _OrderDate;

        /// <summary>
        /// Order date
        /// </summary>
        public DateTime OrderDate { get => _OrderDate; set => Set(ref _OrderDate, value); }
        #endregion

        #region RequiredDate
        private DateTime? _RequiredDate;

        /// <summary>
        /// Required date
        /// </summary>
        public DateTime? RequiredDate { get => _RequiredDate; set => Set(ref _RequiredDate, value); }
        #endregion

        #region ShippedDate
        private DateTime? _ShippedDate;

        /// <summary>
        /// Shipped date
        /// </summary>
        public DateTime? ShippedDate { get => _ShippedDate; set => Set(ref _ShippedDate, value); }
        #endregion

        #region OrderAmount
        private decimal _OrderAmount;

        /// <summary>
        /// Order amount
        /// </summary>
        public decimal OrderAmount { get => _OrderAmount; set => Set(ref _OrderAmount, value); }
        #endregion

        #region OrderEmployee
        private Employee? _OrderEmployee;

        /// <summary>
        /// Employee
        /// </summary>
        public Employee? OrderEmployee { get => _OrderEmployee; set => Set(ref _OrderEmployee, value); }
        #endregion

        #region OrderCustomer
        private Customer? _OrderCustomer;

        /// <summary>
        /// Customer
        /// </summary>
        public Customer? OrderCustomer { get => _OrderCustomer; set => Set(ref _OrderCustomer, value); }
        #endregion

        #region OrderShipVia
        private Shipper? _OrderShipVia;

        /// <summary>
        /// ShipVia
        /// </summary>
        public Shipper? OrderShipVia { get => _OrderShipVia; set => Set(ref _OrderShipVia, value); }
        #endregion

        #region OrderShippingCost
        private decimal _OrderShippingCost;

        /// <summary>
        /// Order shipping cost
        /// </summary>
        public decimal OrderShippingCost { get => _OrderShippingCost; set => Set(ref _OrderShippingCost, value); }
        #endregion

        #region OrderShipName
        private string? _OrderShipName;

        /// <summary>
        /// Receiver name
        /// </summary>
        public string? OrderShipName { get => _OrderShipName; set => Set(ref _OrderShipName, value); }
        #endregion

        #region OrderShipAddress
        private string? _OrderShipAddress;

        /// <summary>
        /// Receiver address
        /// </summary>
        public string? OrderShipAddress { get => _OrderShipAddress; set => Set(ref _OrderShipAddress, value); }
        #endregion



        #region OrderDetails
        private ObservableCollection<OrderDetails>? _OrderDetails;

        /// <summary>
        /// Order details collection 
        /// </summary>
        public ObservableCollection<OrderDetails>? OrderDetails { get => _OrderDetails; set => Set(ref _OrderDetails, value); }
        #endregion

        #region OrderDetailsProduct
        private Product? _OrderDetailsProduct;

        /// <summary>
        /// Product
        /// </summary>
        public Product? OrderDetailsProduct { get => _OrderDetailsProduct; set => Set(ref _OrderDetailsProduct, value); }
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



        #region Employees
        private IEnumerable<Employee>? _Employees;

        /// <summary>
        /// Employees collection
        /// </summary>
        public IEnumerable<Employee>? Employees { get => _Employees; set => Set(ref _Employees, value); }
        #endregion 



        #region CustomersView
        /// <summary>
        /// Customers collection view.
        /// </summary>
        public ICollectionView CustomersView => _customersViewSource.View;
        #endregion 

        #region Customers
        private IEnumerable<Customer>? _Customers;

        /// <summary>
        /// Customers collection
        /// </summary>
        public IEnumerable<Customer>? Customers 
        { 
            get => _Customers;
            set 
            {
                if (Set(ref _Customers, value))
                    _customersViewSource.Source = value;
                OnPropertyChanged(nameof(CustomersView));
            } 
        }
        #endregion 

        #region CustomersFilter
        private string? _CustomersFilter;

        /// <summary>
        /// Filter customers by name, number and mail
        /// </summary>
        public string? CustomersFilter
        {
            get => _CustomersFilter;
            set
            {
                if (Set(ref _CustomersFilter, value))
                    _customersViewSource.View.Refresh();
            }
        }
        #endregion



        #region ShippersView
        /// <summary>
        /// Shippers collection view.
        /// </summary>
        public ICollectionView ShippersView => _shippersViewSource.View;
        #endregion 

        #region Shippers
        private IEnumerable<Shipper>? _Shippers;

        /// <summary>
        /// Shippers collection
        /// </summary>
        public IEnumerable<Shipper>? Shippers
        {
            get => _Shippers;
            set
            {
                if (Set(ref _Shippers, value))
                    _shippersViewSource.Source = value;
                OnPropertyChanged(nameof(ShippersView));
            }
        }
        #endregion 

        #region ShippersFilter
        private string? _ShippersFilter;

        /// <summary>
        /// Filter shippers by name, number and mail
        /// </summary>
        public string? ShippersFilter
        {
            get => _ShippersFilter;
            set
            {
                if (Set(ref _ShippersFilter, value))
                    _shippersViewSource.View.Refresh();
            }
        }
        #endregion

        #endregion

        #region LoadRepositoriesCommand
        private ICommand? _LoadRepositoriesCommand;

        /// <summary>
        /// Load repositories command 
        /// </summary>
        public ICommand? LoadRepositoriesCommand => _LoadRepositoriesCommand ??= new LambdaCommandAsync(OnLoadRepositoriesCommandExecuted, CanLoadRepositoriesCommandExecute);

        private bool CanLoadRepositoriesCommandExecute() => true;

        private async Task OnLoadRepositoriesCommandExecuted()
        {
            if (_productsRepository.Entities is null) 
                throw new ArgumentNullException("Products list is empty or failed to load", nameof(_productsRepository.Entities));
            if (_employeesRepository.Entities is null)
                throw new ArgumentNullException("Employees list is empty or failed to load", nameof(_employeesRepository.Entities));
            if (_customersRepository.Entities is null)
                throw new ArgumentNullException("Customers list is empty or failed to load", nameof(_customersRepository.Entities));
            if (_shippersRepository.Entities is null)
                throw new ArgumentNullException("Shippers list is empty or failed to load", nameof(_shippersRepository.Entities));

            Products = await _productsRepository.Entities.ToArrayAsync();
            Employees = await _employeesRepository.Entities.ToArrayAsync();
            Customers = await _customersRepository.Entities.ToArrayAsync();
            Shippers = await _shippersRepository.Entities.ToArrayAsync();
        }
        #endregion

        #region AddOrderDetailsCommand
        private ICommand? _AddOrderDetailsCommand;

        /// <summary>
        /// Add order details command
        /// </summary>
        public ICommand? AddOrderDetailsCommand => _AddOrderDetailsCommand
            ??= new LambdaCommand(OnAddOrderDetailsCommandExecuted, CanAddOrderDetailsCommandExecute);

        private bool CanAddOrderDetailsCommandExecute() => true;

        private void OnAddOrderDetailsCommandExecuted()
        {
            var orderDetails = new OrderDetails();

            orderDetails.Order = CurrentOrder;
            orderDetails.Product = OrderDetailsProduct;
            orderDetails.UnitPrice = OrderDetailsUnitPrice;
            orderDetails.Quantity = OrderDetailsQuantity;
            orderDetails.Discount = OrderDetailsDiscount;

            OrderDetails?.Add(orderDetails);
        }
        #endregion

        public OrderEditorViewModel() : this(
            new Order { Id = 1, OrderDate = DateTime.Now },
            new HashSet<OrderDetails>(),
            new DebugProductsRepository(),
            new DebugEmployeesRepository(),
            new DebugCustomersRepository(),
            new DebugShippersRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public OrderEditorViewModel(
            Order order, 
            ICollection<OrderDetails>? orderDetails,
            IRepository<Product> products, 
            IRepository<Employee> employees, 
            IRepository<Customer> customers,
            IRepository<Shipper> shippers)
        {
            _productsRepository = products;
            _employeesRepository = employees;
            _customersRepository = customers;
            _shippersRepository = shippers;

            _productsViewSource = new CollectionViewSource();
            _customersViewSource = new CollectionViewSource();
            _shippersViewSource = new CollectionViewSource();

            _productsViewSource.Filter += OnProductsFilter;
            _customersViewSource.Filter += OnCustomersFilter;
            _shippersViewSource.Filter += OnShippersFilter;

            CurrentOrder = order;
            OrderId = order.Id;
            OrderDate = order.OrderDate;
            RequiredDate = order.RequiredDate;
            ShippedDate = order.ShippedDate;
            OrderAmount = order.Amount;
            OrderEmployee = order.Employee;
            OrderCustomer = order.Customer;
            OrderShipVia = order.ShipVia;
            OrderShippingCost = order.ShippingCost;
            OrderShipName = order.ShipName;
            OrderShipAddress = order.ShipAddress;
            OrderDetails = order.OrderDetails?.ToObservableCollection();
        }

        private void OnProductsFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Product product) || string.IsNullOrWhiteSpace(ProductsFilter)) return;

            if (!product.Name?.Contains(ProductsFilter) ?? true)
                e.Accepted = false;
        }

        private void OnCustomersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Customer customer) || string.IsNullOrWhiteSpace(CustomersFilter)) return;

            if (!customer.ToString().Contains(CustomersFilter) &&
                (!customer.Name?.Contains(CustomersFilter) ?? true) &&
                (!customer.Surname?.Contains(CustomersFilter) ?? true) &&
                (!customer.ContactNumber?.Contains(CustomersFilter) ?? true) &&
                (!customer.ContactMail?.Contains(CustomersFilter) ?? true))
                e.Accepted = false;
        }

        private void OnShippersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Shipper shipper) || string.IsNullOrWhiteSpace(ProductsFilter)) return;

            if (!shipper.Name?.Contains(ProductsFilter) ?? true)
                e.Accepted = false;
        }
    }
}
