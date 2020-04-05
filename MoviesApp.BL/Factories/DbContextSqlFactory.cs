using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL;

namespace MoviesApp.BL.Factories
{
    public class DbContextSqlFactory : IDbContextSqlFactory
    {
        public MoviesAppDbContext CreateAppDbContext()
        {

            // var configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.Build();
            // var connectionString = configuration.GetValue<string>("connectionString");
            //var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = TasksDB;MultipleActiveResultSets = True;Integrated Security = True; ";

            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = MoviesAppDb;MultipleActiveResultSets = True;Integrated Security = True; ");
            return new MoviesAppDbContext(optionsBuilder.Options);
        }

      
    }
}
