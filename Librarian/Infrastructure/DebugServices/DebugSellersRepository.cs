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
    class DebugSellersRepository : IRepository<Employee>
    {
        public DebugSellersRepository()
        {
            Entities = Enumerable.Range(1,15)
                .Select(i => new Employee
                {
                    Id = i,
                    Name = $"TS Name #{i}",
                    Surname = $"TS Surname #{i}",
                    Patronymic = $"TS Patronymic #{i}"
                }).AsQueryable();
        }

        public IQueryable<Employee>? Entities { get; }

        public Employee? Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?>? AddAsync(Employee entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Employee? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Employee entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
