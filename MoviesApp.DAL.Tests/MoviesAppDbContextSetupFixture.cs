using System;
using MoviesApp.DAL.Factories;
using Xunit;

namespace MoviesApp.DAL.Tests
{
    public class MoviesAppDbContextSetupFixture : IDisposable
    {
        public InMemoryDbContextFactory DbContextFactory { get; }

        public MoviesAppDbContextSetupFixture(string testDbName) => DbContextFactory = new InMemoryDbContextFactory(testDbName);

        public void PrepareDatabase()
        {
            using var dbx = DbContextFactory.CreateDbContext();
            dbx.Database.EnsureCreated();
        }

        public void TearDownDatabase()
        {
            using var dbx = DbContextFactory.CreateDbContext();
            dbx.Database.EnsureDeleted();
        }

        public void Dispose()
        {
            TearDownDatabase();
        }
    }
}
