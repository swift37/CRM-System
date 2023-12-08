using CRM.DAL.Entities;
using CRM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.DebugServices
{
    public class DebugSuppliesDetailsRepository : IRepository<SupplyDetails>
    {
        public DebugSuppliesDetailsRepository()
        {
            var _productsRepository = new DebugProductsRepository();
            var _suppliesRepository = new DebugSuppliesRepository();

            if (_productsRepository.Entities is null || _suppliesRepository.Entities is null) 
                throw new ArgumentNullException("Error defining entity collection in one of the debug repositories");

            var random = new Random();

            var products = Enumerable.Range(1, 100).Select(d => random.NextItem(_productsRepository.Entities.ToArray())).ToArray();
            var supplies = _suppliesRepository.Entities.ToArray();

            Entities = Enumerable.Range(1, 100)
                .Select(i => new SupplyDetails
                {
                    Quantity = random.Next(10),
                    UnitPrice = (decimal)(random.NextDouble() * 300 + 35),
                    Product = products[i - 1],
                    Supply = supplies[i - 1],
                }).AsQueryable();
        }

        public bool AutoSaveChanges { get; set; }

        public IQueryable<SupplyDetails>? Entities { get; set; }

        public SupplyDetails? Add(SupplyDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task<SupplyDetails?>? AddAsync(SupplyDetails entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void Archive(SupplyDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task ArchiveAsync(SupplyDetails entity)
        {
            throw new NotImplementedException();
        }

        public SupplyDetails? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SupplyDetails?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void UnArchive(SupplyDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task UnArchiveAsync(SupplyDetails entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SupplyDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SupplyDetails entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
