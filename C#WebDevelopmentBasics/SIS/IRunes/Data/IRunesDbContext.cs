using IRunes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunes.Data
{
    public class IRunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=GEORGI\\SQLEXPRESS;" +
                                        "Database=IRunes;" +
                                        "Integrated Security=True;");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}