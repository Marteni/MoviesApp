using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL;
using MoviesApp.DAL.Factories;

namespace MoviesApp.BL.Tests
{
    public class InMemoryDbContextFactory : IDbContextSqlFactory
    {
        public MoviesAppDbContext CreateAppDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseInMemoryDatabase("MoviesAppDB");
            return new MoviesAppDbContext(optionsBuilder.Options);
        }

    }
}
