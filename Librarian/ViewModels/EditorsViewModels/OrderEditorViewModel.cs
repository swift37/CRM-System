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
using System.Windows.Controls;
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
        private readonly IUserDialogService _dialogService;

        private CollectionViewSource _customersViewSource;
        private CollectionViewSource _shippersViewSource;

        #region Properties

        #region CurrentOrder
        private Order? _CurrentOrder;

        /// <summary>
        /// Current Order
        /// </summary>
        public Order? CurrentOrder { get => _CurrentOrder; set => Set(ref _CurrentOrder, value); }
        #endregion

        #region SelectedOrderDetails
        private OrderDetails? _SelectedOrderDetails;

        /// <summary>
        /// Selected order details
        /// </summary>
        public OrderDetails? SelectedOrderDetails { get => _SelectedOrderDetails; set => Set(ref _SelectedOrderDetails, value); }
        #endregion

        #region OrderId
        /// <summary>
        /// Order id
        /// </summary>
        public int OrderId { get; set; }
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

        #region OrderProductsQuantity
        private int _OrderProductsQuantity;

        /// <summary>
        /// Order amount
        /// </summary>
        public int OrderProductsQuantity { get => _OrderProductsQuantity; set => Set(ref _OrderProductsQuantity, value); }
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


        #region IsExistingOrder
        /// <summary>
        /// Is existing order?
        /// </summary>
        public bool IsExistingOrder { get; set; }
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
            if (_employeesRepository.Entities is null)
                throw new ArgumentNullException("Employees list is empty or failed to load", nameof(_employeesRepository.Entities));
            if (_customersRepository.Entities is null)
                throw new ArgumentNullException("Customers list is empty or failed to load", nameof(_customersRepository.Entities));
            if (_shippersRepository.Entities is null)
                throw new ArgumentNullException("Shippers list is empty or failed to load", nameof(_shippersRepository.Entities));
            if (_productsRepository.Entities is null)
                throw new ArgumentNullException("Products list is empty or failed to load", nameof(_productsRepository.Entities));

            Employees = await _employeesRepository.Entities.ToArrayAsync();
            Customers = await _customersRepository.Entities.ToArrayAsync();
            Shippers = await _shippersRepository.Entities.ToArrayAsync();
            //_ = await _productsRepository.Entities.ToArrayAsync();
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

            if (!_dialogService.EditOrderDetails(orderDetails)) return;

            OrderDetails?.Add(orderDetails);

            OrderAmount += (orderDetails.Quantity * orderDetails.UnitPrice) - orderDetails.Discount;
            OrderProductsQuantity += orderDetails.Quantity;
        }
        #endregion

        #region RemoveOrderDetailsCommand
        private ICommand? _RemoveOrderDetailsCommand;

        /// <summary>
        /// Remove order details command
        /// </summary>
        public ICommand? RemoveOrderDetailsCommand => _RemoveOrderDetailsCommand
            ??= new LambdaCommand<OrderDetails>(OnRemoveOrderDetailsCommandExecuted, CanRemoveOrderDetailsCommandExecute);

        private bool CanRemoveOrderDetailsCommandExecute(OrderDetails? orderDetails) => true;

        private void OnRemoveOrderDetailsCommandExecuted(OrderDetails? orderDetails)
        {
            var removableOrderDetails = orderDetails ?? SelectedOrderDetails;
            if (removableOrderDetails is null) return;

            if (OrderDetails != null && OrderDetails.Any(d => d == removableOrderDetails))
                OrderDetails.Remove(removableOrderDetails);

            OrderAmount -= (removableOrderDetails.Quantity * removableOrderDetails.UnitPrice) - removableOrderDetails.Discount;
            OrderProductsQuantity -= removableOrderDetails.Quantity;

            if (ReferenceEquals(SelectedOrderDetails, removableOrderDetails))
                SelectedOrderDetails = null;
        }
        #endregion

        public OrderEditorViewModel() : this(
            new DebugProductsRepository(),
            new DebugEmployeesRepository(),
            new DebugCustomersRepository(),
            new DebugShippersRepository(),
            new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            InitProps(new Order { Id = 1, OrderDate = DateTime.Now });
        }

        public OrderEditorViewModel(
            IRepository<Product> products, 
            IRepository<Employee> employees, 
            IRepository<Customer> customers,
            IRepository<Shipper> shippers,
            IUserDialogService userDialogService)
        {
            _productsRepository = products;
            _employeesRepository = employees;
            _customersRepository = customers;
            _shippersRepository = shippers;
            _dialogService = userDialogService;

            _customersViewSource = new CollectionViewSource();
            _shippersViewSource = new CollectionViewSource();

            _customersViewSource.Filter += OnCustomersFilter;
            _shippersViewSource.Filter += OnShippersFilter;
        }

        public void InitProps(Order order)
        {
            CurrentOrder = order;
            OrderId = order.Id;
            OrderDate = order.OrderDate;
            RequiredDate = order.RequiredDate;
            ShippedDate = order.ShippedDate;
            OrderAmount = order.Amount;
            OrderProductsQuantity = order.ProductsQuantity;
            OrderEmployee = order.Employee;
            OrderCustomer = order.Customer;
            OrderShipVia = order.ShipVia;
            OrderShippingCost = order.ShippingCost;
            OrderShipName = order.ShipName;
            OrderShipAddress = order.ShipAddress;
            OrderDetails = order.OrderDetails?.ToObservableCollection();
            IsExistingOrder = true;
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
            if (!(e.Item is Shipper shipper) || string.IsNullOrWhiteSpace(ShippersFilter)) return;

            if (!shipper.Name?.Contains(ShippersFilter) ?? true)
                e.Accepted = false;
        }
    }
}
