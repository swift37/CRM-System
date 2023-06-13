using Librarian.DAL.Context;
using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class ProductsRepository : DbRepository<Product>
    {
        public override IQueryable<Product>? Entities => base.Entities?
            .Include(entity => entity.Category)
            .Include(entity => entity.Supplier);

        public ProductsRepository(LibrarianDb context) : base(context) { }
    }
}
