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
//                CzechTitle = "Star Wars: Epizoda IV - Nov� nad�je",
//                Genre = GenreType.ScienceFiction,
//                PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180",
//                CountryOfOrigin = "USA",
//                Length = TimeSpan.FromMinutes(121),
//                Description =
//                    "Ryt��i Jedi byli vyhlazeni a Imp�rium vl�dne galaxii pevnou rukou. Mal� skupina povstalc� se odv�� vzdorovat a ukradne pl�ny k nejmocn�j�� zbrani Imp�ria, Hv�zd� smrti. Imper�tor�v nejv�rn�j�� slu�ebn�k, Darth Vader, mus� naj�t pl�ny a skrytou z�kladnu povstalc�. Zpr�va o princezn� Lei a v�dci rebel� se dostane a� k oby�ejn�mu farm��i, Lukovi Skywalkerovi. Ten se ��d� sv�m osudem, zachra�uje princeznu a pom�h� povstalc�m svrhnout Imp�rium spole�n� s takov�mi nezapomenuteln�mi spojenci jako: Obi-Wan Kenobi, dom��liv� Han Solo, loaj�ln� Chewbacca a droidov� R2-D2 a C3PO.",
//            };

//            var returnedModel = RepositoryMovies.InsertOrUpdate(movieModel);

//            Assert.Equal(movieModel, returnedModel, MovieModel.MovieModelEqualityComparer);
//        }

//    }
//}
