using Librarian.DAL.Entities;
using Librarian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.DebugServices
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

        public WorkingRate? Add(WorkingRate entity)
        {
            throw new NotImplementedException();
        }

        public Task<WorkingRate?>? AddAsync(WorkingRate entity, CancellationToken cancellation = default)
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
