using Librarian.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL.Context
{
    public class LibrarianDb : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<WorkingRate> WorkingRates { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrdersDetails { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Shipper> Shippers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Customer>()
                .Property(c => c.CashbackBalance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Employee>()
                .Property(e => e.DateOfBirth)
                .HasColumnType("date");

            modelBuilder.Entity<Employee>()
                .Property(e => e.HireDate)
                .HasColumnType("date");

            modelBuilder.Entity<Employee>()
               .Property(e => e.Extension)
               .HasColumnType("date");

            modelBuilder.Entity<Order>()
                .HasOne(d => d.OrderDetails)
                .WithOne(o => o.Order)
                .HasForeignKey<OrderDetails>(d => d.Id);

            modelBuilder.Entity<Order>()
               .Property(e => e.ShippingCost)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>()
               .Property(e => e.UnitPrice)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>()
               .Property(e => e.Discount)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>()
               .Property(e => e.Amount)
               .HasColumnType("decimal(18,2)");
        }

        public LibrarianDb(DbContextOptions<LibrarianDb> options) : base(options)
        {
        }
    }
}
