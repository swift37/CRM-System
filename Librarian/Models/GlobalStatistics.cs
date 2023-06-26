using LiveCharts;

namespace Librarian.Models
{
    public class GlobalStatistics
    {
        public ChartValues<DataPoint>? Values { get; set; }

        public decimal Income { get; set; }

        public int OrdersCount { get; set; }

        public decimal AverageOrderAmount { get; set; }

    }
}
