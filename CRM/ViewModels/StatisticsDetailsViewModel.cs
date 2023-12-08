using Swftx.Wpf.ViewModels;
using System.Linq;
using CRM.Models;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Configurations;
namespace CRM.ViewModels
{
    public class StatisticsDetailsViewModel : ViewModel
    {
        #region Values
        private ChartValues<DataPoint>? _Values = new ChartValues<DataPoint>();

        /// <summary>
        /// Chart Values Collection
        /// </summary>
        public ChartValues<DataPoint>? Values
        {
            get => _Values;
            set
            {
                if (Set(ref _Values, value))
                    OnPropertyChanged(nameof(MaxOyValue));
            }
        }
        #endregion

        #region Configuration
        private CartesianMapper<DataPoint>? _Configuration;

        /// <summary>
        /// Chart Configuration Collection
        /// </summary>
        public CartesianMapper<DataPoint>? Configuration { get => _Configuration; set => Set(ref _Configuration, value); }
        #endregion

        #region MaxOyValue
        /// <summary>
        /// Max Oy value
        /// </summary>
        public double MaxOyValue => Values?.Select(dp => dp.Value).Max() ?? 0;
        #endregion

        #region AverageSalesPerMonth
        private double _AverageSalesPerMonth;

        /// <summary>
        /// Average count of sales per month
        /// </summary>
        public double AverageSalesPerMonth { get => _AverageSalesPerMonth; set => Set(ref _AverageSalesPerMonth, value); }
        #endregion

        #region AverageMonthlyIncome
        private decimal _AverageMonthlyIncome;

        /// <summary>
        /// Average monthly income
        /// </summary>
        public decimal AverageMonthlyIncome { get => _AverageMonthlyIncome; set => Set(ref _AverageMonthlyIncome, value); }
        #endregion

        #region LastMonthProfit
        private double _LastMonthProfit;

        /// <summary>
        /// Profit for the last month
        /// </summary>
        public double LastMonthProfit { get => _LastMonthProfit; set => Set(ref _LastMonthProfit, value); }
        #endregion

        public StatisticsDetailsViewModel() { }

        public void InitProps(StatisticsDetails statistics)
        {
            Configuration = new CartesianMapper<DataPoint>()
                .Y(dp => dp.Value);

            Values = statistics.Values.AsChartValues();
            AverageSalesPerMonth = statistics.AverageSalesPerMonth;
            AverageMonthlyIncome = statistics.AverageMonthlyIncome;
            LastMonthProfit = statistics.LastMonthProfit;
        }
    }
}
