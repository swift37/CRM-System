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
    public class DebugSuppliersRepository : IRepository<Supplier>
    {
        public DebugSuppliersRepository()
        {
            Entities = Enumerable.Range(1, 15).Select(
                i => new Supplier
                {
                    Name = $"Supplier {i}",
                    ContactName = $"Cont Name {i}",
                    ContactNumber = $"{i}555{i}555{i}",
                    ContactMail = $"supplier{i}@test.test",
                    ContactTitle = $"Cont Title {i}",
                    Address = $"Address {i}"
                }).AsQueryable();
        }

        public IQueryable<Supplier>? Entities { get; set; }

        public Supplier? Add(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier?>? AddAsync(Supplier entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Supplier? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Supplier entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
