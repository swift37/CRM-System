using CRM.DAL.Context;
using CRM.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL
{
    internal class OrdersDetailsRepository : DbRepository<OrderDetails>
    {
        public override IQueryable<OrderDetails>? Entities => base.Entities?
            .Include(entity => entity.Order)
            .Include(entity => entity.Product);

        public OrdersDetailsRepository(CRMDbContext context) : base(context) { }
    }
}
