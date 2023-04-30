using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL.Context
{
    public class LibrarianDb : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public LibrarianDb(DbContextOptions<LibrarianDb> options) : base(options) 
        { 
        }
    }
}
