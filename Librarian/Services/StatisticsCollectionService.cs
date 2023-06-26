using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services.Interfaces;
using LiveCharts.Helpers;
using System;
using System.Linq;
using static Librarian.Services.Interfaces.IStatisticsCollectionService;

namespace Librarian.Services
{
    public class StatisticsCollectionService : IStatisticsCollectionService
    {
        public GlobalStatistics CollectGlobalStatistics(
            IRepository<Order> ordersRepository, 
            TimePeriod period)
        {
            if (ordersRepository.Entities is null)
                throw new ArgumentNullException(nameof(ordersRepository.Entities));

            var orders = ordersRepository.Entities;
            var glStats = new GlobalStatistics();
            double daysCount = (double)period;

            var yearSalesStatistics = Enumerable.Range(1, 12).Select(i => new DataPoint
            {
                Date = DateTime.Now.AddDays(-daysCount / 12 * i - daysCount / 12),
                Value = orders.Where(o =>
                o.OrderDate < DateTime.Now.AddDays(-daysCount / 12 * i - daysCount / 12) && 
                o.OrderDate > DateTime.Now.AddDays(-daysCount / 12 * i))
                .Sum(o => o.ProductsQuantity)
            });

            var timePeriodOrders = orders.Where(o =>
                o.OrderDate < DateTime.Now && o.OrderDate > DateTime.Now.AddDays(daysCount));

            glStats.Income = timePeriodOrders.Sum(o => o.Amount);
            glStats.AverageOrderAmount = timePeriodOrders.Average(o => o.Amount);
            glStats.OrdersCount = timePeriodOrders.Count();

            return glStats;
        }

        public StatisticsDetails CollectProductStatistics(
            Product? product, 
            IRepository<OrderDetails> ordersDetailsRepository)
        {
            if (product is null) 
                throw new ArgumentNullException(nameof(product));
            if (ordersDetailsRepository.Entities is null) 
                throw new ArgumentNullException(nameof(ordersDetailsRepository.Entities));
            
            var statisticsDetails = new StatisticsDetails();

            var productOrderDetailsQuery = ordersDetailsRepository.Entities.Where(od => od.ProductId == product.Id);

            var yearSalesStatistics = Enumerable.Range(1, 12).Select(i => new DataPoint
            {
                Date = DateTime.Today.AddMonths(-i),
                Value = productOrderDetailsQuery.Where(d => d.Order != null &&
                d.Order.OrderDate.Month == DateTime.Today.AddMonths(-i).Month &&
                d.Order.OrderDate.Year == DateTime.Today.AddMonths(-i).Year)
                .Sum(d => d.Quantity)
            });

            var averageSales = yearSalesStatistics.Select(dp => dp.Value).Average();

            statisticsDetails.Values = yearSalesStatistics.AsChartValues();
            statisticsDetails.AverageSalesPerMonth = averageSales;
            statisticsDetails.AverageMonthlyIncome = (decimal)averageSales * product.UnitPrice;
            statisticsDetails.LastMonthProfit = yearSalesStatistics
                .FirstOrDefault(dp => dp.Date == DateTime.Today.AddMonths(-1)).Value / (averageSales) - 1;
           
            return statisticsDetails;
        }

        public StatisticsDetails CollectCategoryStatistics(
            Category? category,
            IRepository<OrderDetails> ordersDetailsRepository)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category));
            if (ordersDetailsRepository.Entities is null)
                throw new ArgumentNullException(nameof(ordersDetailsRepository.Entities));

            var statisticsDetails = new StatisticsDetails();

            var categoryOrderDetailsQuery = ordersDetailsRepository.Entities
                .Where(od => od.Product != null && od.Product.CategoryId == category.Id);

            var yearSalesStatistics = Enumerable.Range(1, 12).Select(i => new DataPoint
            {
                Date = DateTime.Today.AddMonths(-i),
                Value = categoryOrderDetailsQuery.Where(d => d.Order != null &&
                d.Order.OrderDate.Month == DateTime.Today.AddMonths(-i).Month &&
                d.Order.OrderDate.Year == DateTime.Today.AddMonths(-i).Year)
                .Sum(d => d.Quantity)
            });

            var averageSales = yearSalesStatistics.Select(dp => dp.Value).Average();

            statisticsDetails.Values = yearSalesStatistics.AsChartValues();

            statisticsDetails.AverageSalesPerMonth = averageSales;

            statisticsDetails.AverageMonthlyIncome = Enumerable.Range(1, 12).Select(i =>
                categoryOrderDetailsQuery.Where(d => d.Order != null &&
                d.Order.OrderDate.Month == DateTime.Today.AddMonths(-i).Month &&
                d.Order.OrderDate.Year == DateTime.Today.AddMonths(-i).Year)
                .Sum(d => d.Quantity * d.UnitPrice)).Average();

            statisticsDetails.LastMonthProfit = yearSalesStatistics
                .FirstOrDefault(dp => dp.Date == DateTime.Today.AddMonths(-1)).Value / (averageSales) - 1;

            return statisticsDetails;
        }
        
        public StatisticsDetails CollectEmployeeStatistics(
            Employee? employee,
            IRepository<Order> ordersRepository)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));
            if (ordersRepository.Entities is null)
                throw new ArgumentNullException(nameof(ordersRepository.Entities));

            var statisticsDetails = new StatisticsDetails();

            var employeOrdersQuery = ordersRepository.Entities.Where(o => o.EmployeeId == employee.Id);

            var yearSalesStatistics = Enumerable.Range(1, 12).Select(i => new DataPoint
            {
                Date = DateTime.Today.AddMonths(-i),
                Value = employeOrdersQuery.Where(o =>
                o.OrderDate.Month == DateTime.Today.AddMonths(-i).Month &&
                o.OrderDate.Year == DateTime.Today.AddMonths(-i).Year)
                .Sum(o => o.ProductsQuantity)
            });

            var averageSales = yearSalesStatistics.Select(dp => dp.Value).Average();

            statisticsDetails.Values = yearSalesStatistics.AsChartValues();

            statisticsDetails.AverageSalesPerMonth = averageSales;

            statisticsDetails.AverageMonthlyIncome = Enumerable.Range(1, 12).Select(i =>
                employeOrdersQuery.Where(o =>
                o.OrderDate.Month == DateTime.Today.AddMonths(-i).Month &&
                o.OrderDate.Year == DateTime.Today.AddMonths(-i).Year)
                .Sum(o => o.Amount)).Average();

            statisticsDetails.LastMonthProfit = yearSalesStatistics
                .FirstOrDefault(dp => dp.Date == DateTime.Today.AddMonths(-1)).Value / (averageSales) - 1;

            return statisticsDetails;
        }
    }
}
