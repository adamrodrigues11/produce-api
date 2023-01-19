using Microsoft.EntityFrameworkCore;

namespace Day02Exercises.Models
{
    public class ProduceDBContext : DbContext
    {
        public ProduceDBContext(DbContextOptions<ProduceDBContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite primary keys.
            modelBuilder.Entity<ProduceSupplier>()
                .HasKey(ps => new { ps.ProduceID, ps.SupplierID });

            // Define foreign keys here. Do not use foreign key annotations.
            modelBuilder.Entity<ProduceSupplier>()
                .HasOne<Produce>(ps => ps.Produce)
                .WithMany(p => p.ProduceSuppliers)
                .HasForeignKey(ps => new { ps.ProduceID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<ProduceSupplier>()
                .HasOne<Supplier>(ps => ps.Supplier)
                .WithMany(s => s.ProduceSuppliers)
                .HasForeignKey(ps => new { ps.SupplierID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Produce>().HasData(
                new Produce { ProduceID = 1, Description = "Oranges" },
                new Produce { ProduceID = 2, Description = "Apples" },
                new Produce { ProduceID = 3, Description = "Peaches" });

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierID = 1, SupplierName = "Kin's Market" },
                new Supplier { SupplierID = 2, SupplierName = "Fresh Street Market" });

            modelBuilder.Entity<ProduceSupplier>().HasData(
                new ProduceSupplier { SupplierID = 1, ProduceID = 1, Qty = 25 },
                new ProduceSupplier { SupplierID = 2, ProduceID = 2, Qty = 12 },
                new ProduceSupplier { SupplierID = 1, ProduceID = 3, Qty = 30 });
        }

        // Define entity collections.
        public DbSet<Produce> Produces { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProduceSupplier> ProduceSuppliers { get; set; }

    }
}
