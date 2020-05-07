//using System;
//using MoviesApp.BL.Repositories;
//using MoviesApp.DAL.Enums;
//using Xunit;

//namespace MoviesApp.BL.Tests
//{
//    public class MovieRepositoryTests : IClassFixture<MovieRepositoryTestsFixture>
//    {
//        private readonly MovieRepositoryTestsFixture _movieRepositoryTestsFixture;

//        private MovieRepository RepositoryMovies => _movieRepositoryTestsFixture.Repository;

//        //Constructor
//        public MovieRepositoryTests(MovieRepositoryTestsFixture movieRepositoryTestsFixture)
//        {
//            this._movieRepositoryTestsFixture = movieRepositoryTestsFixture;
//        }

//        [Fact]
//        public void Create_MovieWithoutNavigationals_DoesNotThrowAndEqualsCreated()
//        {
//            var movieModel = new MovieModel
//            {
//                OriginalTitle = "Star Wars: Episode IV - New Hope",
//                CzechTitle = "Star Wars: Epizoda IV - Nová nadìje",
//                Genre = GenreType.ScienceFiction,
//                PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180",
//                CountryOfOrigin = "USA",
//                Length = TimeSpan.FromMinutes(121),
//                Description =
//                    "Rytíøi Jedi byli vyhlazeni a Impérium vládne galaxii pevnou rukou. Malá skupina povstalcù se odváží vzdorovat a ukradne plány k nejmocnìjší zbrani Impéria, Hvìzdì smrti. Imperátorùv nejvìrnìjší služebník, Darth Vader, musí najít plány a skrytou základnu povstalcù. Zpráva o princeznì Lei a vùdci rebelù se dostane až k obyèejnému farmáøi, Lukovi Skywalkerovi. Ten se øídí svým osudem, zachraòuje princeznu a pomáhá povstalcùm svrhnout Impérium spoleènì s takovými nezapomenutelnými spojenci jako: Obi-Wan Kenobi, domýšlivý Han Solo, loajální Chewbacca a droidové R2-D2 a C3PO.",
//            };

//            var returnedModel = RepositoryMovies.InsertOrUpdate(movieModel);

//            Assert.Equal(movieModel, returnedModel, MovieModel.MovieModelEqualityComparer);
//        }

//    }
//}
