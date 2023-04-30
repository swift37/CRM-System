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
    class DebugSellersRepository : IRepository<Seller>
    {
        public DebugSellersRepository()
        {
            Entities = Enumerable.Range(1,15)
                .Select(i => new Seller
                {
                    Id = i,
                    Name = $"TS Name #{i}",
                    Surname = $"TS Surname #{i}",
                    Patronymic = $"TS Patronymic #{i}"
                }).AsQueryable();
        }

        public IQueryable<Seller>? Entities { get; }

        public Seller? Add(Seller entity)
        {
            throw new NotImplementedException();
        }

        public Task<Seller?>? AddAsync(Seller entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Seller? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Seller?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Seller entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Seller entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
