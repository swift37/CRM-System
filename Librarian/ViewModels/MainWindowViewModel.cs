using FluentValidation;
using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services;
using Librarian.Services.Interfaces;
using Librarian.Services.Validators;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialogService _dialogService;
        private readonly IStatisticsCollectionService _statisticsService;
        private readonly IValidator<RegisterRequest> _registerValidator;
        private readonly IAuthorizationService _authorizationService;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<Customer> _customersRepository;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderDetails> _ordersDetailsRepository;
        private readonly IRepository<Supply> _suppliesRepository;
        private readonly IRepository<SupplyDetails> _suppliesDetailsRepository;
        private readonly IRepository<WorkingRate> _workingRatesRepository;
        private readonly IRepository<Supplier> _suppliersRepository;
        private readonly IRepository<Shipper> _shippersRepository;

        #region Properties

        #region Tilte
        private string? _Title = "Librarian";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region CurrentViewModel
        private ViewModel? _CurrentViewModel;

        /// <summary>
        /// Current ViewModel
        /// </summary>
        public ViewModel? CurrentViewModel { get => _CurrentViewModel; private set => Set(ref _CurrentViewModel, value); }
        #endregion

        #region CurrentEmployee
        private Employee? _CurrentEmployee;

        /// <summary>
        /// Current Employee
        /// </summary>
        public Employee? CurrentEmployee { get => _CurrentEmployee; set => Set(ref _CurrentEmployee, value); }
        #endregion

        #endregion

        #region Commands

        #region ShowDashboardViewCommand
        private ICommand? _ShowDashboardViewCommand;

        /// <summary>
        /// Show Dashboard View
        /// </summary>
        public ICommand? ShowDashboardViewCommand => _ShowDashboardViewCommand ??= new LambdaCommand(OnShowDashboardViewCommandExecuted, CanShowDashboardViewCommandnExecute);

        private bool CanShowDashboardViewCommandnExecute() => true;

        private void OnShowDashboardViewCommandExecuted()
        {
            CurrentViewModel = new DashboardViewModel(_ordersRepository, _ordersDetailsRepository, _statisticsService);
        }
        #endregion

        #region ShowProductsViewCommand
        private ICommand? _ShowProductsViewCommand;

        /// <summary>
        /// Show ProductsView
        /// </summary>
        public ICommand? ShowProductsViewCommand => _ShowProductsViewCommand ??= new LambdaCommand(OnShowProductsViewCommandExecuted, CanShowProductsViewCommandnExecute);

        private bool CanShowProductsViewCommandnExecute() => true;

        private void OnShowProductsViewCommandExecuted()
        {
            CurrentViewModel = new ProductsViewModel(_productsRepository, _categoriesRepository, _suppliersRepository, _dialogService);
        }
        #endregion

        #region ShowEmployeesViewCommand
        private ICommand? _ShowEmployeesViewCommand;

        /// <summary>
        /// Show EmployeesView
        /// </summary>
        public ICommand? ShowEmployeesViewCommand => _ShowEmployeesViewCommand ??= new LambdaCommand(OnShowEmployeesViewCommandExecuted, CanShowEmployeesViewCommandnExecute);

        private bool CanShowEmployeesViewCommandnExecute() => true;

        private void OnShowEmployeesViewCommandExecuted()
        {
            CurrentViewModel = new EmployeesViewModel(_employeesRepository, _workingRatesRepository, _dialogService, _authorizationService);
        }
        #endregion

        #region ShowCustomersViewCommand
        private ICommand? _ShowCustomersViewCommand;

        /// <summary>
        /// Show CustomersView
        /// </summary>
        public ICommand? ShowCustomersViewCommand => _ShowCustomersViewCommand ??= new LambdaCommand(OnShowCustomersViewCommandExecuted, CanShowCustomersViewCommandnExecute);

        private bool CanShowCustomersViewCommandnExecute() => true;

        private void OnShowCustomersViewCommandExecuted()
        {
            CurrentViewModel = new CustomersViewModel(_customersRepository, _dialogService);
        }
        #endregion

        #region ShowOrdersViewCommand
        private ICommand? _ShowOrdersViewCommand;

        /// <summary>
        /// Show OrdersView
        /// </summary>
        public ICommand? ShowOrdersViewCommand => _ShowOrdersViewCommand ??= new LambdaCommand(OnShowOrdersViewCommandExecuted, CanShowOrdersViewCommandnExecute);

        private bool CanShowOrdersViewCommandnExecute() => true;

        private void OnShowOrdersViewCommandExecuted()
        {
            CurrentViewModel = new OrdersViewModel(
                _ordersRepository, 
                _ordersDetailsRepository,
                _productsRepository, 
                _employeesRepository, 
                _customersRepository,
                _shippersRepository,
                _dialogService);
        }
        #endregion

        #region ShowSuppliesViewCommand
        private ICommand? _ShowSuppliesViewCommand;

        /// <summary>
        /// Show SuppliesView
        /// </summary>
        public ICommand? ShowSuppliesViewCommand => _ShowSuppliesViewCommand ??= new LambdaCommand(OnShowSuppliesViewCommandExecuted, CanShowSuppliesViewCommandnExecute);

        private bool CanShowSuppliesViewCommandnExecute() => true;

        private void OnShowSuppliesViewCommandExecuted()
        {
            CurrentViewModel = new SuppliesViewModel(
                _suppliesRepository,
                _suppliesDetailsRepository,
                _productsRepository,
                _suppliersRepository,
                _dialogService);
        }
        #endregion

        #region ShowStatisticsViewCommand
        private ICommand? _ShowStatisticsViewCommand;

        /// <summary>
        /// Show StatisticView
        /// </summary>
        public ICommand? ShowStatisticsViewCommand => _ShowStatisticsViewCommand ??= new LambdaCommand(OnShowStatisticsViewCommandExecuted, CanShowStatisticsViewCommandnExecute);

        private bool CanShowStatisticsViewCommandnExecute() => true;

        private void OnShowStatisticsViewCommandExecuted()
        {
            CurrentViewModel = new StatisticsViewModel(_productsRepository, _categoriesRepository, _employeesRepository, _ordersRepository, _ordersDetailsRepository, _dialogService, _statisticsService);
        }
        #endregion

        #endregion

        public MainWindowViewModel() : this(
            new DebugProductsRepository(),
            new DebugCategoriesRepository(),
            new DebugEmployeesRepository(),
            new DebugCustomersRepository(),
            new DebugOrdersRepository(),
            new DebugOrdersDetailsRepository(),
            new DebugSuppliesRepository(),
            new DebugSuppliesDetailsRepository(),
            new DebugWorkingRatesRepository(),
            new DebugSuppliersRepository(),
            new DebugShippersRepository(),
            new UserDialogService(),
            new StatisticsCollectionService(),
            new AuthorizationService(),
            new RegisterRequestValidator())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public MainWindowViewModel(
            IRepository<Product> productsRepository, 
            IRepository<Category> categoriesRepository,
            IRepository<Employee> employeesRepository, 
            IRepository<Customer> customersRepository,
            IRepository<Order> ordersRepository,
            IRepository<OrderDetails> ordersDetailsRepository,
            IRepository<Supply> suppliesRepository,
            IRepository<SupplyDetails> suppliesDetailsRepository,
            IRepository<WorkingRate> workingRatesRepository,
            IRepository<Supplier> suppliersRepository,
            IRepository<Shipper> shippersRepository,
            IUserDialogService dialogService,
            IStatisticsCollectionService statisticsService,
            IAuthorizationService authorizationService,
            IValidator<RegisterRequest> registerValidator)
        {
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
            _employeesRepository = employeesRepository;
            _customersRepository = customersRepository;
            _ordersRepository = ordersRepository;
            _ordersDetailsRepository = ordersDetailsRepository;
            _suppliesRepository = suppliesRepository;
            _suppliesDetailsRepository = suppliesDetailsRepository;
            _workingRatesRepository = workingRatesRepository;
            _suppliersRepository = suppliersRepository;
            _shippersRepository = shippersRepository;
            _dialogService = dialogService;
            _statisticsService = statisticsService;
            _registerValidator = registerValidator;
            _authorizationService = authorizationService;
        }
    }       
}
