using Librarian.DAL.Entities;
using Librarian.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.DebugServices
{
    class DebugEmployeesRepository : IRepository<Employee>
    {
        public DebugEmployeesRepository()
        {
            var random = new Random();
            var workingRates = Enumerable.Range(1, 4)
                .Select(i => new WorkingRate
                {
                    Name = $"{i}/4",
                    HoursPerMonth = i * 42,
                    Description = "Test Desc"
                }).ToArray();

            Entities = Enumerable.Range(1,15)
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
                    WorkingRate = random.NextItem(workingRates),
                    Address = $"USA, New York, Test st."
                }).AsQueryable();
        }

        public IQueryable<Employee>? Entities { get; }

        public Employee? Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?>? AddAsync(Employee entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Employee? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?>? GetAsync(int id, CancellationToken cancellation = default)
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

        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Employee entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
