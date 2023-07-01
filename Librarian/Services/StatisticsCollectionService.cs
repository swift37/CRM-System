using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services.Interfaces;
using LiveCharts;
using LiveCharts.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            double hoursCount = (double)period * 24;
            
            glStats.Values = Enumerable.Range(1, 12).Select(i => new DataPoint
            {
                Date = DateTime.Now.AddHours(-hoursCount / 12 * (13 - i) + hoursCount / 12),
                Value = orders.Where(o =>
                o.OrderDate < DateTime.Now.AddHours(-hoursCount / 12 * (13 - i) + hoursCount / 12) && 
                o.OrderDate > DateTime.Now.AddHours(-hoursCount / 12 * (13 - i)))
                .Sum(o => o.ProductsQuantity)
            }).AsChartValues();

            var timePeriodOrders = orders.Where(o =>
                o.OrderDate < DateTime.Now && o.OrderDate > DateTime.Now.AddHours(-hoursCount));
 
            glStats.Income = timePeriodOrders.Sum(o => o.Amount);
            glStats.AverageOrderAmount = timePeriodOrders.Average(o => o.Amount);
            glStats.OrdersCount = timePeriodOrders.Count();

            return glStats;
        }

        public async Task<GlobalStatistics> CollectGlobalStatisticsAsync(
            IRepository<Order> ordersRepository,
            TimePeriod period)
        {
            if (ordersRepository.Entities is null)
                throw new ArgumentNullException(nameof(ordersRepository.Entities));

            var glStats = new GlobalStatistics();
            glStats.Values = new ChartValues<DataPoint>();
            int hoursCount = (int)period * 24;

            await foreach (var item in GetTimePeriodStatistic(ordersRepository, hoursCount))
            {
                glStats.Values.Add(item);
            }

            var timePeriodOrders = ordersRepository.Entities.Where(o =>
                o.OrderDate < DateTime.Now && o.OrderDate > DateTime.Now.AddHours(-hoursCount));
            
            glStats.Income = await timePeriodOrders.SumAsync(o => o.Amount);
            glStats.AverageOrderAmount = await timePeriodOrders.AverageAsync(o => o.Amount);
            glStats.OrdersCount = await timePeriodOrders.CountAsync();

            return glStats;
        }

        private async IAsyncEnumerable<DataPoint> GetTimePeriodStatistic(
            IRepository<Order> ordersRepository,
            int hoursCount)
        {
            if (ordersRepository.Entities is null)
                throw new ArgumentNullException(nameof(ordersRepository.Entities));

            for (int i = 12; i > 0; i--)
            {
                yield return new DataPoint
                {
                    Date = DateTime.Now.AddHours(-hoursCount / 12 * i),
                    Value = await ordersRepository.Entities.Where(o =>
                    o.OrderDate < DateTime.Now.AddHours(-hoursCount / 12 * i) &&
                    o.OrderDate > DateTime.Now.AddHours(-hoursCount / 12 * i - hoursCount / 12))
                .SumAsync(o => o.ProductsQuantity)
                };
            }
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
                Date = DateTime.Today.AddMonths(-13 + i),
                Value = productOrderDetailsQuery.Where(d => d.Order != null &&
                d.Order.OrderDate.Month == DateTime.Today.AddMonths(-13 + i).Month &&
                d.Order.OrderDate.Year == DateTime.Today.AddMonths(-13 + i).Year)
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
                Date = DateTime.Today.AddMonths(-13 + i),
                Value = categoryOrderDetailsQuery.Where(d => d.Order != null &&
                d.Order.OrderDate.Month == DateTime.Today.AddMonths(-13 + i).Month &&
                d.Order.OrderDate.Year == DateTime.Today.AddMonths(-13 + i).Year)
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
                Date = DateTime.Today.AddMonths(-13 + i),
                Value = employeOrdersQuery.Where(o =>
                o.OrderDate.Month == DateTime.Today.AddMonths(-13 + i).Month &&
                o.OrderDate.Year == DateTime.Today.AddMonths(-13 + i).Year)
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
