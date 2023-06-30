using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class SuppliesDetailsRepository : DbRepository<SupplyDetails>
    {
        public override IQueryable<SupplyDetails>? Entities => base.Entities?
            .Include(entity => entity.Supply)
            .Include(entity => entity.Product);

        public SuppliesDetailsRepository(LibrarianDb context) : base(context) { }
    }
}
