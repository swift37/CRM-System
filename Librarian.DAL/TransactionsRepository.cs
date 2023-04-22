using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class TransactionsRepository : DbRepository<Transaction>
    {
        public override IQueryable<Transaction>? Entities => base.Entities?
            .Include(entity => entity.Book)
            .Include(entity => entity.Seller)
            .Include(entity => entity.Buyer);

        public TransactionsRepository(LibrarianDb context) : base(context) { }
    }
}
