using CRM.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Interfaces
{
    public interface ITradingService
    {
        public IEnumerable<Order>? Orders { get; }

        Task<Order?> CrateOrderAsync(string productName, Employee employee, Customer customer, decimal ordersАmount);
    }
}
