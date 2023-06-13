using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class OrdersRepository : DbRepository<Order>
    {
        public override IQueryable<Order>? Entities => base.Entities?
            .Include(entity => entity.OrderDetails)
            .Include(entity => entity.Employee)
            .Include(entity => entity.Customer)
            .Include(entity => entity.ShipVia);

        public OrdersRepository(LibrarianDb context) : base(context) { }
    }
}
