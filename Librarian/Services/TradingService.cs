using Librarian.DAL.Entities;
using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;
using Librarian.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.Services
{
    public class TradingService : ITradingService
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Order> _orders;

        public IEnumerable<Order>? Orders => _orders.Entities;

        public TradingService(IRepository<Product> products, IRepository<Order> orders)
        {
            _products = products;
            _orders = orders;
        }

        public async Task<Order?> CrateOrderAsync(string productName, Employee employee, Customer customer, decimal orderАmount)
        {
            if (_products.Entities is null) throw new ArgumentNullException(nameof(_products.Entities));
 
            var product = await _products.Entities.FirstOrDefaultAsync(product => product.Name == productName).ConfigureAwait(false);
            if (product is null) return null;

            var order = new Order
            {
                //Book = book,
                //Seller = seller,
                //Buyer = buyer,
                //Amount = transactionАmount
            };

            return await _orders.AddAsync(order);
        }
    }
}
