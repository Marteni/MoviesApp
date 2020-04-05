using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using Xunit;

namespace MoviesApp.BL.Tests
{
    public class MovieRepositoryTests : IClassFixture<MovieRepositoryTestFixture>
    {
        private readonly MovieRepositoryTestFixture _movieRepositoryTestsFixture;

        //Constructor
        public MovieRepositoryTests(MovieRepositoryTestFixture movieRepositoryTestFixture)
        {
            this._movieRepositoryTestsFixture = movieRepositoryTestFixture;
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var detailModel = MovieMapper.MapMovieEntityToDetailModel(DAL.Seed.StarWars);
            var returnedModel = _movieRepositoryTestsFixture.Repository.Create(detailModel);

            Assert.NotNull(returnedModel);

            Assert.Equal(detailModel, returnedModel, MovieDetailModel.MovieDetailModelComparer);

            _movieRepositoryTestsFixture.Repository.Delete(returnedModel.Id);
        }

        [Fact]
        public void GetById_FromSeeded_DoesNotThrowAndEqualsSeeded()
        {
            var detailModel = MovieMapper.MapMovieEntityToDetailModel(DAL.Seed.StarWars);
            _movieRepositoryTestsFixture.Repository.Create(detailModel);

            var returnedModel = _movieRepositoryTestsFixture.Repository.GetById(DAL.Seed.StarWars.Id);

            Assert.Equal(detailModel, returnedModel, MovieDetailModel.MovieDetailModelComparer);
            _movieRepositoryTestsFixture.Repository.Delete(returnedModel.Id);
        }

        [Fact]
        public void Update_Name_FromSeeded_CheckUpdated()
        {
            var detailModel = MovieMapper.MapMovieEntityToDetailModel(DAL.Seed.StarWars);
            _movieRepositoryTestsFixture.Repository.Create(detailModel);

            detailModel.OriginalTitle = "This is (not) gonna leave a mark";
            _movieRepositoryTestsFixture.Repository.Update(detailModel);

            var returnedModel = _movieRepositoryTestsFixture.Repository.GetById(DAL.Seed.StarWars.Id);
            Assert.Equal(detailModel, returnedModel, MovieDetailModel.MovieDetailModelComparer);
            _movieRepositoryTestsFixture.Repository.Delete(returnedModel.Id);
        }

        [Fact]
        public void DeleteById_FromSeeded_DoesNotThrow()
        {
            var detailModel = MovieMapper.MapMovieEntityToDetailModel(DAL.Seed.StarWars);
            _movieRepositoryTestsFixture.Repository.Create(detailModel);

            var returnedModel = _movieRepositoryTestsFixture.Repository.GetById(DAL.Seed.StarWars.Id);
            Assert.NotNull(returnedModel);

            _movieRepositoryTestsFixture.Repository.Delete(returnedModel.Id);

            try
            {
                _movieRepositoryTestsFixture.Repository.GetById(DAL.Seed.StarWars.Id);
            }
            catch (System.InvalidOperationException) { }
        }
    }
}
