using CRM.DAL.Entities;
using CRM.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.DebugServices
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
        public bool AutoSaveChanges { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Supplier? Add(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier?>? AddAsync(Supplier entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void Archive(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public Task ArchiveAsync(Supplier entity)
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

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UnArchive(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public Task UnArchiveAsync(Supplier entity)
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
