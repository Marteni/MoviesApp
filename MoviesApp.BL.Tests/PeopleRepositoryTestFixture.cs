using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.DAL.Tests;
using MoviesApp.DAL.Factories;
using MoviesApp.BL.Repositories;

namespace MoviesApp.BL.Tests
{
    public class PeopleRepositoryTestFixture : MoviesAppDbContextSetupFixture
    {
        public PeopleRepositoryTestFixture() : base(nameof(PeopleRepositoryTestFixture))
        {
            peopleRepository = new PeopleRepository(IDbContextFactory);

            PrepareDatabase();
        }

        public PeopleRepository { get; }
}
}
