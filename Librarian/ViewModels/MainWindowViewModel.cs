using Librarian.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IServiceProvider _services = null!;

        #region Properties

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

        #region IsEmployeeTabAccessible
        /// <summary>
        /// Is employee tab accessible?
        /// </summary>
        public bool IsEmployeeTabAccessible => CurrentEmployee?.PermissionLevel > 1;
        #endregion

        #region IsStatisticsTabAccessible
        /// <summary>
        /// Is statistics tab accessible?
        /// </summary>
        public bool IsStatisticsTabAccessible => CurrentEmployee?.PermissionLevel > 2;
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
            CurrentViewModel = _services.GetRequiredService<DashboardViewModel>();
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
            var productsViewModel = _services.GetRequiredService<ProductsViewModel>();
            productsViewModel.CurrentEmployee = CurrentEmployee;
            CurrentViewModel = productsViewModel;
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
            CurrentViewModel = _services.GetRequiredService<EmployeesViewModel>();
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
            CurrentViewModel = _services.GetRequiredService<CustomersViewModel>();
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
            var orderViewModel = _services.GetRequiredService<OrdersViewModel>();
            orderViewModel.CurrentEmployee = CurrentEmployee;
            CurrentViewModel = orderViewModel;
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
            var suppliesViewModel = _services.GetRequiredService<SuppliesViewModel>();
            suppliesViewModel.CurrentEmployee = CurrentEmployee;
            CurrentViewModel = suppliesViewModel;
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
            CurrentViewModel = _services.GetRequiredService<StatisticsViewModel>();
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public MainWindowViewModel(IServiceProvider services) => _services = services;

    }       
}
