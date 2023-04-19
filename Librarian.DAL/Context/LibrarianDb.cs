using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.DAL.Context
{
    public class LibrarianDb : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Deal> Deals { get; set; }

        public LibrarianDb(DbContextOptions<LibrarianDb> options) : base(options) 
        { 
        }
    }
}
