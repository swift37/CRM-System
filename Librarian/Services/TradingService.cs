using Librarian.DAL.Entities;
using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;
using Librarian.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.Services
{
    public class TradingService : ITradingService
    {
        private readonly IRepository<Product> _books;
        private readonly IRepository<Order> _transactions;

        public IEnumerable<Order>? Transactions => _transactions.Entities;

        public TradingService(IRepository<Product> books, IRepository<Order> transactions)
        {
            _books = books;
            _transactions = transactions;
        }

        public async Task<Order?> CrateTransactionAsync(string bookName, Employee seller, Customer buyer, decimal transactionАmount)
        {
            if (_books.Entities is null) throw new ArgumentNullException(nameof(_books.Entities));
 
            var book = await _books.Entities.FirstOrDefaultAsync(book => book.Name == bookName).ConfigureAwait(false);
            if (book is null) return null;

            var transaction = new Order
            {
                //Book = book,
                //Seller = seller,
                //Buyer = buyer,
                //Amount = transactionАmount
            };

            return await _transactions.AddAsync(transaction);
        }
    }
}
