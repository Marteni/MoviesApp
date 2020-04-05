using MoviesApp.BL.Repositories;

namespace MoviesApp.BL.Tests
{
    public class PeopleRepositoryTestFixture
    {
        private readonly IPeopleRepository repository;

        public PeopleRepositoryTestFixture()
        {
            repository = new PeopleRepository(new InMemoryDbContextFactory());
        }

        public IPeopleRepository Repository => repository;
    }
}