using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace MoviesApp.DAL
{
    public class DesignTimeMoviesAppDbContext: IDesignTimeDbContextFactory<MoviesAppDbContext>
    {
      
        public MoviesAppDbContext CreateDbContext(string[] args)
        {

            
            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = MoviesAppDb;MultipleActiveResultSets = True;Integrated Security = True;");
            return new MoviesAppDbContext(optionsBuilder.Options);
        }
    }
}
