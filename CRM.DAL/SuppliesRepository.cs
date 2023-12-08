using CRM.DAL.Context;
using CRM.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    internal class SuppliesRepository : DbRepository<Supply>
    {
        public override IQueryable<Supply>? Entities => base.Entities?
            .Include(entity => entity.SupplyDetails)
            .Include(entity => entity.Supplier);

        public SuppliesRepository(CRMDbContext context) : base(context) { }
    }
}
