using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MoviesApp.DAL.Factories
{
    public class DbContextSqlFactory : IDbContextFactory
    {
        public MoviesAppDbContext CreateDbContext()
        {
           
            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = MoviesAppDb;MultipleActiveResultSets = True;Integrated Security = True;");
            var dbContext = new MoviesAppDbContext(optionsBuilder.Options);
            dbContext.Database.Migrate();
            return dbContext;
        }

    }
}
