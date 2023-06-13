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
    class DebugBuyersRepository : IRepository<Customer>
    {
        public DebugBuyersRepository()
        {
            var random = new Random();

            Entities = Enumerable.Range(1, 30)
                .Select(i => new Customer
                {
                    Id = i,
                    Name = $"Test buyer #{i}",
                    Surname = $"Test surname #{i}", 
                    ContactNumber = random.Next(100000000, 999999999).ToString(),
                    ContactMail = $"buyer{i}@gmail.com",
                    CashbackBalance = random.Next(10, 50)
                }).AsQueryable();
        }

        public IQueryable<Customer>? Entities { get; }

        public Customer? Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?>? AddAsync(Customer entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Customer? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
