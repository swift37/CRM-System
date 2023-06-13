using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class EmployeesRepository : DbRepository<Employee>
    {
        public override IQueryable<Employee>? Entities => base.Entities?
            .Include(entity => entity.WorkingRate);

        public EmployeesRepository(LibrarianDb context) : base(context) { }
    }
}
