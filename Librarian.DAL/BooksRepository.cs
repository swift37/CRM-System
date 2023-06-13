using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class BooksRepository : DbRepository<Product>
    {
        public override IQueryable<Product>? Entities => base.Entities?.Include(entity => entity.Category);

        public BooksRepository(LibrarianDb context) : base(context) { }
    }
}
