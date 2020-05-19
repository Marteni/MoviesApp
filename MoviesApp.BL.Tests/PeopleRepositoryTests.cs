using System;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;
using Xunit;

namespace MoviesApp.BL.Tests
{
    public class PeopleRepositoryTests : IClassFixture<PeopleRepositoryTestFixture>, IDisposable
    {
        private readonly PeopleRepositoryTestFixture _peopleRepositoryTestFixture;
        private PeopleRepository RepositorySUT => _peopleRepositoryTestFixture.Repository;
        //Constructor
        public PeopleRepositoryTests(PeopleRepositoryTestFixture peopleRepositoryTestFixture)
        {
            _peopleRepositoryTestFixture = peopleRepositoryTestFixture;
            _peopleRepositoryTestFixture.PrepareDatabase();
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var detailModel = new PersonDetailModel()
            {
                Id = Guid.Parse("31385101-8e91-4f81-ab5b-9726bfca022e"),
                Name = "Michal",
                Surname = "Novak"
            };
            var returnedModel = RepositorySUT.Create(detailModel);

            Assert.NotNull(returnedModel);

            Assert.Equal(detailModel, returnedModel, PersonDetailModel.PersonDetailModelComparer);

        }

        [Fact]
        public void GetById_FromSeeded_DoesNotThrowAndEqualsSeeded()
        {
            var detailModel = PersonMapper.MapPersonEntityToDetailModel(DAL.Seed.GeorgeLucas);

            var returnedModel = RepositorySUT.GetById(DAL.Seed.GeorgeLucas.Id);

            Assert.Equal(detailModel, returnedModel, PersonDetailModel.PersonDetailModelComparer);
            
        }

        [Fact]
        public void Update_Name_FromSeeded_DoesNotThrow()
        {
            var detailModel = PersonMapper.MapPersonEntityToDetailModel(DAL.Seed.MarkHamill);
            detailModel.Name = "This is (not) gonna leave a mark";
           

           RepositorySUT.Update(detailModel);
       
        }

        [Fact]
        public void Update_Name_FromSeeded_CheckUpdated()
        {
            //Arrange
            var detailModel = PersonMapper.MapPersonEntityToDetailModel(DAL.Seed.MarkHamill);
            detailModel.Name = "Changed recipe name 1";

            //Act
            RepositorySUT.Update(detailModel);

            //Assert
            var returnedModel = RepositorySUT.GetById(detailModel.Id);
            Assert.Equal(detailModel, returnedModel, PersonDetailModel.PersonDetailModelComparer);
        }

        [Fact]
        public void DeleteById_FromSeeded_DoesNotThrow()
        {
            var detailModel = PersonMapper.MapPersonEntityToDetailModel(DAL.Seed.CarrieFisher);

            RepositorySUT.Delete(detailModel.Id);
        }

        public void Dispose()
        {
            _peopleRepositoryTestFixture.TearDownDatabase();
        }
    }
}
