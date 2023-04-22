using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class BooksRepository : DbRepository<Book>
    {
        public override IQueryable<Book>? Entities => base.Entities?.Include(entity => entity.Category);

        public BooksRepository(LibrarianDb context) : base(context) { }
    }
}
