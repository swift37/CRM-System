using Librarian.DAL.Entities;
using Librarian.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.DebugServices
{
    class DebugCategoriesRepository : IRepository<Category>
    {
        public DebugCategoriesRepository()
        {
            Entities = Enumerable.Range(1, 15)
                .Select(i => new Category
                {
                    Id = i,
                    Name = $"Test category #{i}"
                }).AsQueryable();
        }

        public IQueryable<Category>? Entities { get; }

        public Category? Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<Category?>? AddAsync(Category entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Category? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
