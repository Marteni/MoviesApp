using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MoviesApp.BL.Tests
{
    public class PeopleRepositoryTests : IClassFixture<PeopleRepositoryTestFixture>, IDisposable
    {
        private readonly PeopleRepositoryTestFixture _peopleRepositoryTestFixture;

        private PeopleRepository RepositoryPeople => _peopleRepositoryTestFixture.Repository;

        //Constructor
        public PeopleRepositoryTests(PeopleRepositoryTestFixture peopleRepositoryTestFixture)
        {
            _peopleRepositoryTestFixture = peopleRepositoryTestFixture;
            _peopleRepositoryTestFixture.PrepareDatabase();
        }

        [Fact]
        public void Create_PersonWithoutNavigationals_DoesNotThrowAndEqualsCreated()
        {
            var person = new PersonModel
            {
                Name = "Mark",
                Surname = "Hamill",
                Age = 68,
                PictureUrl = "https://img.csfd.cz/files/images/creator/photos/163/523/163523956_9edcf8.jpg?w100h132crop"
            };

            var returnedPerson = RepositoryPeople.InsertOrUpdate(person);

            Assert.Equal(person, returnedPerson, PersonModel.PersonModelEqualityComparer);
        }

        public void Dispose()
        {
            _peopleRepositoryTestFixture.TearDownDatabase();
        }
    }
}
