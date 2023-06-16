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

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.DateOfBirth)
                .HasColumnType("date");

                entity.Property(e => e.HireDate)
                .HasColumnType("date");

                entity.Property(e => e.Extension)
                .HasColumnType("date");
            });

            modelBuilder.Entity<Order>()
               .Property(e => e.ShippingCost)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.HasOne(o => o.Order)
                .WithMany(d => d.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(o => o.Product)
                .WithMany(d => d.OrderDetails)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Discount)
                .HasColumnType("decimal(18,2)");

                entity
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)");
            });
        }

        public LibrarianDb(DbContextOptions<LibrarianDb> options) : base(options)
        {
        }
    }
}
