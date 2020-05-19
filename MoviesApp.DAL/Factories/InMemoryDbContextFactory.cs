using Microsoft.EntityFrameworkCore;

namespace MoviesApp.DAL.Factories
{
    public class InMemoryDbContextFactory : IDbContextFactory
    {
        private readonly string _testDbName;
        public InMemoryDbContextFactory(string testDbName) => _testDbName = testDbName;

        public MoviesAppDbContext CreateDbContext()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase(_testDbName);
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            return new MoviesAppDbContext(dbContextOptionsBuilder.Options);
        }
    }
}