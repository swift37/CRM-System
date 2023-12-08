using CRM.DAL.Context;
using CRM.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL
{
    internal class OrdersRepository : DbRepository<Order>
    {
        public override IQueryable<Order>? Entities => base.Entities?
            .Include(entity => entity.OrderDetails)
            .Include(entity => entity.Employee)
            .Include(entity => entity.Customer)
            .Include(entity => entity.ShipVia);

        public OrdersRepository(CRMDbContext context) : base(context) { }
    }
}
