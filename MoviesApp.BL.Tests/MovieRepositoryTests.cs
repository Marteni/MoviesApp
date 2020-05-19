using System;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;
using MoviesApp.DAL.Enums;
using Xunit;

namespace MoviesApp.BL.Tests
{
    public class MovieRepositoryTests : IClassFixture<MovieRepositoryTestsFixture>, IDisposable
    {
        private readonly MovieRepositoryTestsFixture _movieRepositoryTestsFixture;
        private MovieRepository RepositorySUT => _movieRepositoryTestsFixture.Repository;

        public MovieRepositoryTests(MovieRepositoryTestsFixture movieRepositoryTestsFixture)
        {
            _movieRepositoryTestsFixture = movieRepositoryTestsFixture;
            _movieRepositoryTestsFixture.PrepareDatabase();
        }

        [Fact]
        public void Create_Movie_DoesNotThrowAndEqualsCreated()
        {
            var movieModel = new MovieDetailModel
            {
                Id = Guid.Parse("5947ae88-344a-465a-b8ae-2af1e216097a"),
                OriginalTitle = "Star Wars: Episode IV - New Hope",
                CzechTitle = "Star Wars: Epizoda IV - Nov� nad�je",
                Genre = GenreType.ScienceFiction,
                PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180",
                CountryOfOrigin = "USA",
                Length = TimeSpan.FromMinutes(121),
                Description =
                    "Ryt��i Jedi byli vyhlazeni a Imp�rium vl�dne galaxii pevnou rukou. Mal� skupina povstalc� se odv�� vzdorovat a ukradne pl�ny k nejmocn�j�� zbrani Imp�ria, Hv�zd� smrti. Imper�tor�v nejv�rn�j�� slu�ebn�k, Darth Vader, mus� naj�t pl�ny a skrytou z�kladnu povstalc�. Zpr�va o princezn� Lei a v�dci rebel� se dostane a� k oby�ejn�mu farm��i, Lukovi Skywalkerovi. Ten se ��d� sv�m osudem, zachra�uje princeznu a pom�h� povstalc�m svrhnout Imp�rium spole�n� s takov�mi nezapomenuteln�mi spojenci jako: Obi-Wan Kenobi, dom��liv� Han Solo, loaj�ln� Chewbacca a droidov� R2-D2 a C3PO.",
            };

            var returnedModel = RepositorySUT.Create(movieModel);

            Assert.Equal(movieModel, returnedModel, MovieDetailModel.MovieDetailModelComparer);
        }

        [Fact]
        public void Update_Name_FromSeeded_CheckUpdated()
        {
            //Arrange
            var detailModel = MovieMapper.MapMovieEntityToDetailModel(DAL.Seed.StarWars);
            detailModel.OriginalTitle = "Star Wars V";

            //Act
            RepositorySUT.Update(detailModel);

            //Assert
            var returnedModel = RepositorySUT.GetById(detailModel.Id);
            Assert.Equal(detailModel, returnedModel, MovieDetailModel.MovieDetailModelComparer);
        }

    

        public void Dispose()
        {
            _movieRepositoryTestsFixture?.Dispose();
        }
    }
}
