using CRM.DAL.Context;
using CRM.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL
{
    internal class EmployeesRepository : DbRepository<Employee>
    {
        public override IQueryable<Employee>? Entities => base.Entities?
            .Include(entity => entity.WorkingRate);

        public EmployeesRepository(CRMDbContext context) : base(context) { }
    }
}
