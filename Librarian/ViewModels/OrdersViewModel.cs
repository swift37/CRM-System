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
    public class OrdersViewModel : ViewModel
    {
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderDetails> _ordersDetailsRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<Customer> _customersRepository;
        private readonly IUserDialogService _dialogService;

        private CollectionViewSource _ordersViewSource;

        #region Properties

        #region OrdersView
        /// <summary>
        /// Orders collection view.
        /// </summary>
        public ICollectionView OrdersView => _ordersViewSource.View;
        #endregion

        #region OrdersCount
        private int _OrdersCount;

        /// <summary>
        /// Orders count
        /// </summary>
        public int OrdersCount { get => _OrdersCount; set => Set(ref _OrdersCount, value); }
        #endregion

        #region OrdersFilter
        private string? _OrdersFilter;

        /// <summary>
        /// Orders filter.
        /// </summary>
        public string? OrdersFilter
        {
            get => _OrdersFilter;
            set
            {
                if (Set(ref _OrdersFilter, value))
                    _ordersViewSource.View.Refresh();
            }
        }
        #endregion

        #region Orders
        private ObservableCollection<Order>? _Orders;

        /// <summary>
        /// Orders collection.
        /// </summary>
        public ObservableCollection<Order>? Orders
        {
            get => _Orders;
            set
            {
                if (Set(ref _Orders, value))
                    _ordersViewSource.Source = value;
                OnPropertyChanged(nameof(OrdersView));
            }
        }
        #endregion

        #region SelectedOrder
        private Order? _SelectedOrder;

        /// <summary>
        /// Selected order.
        /// </summary>
        public Order? SelectedOrder { get => _SelectedOrder; set => Set(ref _SelectedOrder, value); }
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
            if (_ordersRepository.Entities is null) return;

            Orders = (await _ordersRepository.Entities.ToArrayAsync()).ToObservableCollection();

            OrdersCount = await _ordersRepository.Entities.CountAsync();
        }
        #endregion

        #region AddOrderCommand
        private ICommand? _AddOrderCommand;

        /// <summary>
        /// Add order command
        /// </summary>
        public ICommand? AddOrderCommand => _AddOrderCommand
            ??= new LambdaCommand(OnAddOrderCommandExecuted, CanAddOrderCommandExecute);

        private bool CanAddOrderCommandExecute() => true;

        private void OnAddOrderCommandExecuted()
        {
            var order = new Order();

            if (!_dialogService.EditOrder(order, _productsRepository, _employeesRepository, _customersRepository)) return;

            _ordersRepository.Add(order);
            Orders?.Add(order);

            SelectedOrder = order;
        }
        #endregion

        #region EditOrderCommand
        private ICommand? _EditOrderCommand;

        /// <summary>
        /// Edit order command 
        /// </summary>
        public ICommand? EditOrderCommand => _EditOrderCommand ??= new LambdaCommand<Order>(OnEditOrderCommandExecuted, CanEditOrderCommandnExecute);

        private bool CanEditOrderCommandnExecute(Order? order) => order != null || SelectedOrder != null;

        private void OnEditOrderCommandExecuted(Order? order)
        {
            var editableOrder = order ?? SelectedOrder;
            if (editableOrder is null) return;

            if (!_dialogService.EditOrder(editableOrder, _productsRepository, _employeesRepository, _customersRepository))
                return;

            _ordersRepository.Update(editableOrder);
            _ordersViewSource.View.Refresh();
        }
        #endregion

        #region RemoveOrderCommand
        private ICommand? _RemoveOrderCommand;

        /// <summary>
        /// Remove order command
        /// </summary>
        public ICommand? RemoveOrderCommand => _RemoveOrderCommand
            ??= new LambdaCommand<Order>(OnRemoveOrderCommandExecuted, CanRemoveOrderCommandExecute);

        private bool CanRemoveOrderCommandExecute(Order? order) => order != null || SelectedOrder != null;

        private void OnRemoveOrderCommandExecuted(Order? order)
        {
            var removableOrder = order ?? SelectedOrder;
            if (removableOrder is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the order \"{removableOrder.Id}\"?",
                "Order deleting")) return;

            if (_ordersRepository.Entities != null
                && _ordersRepository.Entities.Any(c => c == order || c == SelectedOrder))
                _ordersRepository.Remove(removableOrder.Id);


            Orders?.Remove(removableOrder);
            if (ReferenceEquals(SelectedOrder, removableOrder))
                SelectedOrder = null;
        }
        #endregion
        
        public OrdersViewModel() : this(
            new DebugOrdersRepository(),
            new DebugOrdersDetailsRepository(),
            new DebugProductsRepository(),
            new DebugEmployeesRepository(),
            new DebugCustomersRepository(),
            new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public OrdersViewModel(
            IRepository<Order> ordersRepository,
            IRepository<OrderDetails> ordersDetailsRepository,
            IRepository<Product> products,
            IRepository<Employee> employees,
            IRepository<Customer> customers,
            IUserDialogService dialogService)
        {
            _ordersRepository = ordersRepository;
            _ordersDetailsRepository = ordersDetailsRepository;
            _productsRepository = products;
            _employeesRepository = employees;
            _customersRepository = customers;
            _dialogService = dialogService;

            _ordersViewSource = new CollectionViewSource();

            _ordersViewSource.Filter += OnOrdersFilter;
        }

        private void OnOrdersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Order order) || string.IsNullOrWhiteSpace(OrdersFilter)) return;

            var orderDate = order.OrderDate.ToString();

            if ((!orderDate.Contains(OrdersFilter)) &&
                (!order.OrderDetails?.Any(d => d.Product?.Name?.Contains(OrdersFilter) ?? false) ?? true) &&
                !order.Amount.ToString().Contains(OrdersFilter) &&
                (!order.Customer?.ContactNumber?.Contains(OrdersFilter) ?? true) &&
                (!order.Employee?.Name?.Contains(OrdersFilter) ?? true) && 
                (!order.Employee?.Surname?.Contains(OrdersFilter) ?? true) && 
                (!order.ShipVia?.Name?.Contains(OrdersFilter) ?? true))
                    e.Accepted = false;
        }
    }
}
