using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL.Entities;
using MoviesApp.DAL.Enums;
using Xunit;


namespace MoviesApp.DAL.Tests
{
    public class MoviesAppDbContextTests : IClassFixture<MoviesAppDbContextTestsClassSetupFixture>, IDisposable
    {
        private readonly MoviesAppDbContextTestsClassSetupFixture _testContext;

        public MoviesAppDbContextTests(MoviesAppDbContextTestsClassSetupFixture testContext)
        {
            _testContext = testContext;
            _testContext.PrepareDatabase();
        }

        [Fact] 
        public void AddNew_Movie_Persistent()
        {
            //Arrange
            var movie = new MovieEntity()
            {
                Id = new Guid("82a58772-dd6a-4a66-b7cc-08e316232663"),
                OriginalTitle = "Star Wars: Episode III - Revenge of the Sith",
                CzechTitle = "Star Wars: Epizoda III - Pomsta Sith�",
                Genre = GenreType.ScienceFiction,
                PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/000/005/5671_445cb1.jpg?h180",
                CountryOfOrigin = "USA",
                Length = TimeSpan.FromMinutes(140),
                Description = "V t�et� epizod� s�gy Star Wars zu�� Klonov� v�lky, kter� prohloubily rozpory mezi kancl��em Palpatinem a Radou Jedi�. Mlad� ryt�� Jedi Anakin Skywalker se mus� rozhodnout, na �� stran� bude st�t. Podlehne slib�m moci a poku�en� Temn� strany, vstoup� do slu�eb zl�ho Darth Sidiouse a stane se z n�j Darth Vader. Sith�t� lordov� se cht�j� spole�n� pomst�t a prvn�m krokem jejich pl�nu je likvidace Jedi�. Pouze Yoda a Obi-Wan p�e�ij� a musej� se Sith�m postavit, co� vede k dramatick�mu souboji sv�teln�mi me�i mezi Anakinem a Obi-Wanem, kter� rozhodne o osudu galaxie.",
            };
            //Act
            _testContext.MoviesDbContextSUT.Movies.Add(movie);
            _testContext.MoviesDbContextSUT.SaveChanges();

            //Assert
            using var dbx = _testContext.DbContextFactory.CreateDbContext();
            var movieFromDb = dbx.Movies.First(m => m.Id == movie.Id);
            Assert.Equal(movie, movieFromDb, MovieEntity.MovieComparer);
        }

        [Fact]
        public void AddNew_Person_Persistent()
        {
            var person = new PersonEntity()
            {
                Id = new Guid("d10250e0-7642-4f94-920d-23fe9a5db33d"),
                Name = "George",
                Surname = "Lucas",
                Age = 75,
                PictureUrl = "https://img.csfd.cz/files/images/creator/photos/000/269/269670_1f4cd0.jpg?w100h132crop"
            };

            _testContext.MoviesDbContextSUT.People.Add(person);
            _testContext.MoviesDbContextSUT.SaveChanges();

            using var dbx = _testContext.DbContextFactory.CreateDbContext();
            var personFromDb = dbx.People.First(p => p.Id == person.Id);
            Assert.Equal(person, personFromDb, PersonEntity.PersonComparer);
        }

        [Fact]
        public void AddNew_Rating_Persistent()
        {
            var rating = new RatingEntity()
            {
                Id = new Guid("d36ec7a6-469f-4b92-b1a4-cec54ec5dd02"),
                Nick = "SithJedi54",
                NumericEvaluation = 10,
                Review = "Twas AMAZING!"
            };

            _testContext.MoviesDbContextSUT.Ratings.Add(rating);
            _testContext.MoviesDbContextSUT.SaveChanges();

            using var dbx = _testContext.DbContextFactory.CreateDbContext();
            var ratingFromDb = dbx.Ratings.First(r => r.Id == rating.Id);
            Assert.Equal(rating, ratingFromDb, RatingEntity.RatingComparer);
        }

        [Fact]
        public void DeletePerson()
        {
            if (_testContext.MoviesDbContextSUT.Database.IsInMemory())
            {
                return;
            }


            _testContext.MoviesDbContextSUT.People.Remove(_testContext.MoviesDbContextSUT.People.Find(Seed.CarrieFisher.Id));
            _testContext.MoviesDbContextSUT.SaveChanges();

            using var dbx = _testContext.DbContextFactory.CreateDbContext();
            Assert.Equal(2, dbx.People.Count());
        }

        [Fact]
        public void DeleteMovie()
        {
            if (_testContext.MoviesDbContextSUT.Database.IsInMemory())
            {
                return;
            }


            _testContext.MoviesDbContextSUT.Movies.Remove(_testContext.MoviesDbContextSUT.Movies.Find(Seed.StarWars.Id));
            _testContext.MoviesDbContextSUT.SaveChanges();

            using var dbx = _testContext.DbContextFactory.CreateDbContext();
            Assert.Equal(0, dbx.Movies.Count());
        }

        public void Dispose() => _testContext.Dispose();
    }
}
