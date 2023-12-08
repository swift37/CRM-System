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
    public class DebugSuppliesRepository : IRepository<Supply>
    {
        public DebugSuppliesRepository()
        {
            var suppliersRepository = new DebugSuppliersRepository();

            if (suppliersRepository.Entities is null)
                throw new ArgumentNullException(nameof(suppliersRepository.Entities));

            var random = new Random();

            var suppliers = suppliersRepository.Entities.ToArray();

            Entities = Enumerable.Range(1, 100)
                .Select(i => new Supply
                {
                    SupplyDate = DateTime.Now.AddDays(-random.Next(365)),
                    SupplyCost = random.Next(5, 1500),
                    Supplier = random.NextItem(suppliers),
                    ProductsQuantity = random.Next(30, 150)
                }).AsQueryable();
        }

        public bool AutoSaveChanges { get; set; }

        public IQueryable<Supply>? Entities { get; set; }

        public Supply? Add(Supply entity)
        {
            throw new NotImplementedException();
        }

        public Task<Supply?>? AddAsync(Supply entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void Archive(Supply entity)
        {
            throw new NotImplementedException();
        }

        public Task ArchiveAsync(Supply entity)
        {
            throw new NotImplementedException();
        }

        public Supply? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Supply?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void UnArchive(Supply entity)
        {
            throw new NotImplementedException();
        }

        public Task UnArchiveAsync(Supply entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Supply entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Supply entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
