using Librarian.DAL.Entities;
using Librarian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.DebugServices
{
    public class DebugTransactionsRepository : IRepository<Order>
    {
        public DebugTransactionsRepository()
        {
            var booksRepository = new DebugBooksRepository();
            var buyersRepository = new DebugBuyersRepository();
            var sellersRepository = new DebugSellersRepository();

            if (booksRepository.Entities is null || buyersRepository.Entities is null || sellersRepository.Entities is null) 
                throw new ArgumentNullException("Error defining entity collection in one of the debug repositories");

            var random = new Random();

            Entities = Enumerable.Range(1, 100)
                .Select(i => new Order
                {
                    TransactionDate = DateTime.Now,
                    Book = random.NextItem(booksRepository.Entities.ToArray()),
                    Seller = random.NextItem(sellersRepository.Entities.ToArray()),
                    Buyer = random.NextItem(buyersRepository.Entities.ToArray()),
                    Discount = random.Next(50),
                    Amount = (decimal)(random.NextDouble() * 300 + 50)
                }).AsQueryable();
        }

        public IQueryable<Order>? Entities { get; }

        public Order? Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Order?>? AddAsync(Order entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Order? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order?>? GetAsync(int id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
