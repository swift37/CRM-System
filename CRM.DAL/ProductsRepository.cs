using CRM.DAL.Context;
using CRM.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL
{
    internal class ProductsRepository : DbRepository<Product>
    {
        public override IQueryable<Product>? Entities => base.Entities?
            .Include(entity => entity.Category)
            .Include(entity => entity.Supplier);

        public ProductsRepository(CRMDbContext context) : base(context) { }
    }
}
