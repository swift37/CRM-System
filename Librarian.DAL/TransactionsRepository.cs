using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class TransactionsRepository : DbRepository<Order>
    {
        public override IQueryable<Order>? Entities => base.Entities?
            .Include(entity => entity.Book)
            .Include(entity => entity.Seller)
            .Include(entity => entity.Buyer);

        public TransactionsRepository(LibrarianDb context) : base(context) { }
    }
}
