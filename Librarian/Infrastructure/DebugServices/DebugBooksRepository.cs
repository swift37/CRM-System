using Librarian.DAL.Entities;
using Librarian.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.DebugServices
{
    class DebugBooksRepository : IRepository<Book>
    {
        public DebugBooksRepository()
        {
            var books = Enumerable.Range(1, 100)
                .Select(i => new Book
                {
                    Id = i,
                    Name = $"Test book #{i}"
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

        public IQueryable<Book>? Entities { get; }

        public Book? Add(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<Book?>? AddAsync(Book entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Book? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Book entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
