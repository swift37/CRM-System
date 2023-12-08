using LiveCharts;

namespace CRM.Models
{
    public class StatisticsDetails
    {
        public ChartValues<DataPoint>? Values { get; set; }

        public double AverageSalesPerMonth { get; set; }

        public decimal AverageMonthlyIncome { get; set; }

        public double LastMonthProfit { get; set; }
    }
}
