using Librarian.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Services.Interfaces
{
    public interface ITradingService
    {
        public IEnumerable<Order>? Transactions { get; }

        Task<Order?> CrateTransactionAsync(string bookName, Employee seller, Customer buyer, decimal transactionАmount);
    }
}
