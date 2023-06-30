using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
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
    public class StatisticsViewModel : ViewModel
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderDetails> _ordersDetailsRepository;
        private readonly IUserDialogService _dialogService;
        private readonly IStatisticsCollectionService _statisticsService;
        private CollectionViewSource _ProductsStatisticsViewSource;
        private CollectionViewSource _CategoriesStatisticsViewSource;
        private CollectionViewSource _EmployeesStatisticsViewSource;

        #region Properties

        #region ProductsStatistics
        private ObservableCollection<Statistics<Product>>? _ProductsStatistics = new ObservableCollection<Statistics<Product>>();

        /// <summary>
        /// Top Products collection.
        /// </summary>
        public ObservableCollection<Statistics<Product>>? ProductsStatistics
        {
            get => _ProductsStatistics;
            set
            {
                if (Set(ref _ProductsStatistics, value))
                    _ProductsStatisticsViewSource.Source = value;
                OnPropertyChanged(nameof(ProductsStatisticsView));
            }
        }
        #endregion

        #region ProductsStatisticsView
        /// <summary>
        /// Top Products collection view.
        /// </summary>
        public ICollectionView ProductsStatisticsView => _ProductsStatisticsViewSource.View;
        #endregion

        #region ProductsFilter
        private string? _ProductsFilter;

        /// <summary>
        /// Products filter.
        /// </summary>
        public string? ProductsFilter
        {
            get => _ProductsFilter;
            set
            {
                if (Set(ref _ProductsFilter, value))
                    _ProductsStatisticsViewSource.View.Refresh();
            }
        }
        #endregion

        #region SelectedProductStatistics
        private Statistics<Product>? _SelectedProductStatistics;

        /// <summary>
        /// Selected product statistics
        /// </summary>
        public Statistics<Product>? SelectedProductStatistics { get => _SelectedProductStatistics; set => Set(ref _SelectedProductStatistics, value); }
        #endregion

        #region CategoriesStatistics
        private ObservableCollection<Statistics<Category>>? _CategoriesStatistics = new ObservableCollection<Statistics<Category>>();

        /// <summary>
        /// Top Categories collection.
        /// </summary>
        public ObservableCollection<Statistics<Category>>? CategoriesStatistics
        {
            get => _CategoriesStatistics;
            set
            {
                if (Set(ref _CategoriesStatistics, value))
                    _CategoriesStatisticsViewSource.Source = value;
                OnPropertyChanged(nameof(CategoriesStatisticsView));
            }
        }
        #endregion

        #region CategoriesStatisticsView
        /// <summary>
        /// Top Categories collection view.
        /// </summary>
        public ICollectionView CategoriesStatisticsView => _CategoriesStatisticsViewSource.View;
        #endregion

        #region CategoriesFilter
        private string? _CategoriesFilter;

        /// <summary>
        /// Categories filter.
        /// </summary>
        public string? CategoriesFilter
        {
            get => _CategoriesFilter;
            set
            {
                if (Set(ref _CategoriesFilter, value))
                    _CategoriesStatisticsViewSource.View.Refresh();
            }
        }
        #endregion

        #region SelectedCategoryStatistics
        private Statistics<Category>? _SelectedCategoryStatistics;

        /// <summary>
        /// Selected category statistics
        /// </summary>
        public Statistics<Category>? SelectedCategoryStatistics { get => _SelectedCategoryStatistics; set => Set(ref _SelectedCategoryStatistics, value); }
        #endregion

        #region EmployeesStatistics
        private ObservableCollection<Statistics<Employee>>? _EmployeesStatistics = new ObservableCollection<Statistics<Employee>>();

        /// <summary>
        /// Top Employees collection.
        /// </summary>
        public ObservableCollection<Statistics<Employee>>? EmployeesStatistics
        {
            get => _EmployeesStatistics;
            set
            {
                if (Set(ref _EmployeesStatistics, value))
                    _EmployeesStatisticsViewSource.Source = value;
                OnPropertyChanged(nameof(EmployeesStatisticsView));
            }
        }
        #endregion

        #region EmployeesStatisticsView
        /// <summary>
        /// Top Employees collection view.
        /// </summary>
        public ICollectionView EmployeesStatisticsView => _EmployeesStatisticsViewSource.View;
        #endregion

        #region EmployeesFilter
        private string? _EmployeesFilter;

        /// <summary>
        /// Employees filter.
        /// </summary>
        public string? EmployeesFilter
        {
            get => _EmployeesFilter;
            set
            {
                if (Set(ref _EmployeesFilter, value))
                    _EmployeesStatisticsViewSource.View.Refresh();
            }
        }
        #endregion

        #region SelectedEmployeeStatistics
        private Statistics<Employee>? _SelectedEmployeeStatistics;

        /// <summary>
        /// Selected employee statistics
        /// </summary>
        public Statistics<Employee>? SelectedEmployeeStatistics { get => _SelectedEmployeeStatistics; set => Set(ref _SelectedEmployeeStatistics, value); }
        #endregion

        #endregion

        #region Commands

        #region CollectStatisticsCommand
        private ICommand? _CollectStatisticsCommand;

        /// <summary>
        /// Collect products statistics 
        /// </summary>
        public ICommand? CollectStatisticsCommand => _CollectStatisticsCommand ??= new LambdaCommand(OnCollectStatisticsCommandExecuted, CanCollectStatisticsCommandnExecute);

        private bool CanCollectStatisticsCommandnExecute() => true;

        private async void OnCollectStatisticsCommandExecuted()
        {
            await CollectProductsOrdersStatisticAsync();
            await CollectCategoriesTransactionsStatisticAsync();
            await CollectEmployeesDealsStatisticAsync();
        }

        private async Task CollectProductsOrdersStatisticAsync()
        {
            var ordersDetails = _ordersDetailsRepository.Entities;
            var orders = _ordersRepository.Entities;

            if (ordersDetails is null || orders is null) return;

            var topProductsQuery = ordersDetails.GroupBy(od => od.Product)
                .Select(od => new Statistics<Product>
                {
                    Entity = od.Key,
                    Popularity = (double)od.Select(d => d.OrderId).Distinct().Count() / orders.Count(),
                    TotalSales = od.Sum(o => o.Quantity),
                    TotalIncome = od.Sum(o => o.UnitPrice * o.Quantity)
                })
                .OrderByDescending(product => product.TotalSales)
                .Take(100);

            ProductsStatistics?.Clear();
            ProductsStatistics = (await topProductsQuery.ToArrayAsync()).ToObservableCollection();
        }

        private async Task CollectCategoriesTransactionsStatisticAsync()
        {
            var ordersDetails = _ordersDetailsRepository.Entities;
            var orders = _ordersRepository.Entities;

            if (ordersDetails is null || orders is null) return;

            var topCategoriesQuery = ordersDetails.GroupBy(od => od.Product.Category)
                .Select(od => new Statistics<Category>
                { 
                    Entity = od.Key, 
                    Popularity = (double)od.Select(d => d.OrderId).Distinct().Count() / orders.Count(),
                    TotalSales = od.Sum(o => o.Quantity), 
                    TotalIncome = od.Sum(o => o.UnitPrice * o.Quantity) 
                })
                .OrderByDescending(category => category.TotalSales);

            CategoriesStatistics?.Clear();
            CategoriesStatistics = (await topCategoriesQuery.ToArrayAsync()).ToObservableCollection();
        }

        private async Task CollectEmployeesDealsStatisticAsync()
        {
            var orders = _ordersRepository.Entities;

            if (orders is null) return;

            var topEmployeesQuery = orders.GroupBy(o => o.Employee)
                .Select(o => new Statistics<Employee>
                {
                    Entity = o.Key,
                    Popularity = (double)o.Count() / orders.Count(),
                    TotalSales = o.Sum(o => o.ProductsQuantity),
                    TotalIncome = o.Sum(o => o.Amount)
                })
                .OrderByDescending(employee => employee.TotalSales);

            EmployeesStatistics?.Clear();
            EmployeesStatistics = (await topEmployeesQuery.ToArrayAsync()).ToObservableCollection();
        }
        #endregion

        #region CollectProductStatisticsDetailsCommand
        private ICommand? _CollectProductStatisticsDetailsCommand;

        /// <summary>
        /// Collect product statistics details command
        /// </summary>
        public ICommand? CollectProductStatisticsDetailsCommand => _CollectProductStatisticsDetailsCommand ??= new LambdaCommand<Product>(OnCollectProductStatisticsDetailsCommandExecuted, CanCollectProductStatisticsDetailsCommandExecute);

        private bool CanCollectProductStatisticsDetailsCommandExecute(Product? product) => 
            product != null || (SelectedProductStatistics != null && SelectedProductStatistics.Entity != null);

        private void OnCollectProductStatisticsDetailsCommandExecuted(Product? product)
        {
            var selectedProduct = product ?? SelectedProductStatistics?.Entity;

            var statisticsDetails = _statisticsService.CollectProductStatistics(selectedProduct, _ordersDetailsRepository);

            _dialogService.ShowStatisticsDetails(statisticsDetails);
        }
        #endregion

        #region CollectCategoryStatisticsDetailsCommand
        private ICommand? _CollectCategoryStatisticsDetailsCommand;

        /// <summary>
        /// Collect category statistics details command
        /// </summary>
        public ICommand? CollectCategoryStatisticsDetailsCommand => _CollectCategoryStatisticsDetailsCommand ??= new LambdaCommand<Category>(OnCollectCategoryStatisticsDetailsCommandExecuted, CanCollectCategoryStatisticsDetailsCommandExecute);

        private bool CanCollectCategoryStatisticsDetailsCommandExecute(Category? category) =>
            category != null || (SelectedCategoryStatistics != null && SelectedCategoryStatistics.Entity != null);

        private void OnCollectCategoryStatisticsDetailsCommandExecuted(Category? category)
        {
            var selectedCategory = category ?? SelectedCategoryStatistics?.Entity;

            var statisticsDetails = _statisticsService.CollectCategoryStatistics(selectedCategory, _ordersDetailsRepository);

            _dialogService.ShowStatisticsDetails(statisticsDetails);
        }
        #endregion

        #region CollectEmployeeStatisticsDetailsCommand
        private ICommand? _CollectEmployeeStatisticsDetailsCommand;

        /// <summary>
        /// Collect employee statistics details command
        /// </summary>
        public ICommand? CollectEmployeeStatisticsDetailsCommand => _CollectEmployeeStatisticsDetailsCommand ??= new LambdaCommand<Employee>(OnCollectEmployeeStatisticsDetailsCommandExecuted, CanCollectEmployeeStatisticsDetailsCommandExecute);

        private bool CanCollectEmployeeStatisticsDetailsCommandExecute(Employee? employee) =>
            employee != null || (SelectedEmployeeStatistics != null && SelectedEmployeeStatistics.Entity != null);

        private void OnCollectEmployeeStatisticsDetailsCommandExecuted(Employee? employee)
        {
            var selectedEmployee = employee ?? SelectedEmployeeStatistics?.Entity;

            var statisticsDetails = _statisticsService.CollectEmployeeStatistics(selectedEmployee, _ordersRepository);

            _dialogService.ShowStatisticsDetails(statisticsDetails);
        }
        #endregion

        #endregion

        public StatisticsViewModel(
            IRepository<Product> productsRepository,
            IRepository<Category> categoriesRepository,
            IRepository<Employee> employeesRepository, 
            IRepository<Order> ordersRepository,
            IRepository<OrderDetails> ordersDetailsRepository,
            IUserDialogService dialogService,
            IStatisticsCollectionService statisticsService)
        {
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
            _employeesRepository = employeesRepository;
            _ordersRepository = ordersRepository;
            _ordersDetailsRepository = ordersDetailsRepository;
            _dialogService = dialogService;
            _statisticsService = statisticsService;

            _ProductsStatisticsViewSource = new CollectionViewSource();
            _CategoriesStatisticsViewSource = new CollectionViewSource();
            _EmployeesStatisticsViewSource = new CollectionViewSource();

            _ProductsStatisticsViewSource.Filter += OnProductFilter;
            _CategoriesStatisticsViewSource.Filter += OnCategoryFilter;
            _EmployeesStatisticsViewSource.Filter += OnEmployeeFilter;
        }

        private void OnProductFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Statistics<Product> productStatistics) || string.IsNullOrWhiteSpace(ProductsFilter)) return;

            if (!productStatistics.Entity?.Name?.Contains(ProductsFilter) ?? true) 
                e.Accepted = false;
        }

        private void OnCategoryFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Statistics<Category> categoryStatistics) || string.IsNullOrWhiteSpace(CategoriesFilter)) return;

            if (!categoryStatistics.Entity?.Name?.Contains(CategoriesFilter) ?? true)
                e.Accepted = false;
        }

        private void OnEmployeeFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Statistics<Employee> employeeStatistics) || string.IsNullOrWhiteSpace(EmployeesFilter)) return;

            if (!employeeStatistics.Entity?.Name?.Contains(EmployeesFilter) ?? true)
                e.Accepted = false;
        }
    }
}
