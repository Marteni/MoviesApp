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
                CzechTitle = "Star Wars: Epizoda IV - Nová nadìje",
                Genre = GenreType.ScienceFiction,
                PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180",
                CountryOfOrigin = "USA",
                Length = TimeSpan.FromMinutes(121),
                Description =
                    "Rytíøi Jedi byli vyhlazeni a Impérium vládne galaxii pevnou rukou. Malá skupina povstalcù se odváží vzdorovat a ukradne plány k nejmocnìjší zbrani Impéria, Hvìzdì smrti. Imperátorùv nejvìrnìjší služebník, Darth Vader, musí najít plány a skrytou základnu povstalcù. Zpráva o princeznì Lei a vùdci rebelù se dostane až k obyèejnému farmáøi, Lukovi Skywalkerovi. Ten se øídí svým osudem, zachraòuje princeznu a pomáhá povstalcùm svrhnout Impérium spoleènì s takovými nezapomenutelnými spojenci jako: Obi-Wan Kenobi, domýšlivý Han Solo, loajální Chewbacca a droidové R2-D2 a C3PO.",
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
