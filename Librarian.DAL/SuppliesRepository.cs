using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.DAL
{
    internal class SuppliesRepository : DbRepository<Supply>
    {
        public override IQueryable<Supply>? Entities => base.Entities?
            .Include(entity => entity.SupplyDetails)
            .Include(entity => entity.Supplier);

        public SuppliesRepository(LibrarianDb context) : base(context) { }
    }
}
