using Chushka.Data.EntityConfigurations;
using Chushka.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chushka.Data
{
    public class ChushkaDbContext : IdentityDbContext<User>
    {
        public ChushkaDbContext()
        {}
        public ChushkaDbContext(DbContextOptions<ChushkaDbContext> options)
            : base(options)
        {}
        
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ProductConfiguration())
                .ApplyConfiguration(new OrderConfiguration())
                .ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
