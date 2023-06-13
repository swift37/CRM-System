using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class OrdersDetailsRepository : DbRepository<OrderDetails>
    {
        public override IQueryable<OrderDetails>? Entities => base.Entities?
            .Include(entity => entity.Order)
            .Include(entity => entity.Product);

        public OrdersDetailsRepository(LibrarianDb context) : base(context) { }
    }
}
