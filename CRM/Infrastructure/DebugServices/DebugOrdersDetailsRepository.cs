using CRM.DAL.Entities;
using CRM.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.DebugServices
{
    public class DebugOrdersDetailsRepository : IRepository<OrderDetails>
    {
        public DebugOrdersDetailsRepository()
        {
            var _productsRepository = new DebugProductsRepository();
            var _ordersRepository = new DebugOrdersRepository();
            if(_productsRepository.Entities is null || _ordersRepository.Entities is null) throw new ArgumentNullException("Error defining entity collection in one of the debug repositories");

            var random = new Random();

            var products = Enumerable.Range(1, 100).Select(d => random.NextItem(_productsRepository.Entities.ToArray())).ToArray();
            var orders = _ordersRepository.Entities.ToArray();

            Entities = Enumerable.Range(1, 100)
                .Select(i => new OrderDetails
                {
                    Quantity = random.Next(10),
                    UnitPrice = products[i - 1].UnitPrice,
                    Product = products[i - 1],
                    Order = orders[i - 1],
                    Discount = random.Next(50)
                }).AsQueryable();
        }

        public IQueryable<OrderDetails>? Entities { get; set; }
        public bool AutoSaveChanges { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public OrderDetails? Add(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails?>? AddAsync(OrderDetails entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void Archive(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task ArchiveAsync(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public OrderDetails? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UnArchive(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task UnArchiveAsync(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderDetails entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
