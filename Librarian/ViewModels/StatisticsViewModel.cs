using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        private CollectionViewSource _topProductsViewSource;

        #region Properties

        #region TopProducts
        private ObservableCollection<TopProductsInfo>? _TopProducts = new ObservableCollection<TopProductsInfo>();

        /// <summary>
        /// Top Products collection.
        /// </summary>
        public ObservableCollection<TopProductsInfo>? TopProducts
        {
            get => _TopProducts;
            set
            {
                if (Set(ref _TopProducts, value))
                    _topProductsViewSource.Source = value;
                OnPropertyChanged(nameof(TopProductsView));
            }
        }
        #endregion

        #region ProductsView
        /// <summary>
        /// Top Products collection view.
        /// </summary>
        public ICollectionView TopProductsView => _topProductsViewSource.View;
        #endregion

        #region ProductsNameFilter
        private string? _ProductsNameFilter;

        /// <summary>
        /// Products name filter.
        /// </summary>
        public string? ProductsNameFilter 
        { 
            get => _ProductsNameFilter; 
            set
            {
                if (Set(ref _ProductsNameFilter, value))
                    _topProductsViewSource.View.Refresh();
            } 
        }
        #endregion

        #region TopCategories
        /// <summary>
        /// Top categories collection view.
        /// </summary>
        public ObservableCollection<TopCategoryInfo> TopCategories { get; set; } = new ObservableCollection<TopCategoryInfo>();
        #endregion

        #region TopEmployees
        /// <summary>
        /// Top Employees collection view.
        /// </summary>
        public ObservableCollection<TopEmployeeInfo> TopEmployees { get; set; } = new ObservableCollection<TopEmployeeInfo>();
        #endregion

        #region ProductsCount
        private int _ProductsCount;

        /// <summary>
        /// Products count.
        /// </summary>
        public int ProductsCount { get => _ProductsCount; set => Set(ref _ProductsCount, value); }
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
            if (_productsRepository.Entities is null) return;
            ProductsCount = await _productsRepository.Entities.CountAsync();

            await CollectProductsOrdersStatisticAsync();
            await CollectCategoriesTransactionsStatisticAsync();
            await CollectEmployeesDealsStatisticAsync();
        }

        private async Task CollectProductsOrdersStatisticAsync()
        {
            var ordersDetails = _ordersDetailsRepository.Entities;

            if (ordersDetails is null) return;
            if (_productsRepository.Entities is null) return;

            var topProductsQuery = ordersDetails.GroupBy(od => od.ProductId)
                .Select(prodStat => new { 
                    ProductId = prodStat.Key, 
                    SalesCount = prodStat.Sum(d => d.Quantity), 
                    SalesAmount = prodStat.Sum(o => o.UnitPrice * o.Quantity) })
                .OrderByDescending(product => product.SalesCount)
                .Take(50)
                .Join(_productsRepository.Entities,
                    prodStat => prodStat.ProductId,
                    product => product.Id,
                    (prodStat, product) => new TopProductsInfo { Product = product, SalesCount = prodStat.SalesCount, SalesAmount = prodStat.SalesAmount });

            TopProducts = (await topProductsQuery.ToArrayAsync()).ToObservableCollection();    
        }

        private async Task CollectCategoriesTransactionsStatisticAsync()
        {
            var ordersDetails = _ordersDetailsRepository.Entities;

            if (ordersDetails is null) return;
            if (_categoriesRepository.Entities is null) return;

            var topCategoriesQuery = ordersDetails.GroupBy(od => od.Product.Category.Id)
                .Select(catStat => new { 
                    CategoryId = catStat.Key, 
                    SalesCount = catStat.Sum(o => o.Quantity), 
                    SalesAmount = catStat.Sum(o => o.UnitPrice * o.Quantity) })
                .OrderByDescending(category => category.SalesCount)
                .Take(15)
                .Join(_categoriesRepository.Entities,
                    catStat => catStat.CategoryId,
                    category => category.Id,
                    (catStat, category) => new TopCategoryInfo { Category = category, SalesCount = catStat.SalesCount, SalesAmount = catStat.SalesAmount });

            TopCategories.ClearAdd(await topCategoriesQuery.ToArrayAsync());
        }

        private async Task CollectEmployeesDealsStatisticAsync()
        {
            var orders = _ordersRepository.Entities;

            if (orders is null) return;
            if (_employeesRepository.Entities is null) return;

            var topEmployeesQuery = orders.GroupBy(o => o.Employee.Id)
                .Select(empStat => new
                {
                    EmployeeId = empStat.Key,
                    SalesCount = empStat.Sum(o => o.OrderDetails.Sum(d => d.Quantity)),
                    SalesAmount = empStat.Sum(o => o.Amount)
                })
                .OrderByDescending(employee => employee.SalesCount)
                .Join(_employeesRepository.Entities,
                    empStat => empStat.EmployeeId,
                    employee => employee.Id,
                    (empStat, employee) => new TopEmployeeInfo { Employee = employee, SalesCount = empStat.SalesCount, SalesAmount = empStat.SalesAmount });

            TopEmployees.ClearAdd(await topEmployeesQuery.ToArrayAsync());
        }
        #endregion

        #endregion

        public StatisticsViewModel(
            IRepository<Product> productsRepository,
            IRepository<Category> categoriesRepository,
            IRepository<Employee> employeesRepository, 
            IRepository<Order> ordersRepository,
            IRepository<OrderDetails> ordersDetailsRepository)
        {
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
            _employeesRepository = employeesRepository;
            _ordersRepository = ordersRepository;
            _ordersDetailsRepository = ordersDetailsRepository;

            _topProductsViewSource = new CollectionViewSource();
            _topProductsViewSource.Filter += OnProductNameFilter;
        }

        private void OnProductNameFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is TopProductsInfo topProduct) || string.IsNullOrWhiteSpace(ProductsNameFilter)) return;

            if (!topProduct.Product?.Name?.Contains(ProductsNameFilter) ?? true) 
                e.Accepted = false;
        }
    }
}
