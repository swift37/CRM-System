using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;

namespace Librarian.Services.Interfaces
{
    public interface IStatisticsCollectionService
    {
        StatisticsDetails CollectProductStatistics(
            Product? product, 
            IRepository<OrderDetails> _orderDetailsRepository);

        StatisticsDetails CollectCategoryStatistics(
            Category? category,
            IRepository<OrderDetails> _orderDetailsRepository);

        StatisticsDetails CollectEmployeeStatistics(
            Employee? employee,
            IRepository<Order> _ordersRepository);

    }
}
