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
    class DebugBuyersRepository : IRepository<Buyer>
    {
        public DebugBuyersRepository()
        {
            var random = new Random();

            Entities = Enumerable.Range(1, 30)
                .Select(i => new Buyer
                {
                    Id = i,
                    Name = $"Test buyer #{i}",
                    Surname = $"Test surname #{i}", 
                    ContactNumber = random.Next(100000000, 999999999).ToString(),
                    ContactMail = $"buyer{i}@gmail.com",
                    CashbackBalance = random.Next(10, 50)
                }).AsQueryable();
        }

        public IQueryable<Buyer>? Entities { get; }

        public Buyer? Add(Buyer entity)
        {
            throw new NotImplementedException();
        }

        public Task<Buyer?>? AddAsync(Buyer entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Buyer? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Buyer?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Buyer entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Buyer entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
