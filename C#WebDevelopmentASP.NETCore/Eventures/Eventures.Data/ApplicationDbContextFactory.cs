using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Eventures.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<EventuresDbContext>
    {
        public EventuresDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EventuresDbContext>();

            builder.UseSqlServer("Server=.\\SQLEXPRESS;" +
                                 "Database=Eventures;" +
                                 "Trusted_Connection=True;" +
                                 "MultipleActiveResultSets=true",
                                 optionsBuilder => 
                                                 optionsBuilder.MigrationsAssembly(typeof(EventuresDbContext)
                                                               .GetTypeInfo().Assembly.GetName().Name));

            return new EventuresDbContext(builder.Options);
        }
    }
}