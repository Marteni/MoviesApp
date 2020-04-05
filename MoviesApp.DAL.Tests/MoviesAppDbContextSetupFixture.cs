using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.DAL.Tests
{
    public class MoviesAppDbContextSetupFixture : IDisposable
    {
        public DbContextInMemoryFactory DbContextFactory { get; }

        public MoviesAppDbContextSetupFixture(string testDbName) => DbContextFactory = new DbContextInMemoryFactory(testDbName);

        public void PrepareDatabase()
        {
            using var dbx = DbContextFactory.Create();
            dbx.Database.EnsureCreated();
        }

        public void TearDownDatabase()
        {
            using var dbx = DbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }

        public void Dispose()
        {
            TearDownDatabase();
        }
    }
}
