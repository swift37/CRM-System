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
            await InitializeSuppliers();
            await InitializeProducts();
            await InitializeWorkingRates();
            await InitializeEmployees();
            await InitializeCustomers();
            await InitializeShippers();
            await InitializeOrders();
            await InitializeOrdersDetails();

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

        private const int _suppliersCount = 10;

        private Supplier[]? _suppliers;

        private async Task InitializeSuppliers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _suppliersRepository...");

            var random = new Random();
            _suppliers = Enumerable.Range(1, _suppliersCount)
                .Select(i => new Supplier
                {
                    Name = $"Company {i}",
                    ContactName = $"Supplier {i}",
                    ContactTitle = "Manager",
                    ContactMail = $"company{i}@gmail.com",
                    ContactNumber = random.Next(100000000, 999999999).ToString(),
                    Address = $"USA, New York, Test st."
                }).ToArray();

            await _dbContext.Suppliers.AddRangeAsync(_suppliers);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _suppliersRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _productsCount = 1500;

        private Product[]? _products;

        private async Task InitializeProducts()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _productsRepository...");

            if (_categories is null) throw new ArgumentNullException(nameof(_categories));
            if (_suppliers is null) throw new ArgumentNullException(nameof(_suppliers));
            var random = new Random();
            _products = Enumerable.Range(1, _productsCount)
                .Select(i => new Product
                {
                    Name = $"Product {i}",
                    UnitsInEnterprise = random.Next(100),
                    UnitsInStock = random.Next(1000),
                    Supplier = random.NextItem(_suppliers),
                    UnitPrice = (decimal)(random.NextDouble() * 300 + 50),
                    Category = random.NextItem(_categories)
                }).ToArray();

            await _dbContext.Products.AddRangeAsync(_products);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _productsRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _customersCount = 500;

        private Customer[]? _customers;

        private async Task InitializeCustomers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _customersRepository...");

            var random = new Random();
            _customers = Enumerable.Range(1, _customersCount)
                .Select(i => new Customer
                {
                    Name = $"Customer {i}",
                    Surname = "Surnm",
                    ContactName = $"Tester",
                    ContactTitle = $"Manager",
                    Address = $"USA, New York, Test st.",
                    ContactNumber = random.Next(100000000, 999999999).ToString(),
                    ContactMail = $"customer{i}@gmail.com",
                    CashbackBalance = (decimal)(random.NextDouble() * 100)
                }).ToArray();

            await _dbContext.Customers.AddRangeAsync(_customers);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _customersRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private WorkingRate[]? _workingRates;

        private async Task InitializeWorkingRates()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _workingRatesRepository...");

            var random = new Random();
            _workingRates = Enumerable.Range(1, 4)
                .Select(i => new WorkingRate
                {
                    Name = $"{i}/4",
                    HoursPerMonth = i * 42,
                    Description = "Test Desc"
                }).ToArray();

            await _dbContext.WorkingRates.AddRangeAsync(_workingRates);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _workingRatesRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _employeesCount = 25;

        private Employee[]? _employees;

        private async Task InitializeEmployees()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _employeesRepository...");

            if (_workingRates is null) throw new ArgumentNullException(nameof(_workingRates));
            var random = new Random();
            _employees = Enumerable.Range(1, _employeesCount)
                .Select(i => new Employee
                {
                    Name = $"Employee {i}",
                    Surname = $"Surnm",
                    DateOfBirth = DateTime.Now.AddYears(-random.Next(18, 45)),
                    HireDate = DateTime.Now.AddYears(-random.Next(1, 10)),
                    Title = "Tester",
                    Extension = DateTime.Now.AddYears(random.Next(3, 15)),
                    ContactNumber = random.Next(100000000, 999999999).ToString(),
                    ContactMail = $"seller{i}@gmail.com",
                    IdentityDocumentNumber = Guid.NewGuid().ToString(),
                    WorkingRate = random.NextItem(_workingRates),
                    Address = $"USA, New York, Test st."
                }).ToArray();

            await _dbContext.Employees.AddRangeAsync(_employees);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _employeesRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _shippersCount = 7;

        private Shipper[]? _shippers;

        private async Task InitializeShippers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize _shippersRepository...");

            var random = new Random();
            _shippers = Enumerable.Range(1, _shippersCount)
                .Select(i => new Shipper
                {
                    Name = $"Shipper {i}",
                    ContactNumber = random.Next(100000000, 999999999).ToString()
                }).ToArray();

            await _dbContext.Shippers.AddRangeAsync(_shippers);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize _shippersRepository comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _ordersCount = 5000;

        private Order[]? _orders;

        public async Task InitializeOrders()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize orders...");

            if (_products is null || _employees is null || _customers is null) throw new ArgumentNullException("Field _productsRepository, _employeesRepository or _customersRepository can`t be empty");

            if (_shippers is null) throw new ArgumentNullException(nameof(_shippers));
            var random = new Random();

            var dates = Enumerable.Range(1, _ordersCount).Select(d => DateTime.Now.AddDays(-random.Next(365))).ToArray();

            _orders = Enumerable.Range(1, _ordersCount)
                .Select(i => new Order
                {
                    OrderDate = dates[i - 1],
                    ShippedDate = dates[i - 1].AddDays(random.Next(5)),
                    RequiredDate = dates[i - 1].AddDays(random.Next(5, 5)),
                    Employee = random.NextItem(_employees),
                    Customer = random.NextItem(_customers),
                    Amount = random.Next(5,1500),
                    ProductsQuantity = random.Next(1,15),
                    ShipName = $"Name {i}",
                    ShipAddress = $"USA, New York, Test st.",
                    ShippingCost = (decimal)(random.NextDouble() * 300 + 50),
                    ShipVia = random.NextItem(_shippers),
                }).ToArray();

            await _dbContext.Orders.AddRangeAsync(_orders);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize orders comleted in {timer.Elapsed.TotalSeconds} s");
        }

        private const int _ordersDeatailsCount = _ordersCount;

        public async Task InitializeOrdersDetails()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialize ordersDeatails...");

            if (_orders is null || _products is null || _employees is null || _customers is null) throw new ArgumentNullException("Field _ordersRepository, _productsRepository, _employeesRepository or _customersRepository can`t be empty");

            var random = new Random();

            var products = Enumerable.Range(1, _ordersCount).Select(d => random.NextItem(_products)).ToArray();

            var ordersDeatails = Enumerable.Range(1, _ordersDeatailsCount)
                .Select(i => new OrderDetails
                {
                    Quantity = random.Next(10),
                    UnitPrice = products[i - 1].UnitPrice,
                    Product = products[i - 1],
                    Order = _orders[i - 1],
                    Discount = random.Next(50)
                });

            await _dbContext.OrdersDetails.AddRangeAsync(ordersDeatails);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Initialize ordersDeatails comleted in {timer.Elapsed.TotalSeconds} s");
        }
    }
}
