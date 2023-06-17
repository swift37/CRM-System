using Librarian.DAL.Entities;
using Librarian.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.DebugServices
{
    class DebugProductsRepository : IRepository<Product>
    {
        public DebugProductsRepository()
        {
            var random = new Random();

            var suppliers = Enumerable.Range(1, 10)
                .Select(i => new Supplier
                {
                    Name = $"Company {i}",
                    ContactName = $"Supplier {i}",
                    ContactTitle = "Manager",
                    ContactMail = $"company{i}@gmail.com",
                    ContactNumber = random.Next(100000000, 999999999).ToString(),
                    Address = $"USA, New York, Test st."
                })
                .ToArray();

            var products = Enumerable.Range(1, 100)
                .Select(i => new Product
                {
                    Name = $"Product {i}",
                    UnitsInEnterprise = random.Next(100),
                    UnitsInStock = random.Next(1000),
                    UnitPrice = (decimal)(random.NextDouble() * 300 + 50),
                    Supplier = random.NextItem(suppliers)
                })
                .ToArray();

            var categories = Enumerable.Range(1, 10)
                .Select(i => new Category
                {
                    Id = i,
                    Name = $"Category #{i}"
                })
                .ToArray();

            foreach (var product in products)
            {
                var category = categories[product.Id % categories.Length];
                category.Products?.Add(product);
                product.Category = category;
            }

            Entities = products.AsQueryable();
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

        public void Archive(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task ArchiveAsync(Product entity)
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

        public void UnArchive(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task UnArchiveAsync(Product entity)
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
