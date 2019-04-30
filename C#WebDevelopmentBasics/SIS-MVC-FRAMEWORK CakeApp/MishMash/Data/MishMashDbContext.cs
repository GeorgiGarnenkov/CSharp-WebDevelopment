using Microsoft.EntityFrameworkCore;
using MishMash.Models;

namespace MishMash.Data
{
    public class MishMashDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserChannel> UserChannels { get; set; }

        public MishMashDbContext()
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MishMash;Integrated Security=True;");
        }
    }           
}