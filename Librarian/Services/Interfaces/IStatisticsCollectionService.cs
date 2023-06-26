using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;

namespace Librarian.Services.Interfaces
{
    public interface IStatisticsCollectionService
    {
        GlobalStatistics CollectGlobalStatistics(
            IRepository<Order> ordersRepository, TimePeriod period);

        StatisticsDetails CollectProductStatistics(
            Product? product, 
            IRepository<OrderDetails> ordersDetailsRepository);

        StatisticsDetails CollectCategoryStatistics(
            Category? category,
            IRepository<OrderDetails> ordersDetailsRepository);

        StatisticsDetails CollectEmployeeStatistics(
            Employee? employee,
            IRepository<Order> ordersRepository);

        public enum TimePeriod
        {
            Today = 1,
            Week = 7, 
            Month = 30,
            Yaer = 365
        }

    }
}
