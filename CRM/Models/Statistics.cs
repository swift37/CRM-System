using CRM.Interfaces;

namespace CRM.Models
{
    public class Statistics<T> where T : IEntity
    {
        public T? Entity { get; set; }

        public double Popularity { get; set; }

        public int TotalSales { get; set; }

        public decimal TotalIncome { get; set; }
    }
}
