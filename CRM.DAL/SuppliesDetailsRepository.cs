using CRM.DAL.Context;
using CRM.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL
{
    internal class SuppliesDetailsRepository : DbRepository<SupplyDetails>
    {
        public override IQueryable<SupplyDetails>? Entities => base.Entities?
            .Include(entity => entity.Supply)
            .Include(entity => entity.Product);

        public SuppliesDetailsRepository(CRMDbContext context) : base(context) { }
    }
}
