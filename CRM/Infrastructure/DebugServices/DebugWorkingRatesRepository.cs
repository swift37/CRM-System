using CRM.DAL.Entities;
using CRM.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.DebugServices
{
    public class DebugWorkingRatesRepository : IRepository<WorkingRate>
    {
        public DebugWorkingRatesRepository()
        {
            Entities = Enumerable.Range(1, 4)
                .Select(i => new WorkingRate
                {
                    Name = $"{i}/4",
                    HoursPerMonth = i * 42,
                    Description = "Test Desc"
                }).AsQueryable();
        }

        public IQueryable<WorkingRate>? Entities { get; set; }
        public bool AutoSaveChanges { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WorkingRate? Add(WorkingRate entity)
        {
            throw new NotImplementedException();
        }

        public Task<WorkingRate?>? AddAsync(WorkingRate entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void Archive(WorkingRate entity)
        {
            throw new NotImplementedException();
        }

        public Task ArchiveAsync(WorkingRate entity)
        {
            throw new NotImplementedException();
        }

        public WorkingRate? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WorkingRate?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void UnArchive(WorkingRate entity)
        {
            throw new NotImplementedException();
        }

        public Task UnArchiveAsync(WorkingRate entity)
        {
            throw new NotImplementedException();
        }

        public void Update(WorkingRate entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WorkingRate entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
