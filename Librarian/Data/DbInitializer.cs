using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.Data
{
    public class DbInitializer
    {
        private readonly LibrarianDb _dbContext;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(LibrarianDb db, ILogger<DbInitializer> logger)
        {
            _dbContext = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize database...");

            //_logger.LogInformation("Deleting an existing database...");
            //await _dbContext.Database.EnsureDeletedAsync().ConfigureAwait(false);
            //_logger.LogInformation($"Deleting an existing database comleted in {timer.ElapsedMilliseconds} ms");

            _logger.LogInformation("Migration database...");
            await _dbContext.Database.MigrateAsync().ConfigureAwait(false);
            _logger.LogInformation($"Migration database comleted in {timer.ElapsedMilliseconds} ms");

            if (await _dbContext.Products.AnyAsync()) return;

            await InitializeCategories();
            await InitializeBooks();
            await InitializeSellers();
            await InitializeBuyers();
            await InitializeTransactions();

            _logger.LogInformation($"Initialize database comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _categoriesCount = 25;

        private Category[]? _categories;

        private async Task InitializeCategories()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize categories...");

            _categories = new Category[_categoriesCount];
            for (int i = 0; i < _categoriesCount; i++)
                _categories[i] = new Category { Name = $"Category {i + 1}" };

            await _dbContext.Categories.AddRangeAsync(_categories);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize categories comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _booksCount = 1000;

        private Product[]? _books;

        private async Task InitializeBooks()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _booksRepository...");

            if (_categories is null) throw new ArgumentNullException(nameof(_categories));
            var random = new Random();
            _books = Enumerable.Range(1, _booksCount)
                .Select(i => new Product
                {
                    Name = $"Book {i}",
                    Price = (decimal)(random.NextDouble() * 300 + 50),
                    Category = random.NextItem(_categories)
                }).ToArray();

            await _dbContext.Products.AddRangeAsync(_books);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _booksRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _buyersCount = 100;

        private Customer[]? _buyers;

        private async Task InitializeBuyers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _buyersRepository...");

            var random = new Random();
            _buyers = Enumerable.Range(1, _buyersCount)
                .Select(i => new Customer
                {
                    Name = $"Buyer {i}",
                    ContactNumber = random.Next(100000000,999999999).ToString(),
                    ContactMail = $"buyer{i}@gmail.com",
                    CashbackBalance = (decimal)(random.NextDouble() * 100)
                }).ToArray();

            await _dbContext.Customers.AddRangeAsync(_buyers);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _buyersRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _sellersCount = 15;

        private Employee[]? _sellers;

        private async Task InitializeSellers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _sellersRepository...");

            var random = new Random();
            _sellers = Enumerable.Range(1, _sellersCount)
                .Select(i => new Employee
                {
                    Name = $"Seller Name: {i}",
                    Surname = $"Seller Surname: {i}",
                    Patronymic = $"Seller Patronymic {i}",
                    DeteOfBirth = DateTime.Now,
                    ContactNumber = random.Next(100000000, 999999999).ToString(),
                    ContactMail = $"seller{i}@gmail.com",
                    IndeidentityDocumentNumber = Guid.NewGuid().ToString(),
                    WorkingRate = "1/2"
                }).ToArray();

            await _dbContext.Employees.AddRangeAsync(_sellers);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _sellersRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _transactionsCount = 3500;

        public async Task InitializeTransactions()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize transactions...");

            if (_books is null || _sellers is null || _buyers is null) throw new ArgumentNullException("Field _booksRepository, _sellersRepository or _buyersRepository can`t be empty");

            var random = new Random();

            var transactions = Enumerable.Range(1, _transactionsCount)
                .Select(d => new Order
                {
                    TransactionDate = DateTime.UtcNow,
                    Book = random.NextItem(_books),
                    Seller = random.NextItem(_sellers),
                    Buyer = random.NextItem(_buyers),
                    Discount = random.Next(50),
                    Amount = (decimal)(random.NextDouble() * 300 + 50)
                });

            await _dbContext.Orders.AddRangeAsync(transactions);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize transactions comleted in {timer.Elapsed.TotalSeconds} s");
        }
    }
}
