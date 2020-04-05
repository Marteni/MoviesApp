using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoviesApp.DAL;

namespace MoviesApp.BL.Factories
{
    public class DbContextSqlFactory : IDbContextSqlFactory
    {
        public MoviesAppDbContext CreateAppDbContext()
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
            var connectionString = configuration.GetValue<string>("connectionString");
            
            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new MoviesAppDbContext(optionsBuilder.Options);
        }
    }
}
