using MoviesApp.BL.Repositories;

namespace MoviesApp.BL.Tests
{
    public class MovieRepositoryTestFixture
    {
        private readonly IMovieRepository repository;

        public MovieRepositoryTestFixture()
        {
            repository = new MovieRepository(new InMemoryDbContextFactory());
        }
        public IMovieRepository Repository => repository;
    }
}