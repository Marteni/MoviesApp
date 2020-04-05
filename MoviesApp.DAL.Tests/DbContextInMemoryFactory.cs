using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL.Factories;

namespace MoviesApp.DAL.Tests
{
    public class DbContextInMemoryFactory : IDbContextFactory
    {
        private readonly string _databaseName;

        public DbContextInMemoryFactory(string databaseName)
        {
            _databaseName = databaseName;
        }
        public MoviesAppDbContext Create()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase(_databaseName);
            return new MoviesAppDbContext(dbContextOptionsBuilder.Options);
        }
    }
}