using Librarian.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Services.Interfaces
{
    public interface ITradingService
    {
        public IEnumerable<Transaction>? Transactions { get; }

        Task<Transaction?> CrateTransactionAsync(string bookName, Seller seller, Buyer buyer, decimal transactionАmount);
    }
}
