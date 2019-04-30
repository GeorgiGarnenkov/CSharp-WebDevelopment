using PANDAWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace PANDAWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
                
        public virtual DbSet<Package> Packages { get; set; }
                
        public virtual DbSet<Receipt> Receipts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Panda;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Receipt)
                .WithOne(i => i.Package)
                .HasForeignKey<Receipt>(b => b.PackageId);

            modelBuilder.Entity<Receipt>()
                .HasOne(x => x.Recipient)
                .WithMany(x => x.Receipts)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}