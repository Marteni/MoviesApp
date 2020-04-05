using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using Xunit;

namespace MoviesApp.BL.Tests
{
    public class PeopleRepositoryTests : IClassFixture<PeopleRepositoryTestFixture>
    {
        private readonly PeopleRepositoryTestFixture _peopleRepositoryTestFixture;

        //Constructor
        public PeopleRepositoryTests(PeopleRepositoryTestFixture peopleRepositoryTestFixture)
        {
            this._peopleRepositoryTestFixture = peopleRepositoryTestFixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var detailModel = PersonMapper.MapPersonEntityToDetailModel(DAL.Seed.MarkHamill);
            var returnedModel = _peopleRepositoryTestFixture.Repository.Create(detailModel);

            Assert.NotNull(returnedModel);

            Assert.Equal(detailModel, returnedModel, PersonDetailModel.PersonDetailModelComparer);

            _peopleRepositoryTestFixture.Repository.Delete(returnedModel.Id);
        }

        [Fact]
        public void GetById_FromSeeded_DoesNotThrowAndEqualsSeeded()
        {
            var detailModel = PersonMapper.MapPersonEntityToDetailModel(DAL.Seed.MarkHamill);
            _peopleRepositoryTestFixture.Repository.Create(detailModel);

            var returnedModel = _peopleRepositoryTestFixture.Repository.GetById(DAL.Seed.MarkHamill.Id);

            Assert.Equal(detailModel, returnedModel, PersonDetailModel.PersonDetailModelComparer);
            _peopleRepositoryTestFixture.Repository.Delete(returnedModel.Id);
        }

        [Fact]
        public void Update_Name_FromSeeded_CheckUpdated()
        {
            var detailModel = PersonMapper.MapPersonEntityToDetailModel(DAL.Seed.MarkHamill);
            _peopleRepositoryTestFixture.Repository.Create(detailModel);

            detailModel.Name = "This is (not) gonna leave a mark";
            _peopleRepositoryTestFixture.Repository.Update(detailModel);

            var returnedModel = _peopleRepositoryTestFixture.Repository.GetById(detailModel.Id);
            Assert.Equal(detailModel, returnedModel, PersonDetailModel.PersonDetailModelComparer);
            _peopleRepositoryTestFixture.Repository.Delete(returnedModel.Id);
        }

        [Fact]
        public void DeleteById_FromSeeded_DoesNotThrow()
        {
            var detailModel = PersonMapper.MapPersonEntityToDetailModel(DAL.Seed.MarkHamill);
            _peopleRepositoryTestFixture.Repository.Create(detailModel);

            var returnedModel = _peopleRepositoryTestFixture.Repository.GetById(DAL.Seed.MarkHamill.Id);
            Assert.NotNull(returnedModel);

            _peopleRepositoryTestFixture.Repository.Delete(returnedModel.Id);

            try 
            {
                _peopleRepositoryTestFixture.Repository.GetById(returnedModel.Id);
            }
            catch (System.InvalidOperationException e){}
        }
    }
}
