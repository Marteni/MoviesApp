using MoviesApp.BL.Repositories;
using MoviesApp.DAL;
using MoviesApp.DAL.Tests;

namespace MoviesApp.BL.Tests
{
    public class PeopleRepositoryTestFixture : MoviesAppDbContextSetupFixture
    {
        

        public PeopleRepositoryTestFixture() : base(nameof(PeopleRepositoryTestFixture))
        {
            Repository = new PeopleRepository(DbContextFactory);
        }
        public PeopleRepository Repository { get; }


    }
}