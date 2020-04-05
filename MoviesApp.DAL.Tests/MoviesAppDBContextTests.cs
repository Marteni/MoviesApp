using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL.Entities;
using MoviesApp.DAL.Enums;
using Xunit;

/*
 * USES Microsoft.EntityFrameworkCore.InMemory
 * InMemory => can't test foreign keys
 */

namespace MoviesApp.DAL.Tests
{
    public class MoviesAppDBContextTests : IDisposable
    {
        private readonly DbContextInMemoryFactory _dbContextFactory;
        private readonly MoviesAppDbContext _moviesAppDbContext;

        public MoviesAppDBContextTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(MoviesAppDBContextTests));
            _moviesAppDbContext = _dbContextFactory.Create();
            _moviesAppDbContext.Database.EnsureCreated();   // For InMemory database
            //_moviesAppDbContext.Database.Migrate();       // For SQL database
        }

        [Fact]
        public void AddNew_Movie_Persistent()
        {
            var movie = new MovieEntity()
            {
                Id = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB7712A"),
                OriginalTitle = "Star Wars: Episode III - Revenge of the Sith",
                CzechTitle = "Star Wars: Epizoda III - Pomsta Sithù",
                Genre = GenreType.ScienceFiction,
                PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/000/005/5671_445cb1.jpg?h180",
                CountryOfOrigin = "USA",
                Length = TimeSpan.FromMinutes(140),
                Description = "V tøetí epizodì ságy Star Wars zuøí Klonové války, které prohloubily rozpory mezi kancléøem Palpatinem a Radou Jediù. Mladý rytíø Jedi Anakin Skywalker se musí rozhodnout, na èí stranì bude stát. Podlehne slibùm moci a pokušení Temné strany, vstoupí do služeb zlého Darth Sidiouse a stane se z nìj Darth Vader. Sithští lordové se chtìjí spoleènì pomstít a prvním krokem jejich plánu je likvidace Jediù. Pouze Yoda a Obi-Wan pøežijí a musejí se Sithùm postavit, což vede k dramatickému souboji svìtelnými meèi mezi Anakinem a Obi-Wanem, který rozhodne o osudu galaxie.",
                Actors = new List<MoviesPersonActorEntity>(),
                Directors = new List<MoviesPersonDirectorEntity>(),
                Ratings = new List<RatingEntity>()
            };

            _moviesAppDbContext.Movies.Add(movie);
            _moviesAppDbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();
            var movieFromDb = dbx.Movies.Single(m => m.Id == movie.Id);
            Assert.Equal(movie, movieFromDb, MovieEntity.MovieComparer);
        }

        [Fact]
        public void AddNew_Person_Persistent()
        {
            var person = new PersonEntity()
            {
                Id = new Guid("14858480-C954-4424-A549-16E2B030239A"),
                Name = "George",
                Surname = "Lucas",
                Age = 75,
                PictureUrl = "https://img.csfd.cz/files/images/creator/photos/000/269/269670_1f4cd0.jpg?w100h132crop",
                ActedInMovies = new List<MoviesPersonActorEntity>(),
                DirectedMovies = new List<MoviesPersonDirectorEntity>()
            };

            _moviesAppDbContext.People.Add(person);
            _moviesAppDbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();
            var personFromDb = dbx.People.Single(p => p.Id == person.Id);
            Assert.Equal(person, personFromDb, PersonEntity.PersonComparer);
        }

        [Fact]
        public void AddNew_Rating_Persistent()
        {
            var rating = new RatingEntity()
            {
                Id = new Guid("595FE374-060A-4E06-9201-56C1D61D30AA"),
                Nick = "SithJedi54",
                NumericEvaluation = 10,
                Review = "Twas AMAZING!"
            };

            _moviesAppDbContext.Ratings.Add(rating);
            _moviesAppDbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();
            var ratingFromDb = dbx.Ratings.Single(r => r.Id == rating.Id);
            Assert.Equal(rating, ratingFromDb, RatingEntity.RatingComparer);
        }

        public void Dispose() => _moviesAppDbContext?.Dispose();
    }
}
