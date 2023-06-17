using Librarian.DAL.Entities;
using Librarian.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.DebugServices
{
    class DebugCustomersRepository : IRepository<Customer>
    {
        public DebugCustomersRepository()
        {
            var random = new Random();

            Entities = Enumerable.Range(1, 30)
                .Select(i => new Customer
                {
                    Name = $"Customer {i}",
                    Surname = "Surnm",
                    ContactName = $"Tester",
                    ContactTitle = $"Manager",
                    Address = $"USA, New York, Test st.",
                    ContactNumber = random.Next(100000000, 999999999).ToString(),
                    ContactMail = $"customer{i}@gmail.com",
                    CashbackBalance = (decimal)(random.NextDouble() * 100)
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

        public void Archive(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task ArchiveAsync(Customer entity)
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
