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
        private readonly IRepository<Book> _books;
        private readonly IRepository<Transaction> _transactions;

        public IEnumerable<Transaction>? Transactions => _transactions.Entities;

        public TradingService(IRepository<Book> books, IRepository<Transaction> transactions)
        {
            _books = books;
            _transactions = transactions;
        }

        public async Task<Transaction?> CrateTransactionAsync(string bookName, Seller seller, Buyer buyer, decimal transactionАmount)
        {
            if (_books.Entities is null) throw new ArgumentNullException(nameof(_books.Entities));
 
            var book = await _books.Entities.FirstOrDefaultAsync(book => book.Name == bookName).ConfigureAwait(false);
            if (book is null) return null;

            var transaction = new Transaction
            {
                Book = book,
                Seller = seller,
                Buyer = buyer,
                Price = transactionАmount
            };

            return await _transactions.AddAsync(transaction);
        }
    }
}
