using Librarian.DAL.Entities;
using Librarian.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.DebugServices
{
    class DebugBooksRepository : IRepository<Product>
    {
        public DebugBooksRepository()
        {
            var random = new Random();

            var books = Enumerable.Range(1, 100)
                .Select(i => new Product
                {
                    Id = i,
                    Name = $"Test book #{i}",
                    Price = (decimal)(random.NextDouble() * 300 + 50)
                })
                .ToArray();

            var categories = Enumerable.Range(1, 10)
                .Select(i => new Category
                {
                    Id = i,
                    Name = $"Test category #{i}"
                })
                .ToArray();

            foreach (var book in books)
            {
                var category = categories[book.Id % categories.Length];
                category.Books?.Add(book);
                book.Category = category;
            }

            Entities = books.AsQueryable();
        }

        public IQueryable<Product>? Entities { get; }

        public Product? Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Product?>? AddAsync(Product entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Product? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
