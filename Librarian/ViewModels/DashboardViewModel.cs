using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services;
using Librarian.Services.Interfaces;
using LiveCharts.Configurations;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class DashboardViewModel : ViewModel
    {
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderDetails> _ordersDetailsRepository;
        private readonly IStatisticsCollectionService _statisticsService;

        #region Properties

        #region LastOrders
        private ObservableCollection<OrderDetails>? _LastOrders;

        /// <summary>
        /// Last Orders
        /// </summary>
        public ObservableCollection<OrderDetails>? LastOrders { get => _LastOrders; set => Set(ref _LastOrders, value); }
        #endregion

        #region TodayStatistics
        private GlobalStatistics? _TodayStatistics;

        /// <summary>
        /// Today Statistics
        /// </summary>
        public GlobalStatistics? TodayStatistics { get => _TodayStatistics; set => Set(ref _TodayStatistics, value); }
        #endregion

        #region WeekStatistics
        private GlobalStatistics? _WeekStatistics;

        /// <summary>
        /// Week Statistics
        /// </summary>
        public GlobalStatistics? WeekStatistics { get => _WeekStatistics; set => Set(ref _WeekStatistics, value); }
        #endregion

        #region GlobalStatistics
        private GlobalStatistics? _GlobalStatistics;

        /// <summary>
        /// Global Statistics
        /// </summary>
        public GlobalStatistics? GlobalStatistics { get => _GlobalStatistics; set => Set(ref _GlobalStatistics, value); }
        #endregion

        #region YearStatistics
        private GlobalStatistics? _YearStatistics;

        /// <summary>
        /// Year Statistics
        /// </summary>
        public GlobalStatistics? YearStatistics { get => _YearStatistics; set => Set(ref _YearStatistics, value); }
        #endregion

        #region OxLabeles
        private string[]? _OxLabeles;

        /// <summary>
        /// Ox Chart Labeles
        /// </summary>
        public string[]? OxLabeles { get => _OxLabeles; set => Set(ref _OxLabeles, value); }
        #endregion

        #region ChartConfiguration
        private CartesianMapper<DataPoint>? _ChartConfiguration;

        /// <summary>
        /// Chart Configuration 
        /// </summary>
        public CartesianMapper<DataPoint>? ChartConfiguration { get => _ChartConfiguration; set => Set(ref _ChartConfiguration, value); }
        #endregion

        #endregion

        #region Commands

        #region LoadStartDataCommand
        private ICommand? _LoadStartDataCommand;

        /// <summary>
        /// Load start data command
        /// </summary>
        public ICommand? LoadStartDataCommand => _LoadStartDataCommand ??= new LambdaCommandAsync(OnLoadStartDataCommandExecuted, CanLoadStartDataCommandExecute);

        private bool CanLoadStartDataCommandExecute() => true;

        private async Task OnLoadStartDataCommandExecuted()
        {
            if (_ordersDetailsRepository.Entities is null) return;

            LastOrders = (await _ordersDetailsRepository.Entities
                .OrderByDescending(o => o.Order.OrderDate)
                .Take(7)
                .Include(o => o.Order.Employee)
                .ToArrayAsync())
                .ToObservableCollection();

            _ = OnCollectMonthStatisticsCommandExecuted();
        }

        #endregion

        #region CollectTodayStatisticsCommand
        private ICommand? _CollectTodayStatisticsCommand;

        /// <summary>
        /// Collect today statistics command
        /// </summary>
        public ICommand? CollectTodayStatisticsCommand => _CollectTodayStatisticsCommand ??= new LambdaCommandAsync(OnCollectTodayStatisticsCommandExecuted, CanCollectTodayStatisticsCommandExecute);

        private bool CanCollectTodayStatisticsCommandExecute() => true;

        private async Task OnCollectTodayStatisticsCommandExecuted()
        {
            if (_ordersDetailsRepository.Entities is null) return;

            GlobalStatistics = await _statisticsService.CollectGlobalStatisticsAsync(_ordersRepository, IStatisticsCollectionService.TimePeriod.Today);
            OxLabeles = GlobalStatistics.Values?.Select(o => o.Date.ToString("HH:mm")).ToArray();
        }

        #endregion

        #region CollectWeekStatisticsCommand
        private ICommand? _CollectWeekStatisticsCommand;

        /// <summary>
        /// Collect statistics for last week command
        /// </summary>
        public ICommand? CollectWeekStatisticsCommand => _CollectWeekStatisticsCommand ??= new LambdaCommandAsync(OnCollectWeekStatisticsCommandExecuted, CanCollectWeekStatisticsCommandExecute);

        private bool CanCollectWeekStatisticsCommandExecute() => true;

        private async Task OnCollectWeekStatisticsCommandExecuted()
        {
            if (_ordersDetailsRepository.Entities is null) return;

            GlobalStatistics = await _statisticsService.CollectGlobalStatisticsAsync(_ordersRepository, IStatisticsCollectionService.TimePeriod.Week);
            OxLabeles = GlobalStatistics.Values?.Select(o => o.Date.ToString("dd.MM")).ToArray();
        }

        #endregion

        #region CollectMonthStatisticsCommand
        private ICommand? _CollectMonthStatisticsCommand;

        /// <summary>
        /// Collect statistics for last month command
        /// </summary>
        public ICommand? CollectMonthStatisticsCommand => _CollectMonthStatisticsCommand ??= new LambdaCommandAsync(OnCollectMonthStatisticsCommandExecuted, CanCollectMonthStatisticsCommandExecute);

        private bool CanCollectMonthStatisticsCommandExecute() => true;

        private async Task OnCollectMonthStatisticsCommandExecuted()
        {
            if (_ordersDetailsRepository.Entities is null) return;

            GlobalStatistics = await _statisticsService.CollectGlobalStatisticsAsync(_ordersRepository, IStatisticsCollectionService.TimePeriod.Month);
            OxLabeles = GlobalStatistics.Values?.Select(o => o.Date.ToString("dd.MM")).ToArray();
        }

        #endregion

        #region CollectYearStatisticsCommand
        private ICommand? _CollectYearStatisticsCommand;

        /// <summary>
        /// Collect statistics for last year command
        /// </summary>
        public ICommand? CollectYearStatisticsCommand => _CollectYearStatisticsCommand ??= new LambdaCommandAsync(OnCollectYearStatisticsCommandExecuted, CanCollectYearStatisticsCommandExecute);

        private bool CanCollectYearStatisticsCommandExecute() => true;

        private async Task OnCollectYearStatisticsCommandExecuted()
        {
            if (_ordersDetailsRepository.Entities is null) return;

            GlobalStatistics = await _statisticsService.CollectGlobalStatisticsAsync(_ordersRepository, IStatisticsCollectionService.TimePeriod.Year);
            OxLabeles = GlobalStatistics.Values?.Select(o => o.Date.ToString("MM.yy")).ToArray();
        }

        #endregion

        #endregion

        public DashboardViewModel() : this(
            new DebugOrdersRepository(), 
            new DebugOrdersDetailsRepository(), 
            new StatisticsCollectionService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnCollectMonthStatisticsCommandExecuted();
        }

        public DashboardViewModel(
            IRepository<Order> transactionsRepository, 
            IRepository<OrderDetails> ordersDetailsRepository,
            IStatisticsCollectionService statisticsService)
        {
            _ordersRepository = transactionsRepository;
            _ordersDetailsRepository = ordersDetailsRepository;
            _statisticsService = statisticsService;

            ChartConfiguration = new CartesianMapper<DataPoint>()
                .Y(dp => dp.Value);
        }
    }
}
