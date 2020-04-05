using MoviesApp.BL.Repositories;
using MoviesApp.DAL.Factories;
using MoviesApp.DAL.Tests;

namespace MoviesApp.BL.Tests
{
    public class MovieRepositoryTestsFixture : MoviesAppDbContextSetupFixture
    {
        public MovieRepositoryTestsFixture() : base(nameof(MovieRepositoryTestsFixture))
        {
            Repository = new MovieRepository(IDbContextFactory);

            PrepareDatabase();
        }

        public Repository { get; }
    }
}