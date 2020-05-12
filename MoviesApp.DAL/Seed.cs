using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL.Entities;
using MoviesApp.DAL.Enums;

namespace MoviesApp.DAL
{
    public static class Seed
    {
            public static readonly RatingEntity RatingMovie1 = new RatingEntity()
            {
                Id = Guid.Parse("595FE374-060A-4E06-9201-56C1D61D30A2"),
                RatedMovieId = Guid.Parse("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
                Nick = "SithJedi54",
                NumericEvaluation = 10,
                Review = "Twas AMAZING!"
            };


            public static readonly PersonEntity GeorgeLucas = new PersonEntity()
            {
                Id = Guid.Parse("14858480-C954-4424-A549-16E2B0302397"),
                Name = "George",
                Surname = "Lucas",
                Age = 75,
                PictureUrl = "https://img.csfd.cz/files/images/creator/photos/000/269/269670_1f4cd0.jpg?w100h132crop",
                ActedInMovies = new List<MoviesPersonActorEntity>(),
                DirectedMovies = new List<MoviesPersonDirectorEntity>()
                //{
                //    new MoviesPersonDirectorEntity()
                //    {
                //        Id = Guid.Parse("19E3745E-9259-471A-890B-F6DB55D48F24"),
                //        DirectorId = Guid.Parse("14858480-C954-4424-A549-16E2B0302397"),
                //        MovieId = Guid.Parse("0302A349-FFC2-429F-BC1C-8AD64FB77129")
                //    },
                //    new MoviesPersonDirectorEntity()
                //    {
                //        Id = Guid.Parse("B9B09A92-8533-4C8E-BF5E-F371348B88DC"),
                //        DirectorId = Guid.Parse("14858480-C954-4424-A549-16E2B0302397"),
                //        MovieId = Guid.Parse("5866E0F7-DED9-4C71-9638-68EFDBEB958C")
                //    }
                //}
            };


            public static readonly PersonEntity MarkHamill = new PersonEntity()
            {
                Id = Guid.Parse("ED74BA50-F208-49CA-A71A-7BFDCA3E1469"),
                Name = "Mark",
                Surname = "Hamill",
                Age = 68,
                PictureUrl = "https://img.csfd.cz/files/images/creator/photos/163/523/163523956_9edcf8.jpg?w100h132crop",
                DirectedMovies = new List<MoviesPersonDirectorEntity>(),
                ActedInMovies = new List<MoviesPersonActorEntity>()
                //{
                //    new MoviesPersonActorEntity()
                //    {
                //        Id = Guid.Parse("12E1CE4D-2C8C-4BCE-B610-129A784EB03B"),
                //        ActorId = Guid.Parse("ED74BA50-F208-49CA-A71A-7BFDCA3E1469"),
                //        MovieId = Guid.Parse("0302A349-FFC2-429F-BC1C-8AD64FB77129")
                //    },
                //    new MoviesPersonActorEntity()
                //    {
                //        Id = Guid.Parse("A4525787-679A-4AFC-86C3-2E0BC0E3518B"),
                //        ActorId = Guid.Parse("ED74BA50-F208-49CA-A71A-7BFDCA3E1469"),
                //        MovieId = Guid.Parse("5866E0F7-DED9-4C71-9638-68EFDBEB958C")
                //    }
                //}
            };


            public static readonly PersonEntity CarrieFisher = new PersonEntity()
            {
                Id = Guid.Parse("CAB2CA13-0F8B-4839-A10D-BBEDEAB84565"),
                Name = "Carrie",
                Surname = "Fisher",
                Age = 60,
                PictureUrl = "https://img.csfd.cz/files/images/creator/photos/164/132/164132451_95e211.jpg?w100h132crop",
                DirectedMovies = new List<MoviesPersonDirectorEntity>(),
                ActedInMovies = new List<MoviesPersonActorEntity>()
                //{
                //    new MoviesPersonActorEntity()
                //    {
                //        Id = Guid.Parse("7648EBFA-9E58-4835-A237-E9E7B6387262"),
                //        ActorId = Guid.Parse("CAB2CA13-0F8B-4839-A10D-BBEDEAB84565"),
                //        MovieId = Guid.Parse("0302A349-FFC2-429F-BC1C-8AD64FB77129")
                //    },
                //    new MoviesPersonActorEntity()
                //    {
                //        Id = Guid.Parse("B65AE050-EAD0-4E04-BA65-828A65A613DA"),
                //        ActorId = Guid.Parse("CAB2CA13-0F8B-4839-A10D-BBEDEAB84565"),
                //        MovieId = Guid.Parse("5866E0F7-DED9-4C71-9638-68EFDBEB958C")
                //    }
                //}
            };


            public static readonly MovieEntity StarWars = new MovieEntity()
            {
                Id = Guid.Parse("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
                OriginalTitle = "Star Wars: Episode IV - New Hope",
                CzechTitle = "Star Wars: Epizoda IV - Nová naděje",
                Genre = GenreType.ScienceFiction,
                PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180",
                CountryOfOrigin = "USA",
                Length = TimeSpan.FromMinutes(121),
                Description =
                    "Rytíři Jedi byli vyhlazeni a Impérium vládne galaxii pevnou rukou. Malá skupina povstalců se odváží vzdorovat a ukradne plány k nejmocnější zbrani Impéria, Hvězdě smrti. Imperátorův nejvěrnější služebník, Darth Vader, musí najít plány a skrytou základnu povstalců. Zpráva o princezně Lei a vůdci rebelů se dostane až k obyčejnému farmáři, Lukovi Skywalkerovi. Ten se řídí svým osudem, zachraňuje princeznu a pomáhá povstalcům svrhnout Impérium společně s takovými nezapomenutelnými spojenci jako: Obi-Wan Kenobi, domýšlivý Han Solo, loajální Chewbacca a droidové R2-D2 a C3PO.",
                Actors = new List<MoviesPersonActorEntity>(),
                //{
                //    //new MoviesPersonActorEntity()
                //    //{
                //    //    Id = new Guid("12E1CE4D-2C8C-4BCE-B610-129A784EB03B"),
                //    //    ActorId = new Guid("ED74BA50-F208-49CA-A71A-7BFDCA3E1469"),
                //    //    MovieId = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
                //    //    Actor = MarkHamill
                //    //},
                //    //new MoviesPersonActorEntity()
                //    //{
                //    //    Id = new Guid("7648EBFA-9E58-4835-A237-E9E7B6387262"),
                //    //    ActorId = new Guid("CAB2CA13-0F8B-4839-A10D-BBEDEAB84565"),
                //    //    MovieId = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
                //    //    Actor = CarrieFisher
                //    //}
                //},
                Directors = new List<MoviesPersonDirectorEntity>(),
                //{
                //    //new MoviesPersonDirectorEntity()
                //    //{
                //    //    Id = new Guid("6CE02CF5-4B04-469B-9CE1-6C99FDCE0FFA"),
                //    //    DirectorId = new Guid("14858480-C954-4424-A549-16E2B0302397"),
                //    //    MovieId = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
                //    //    Director = GeorgeLucas
                //    //}
                //},
                Ratings = new List<RatingEntity>()

            };


        public static void SeedGeorge(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasData(new  
                {
                    Id = GeorgeLucas.Id,
                    Name = GeorgeLucas.Name,
                    Surname = GeorgeLucas.Name,
                    Age = GeorgeLucas.Age,
                    PictureUrl = GeorgeLucas.PictureUrl,
                    DirectedMovies = GeorgeLucas.DirectedMovies,
                    ActedInMovies = GeorgeLucas.ActedInMovies
                });
        }

        public static void SeedMark(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasData(new  
                {
                    Id = MarkHamill.Id,
                    Name = MarkHamill.Name,
                    Surname = MarkHamill.Surname,
                    Age = MarkHamill.Age, 
                    PictureUrl = MarkHamill.PictureUrl,
                    DirectedMovies= MarkHamill.DirectedMovies,
                    ActedInMovies = MarkHamill.ActedInMovies
                });
        }

        public static void SeedCarry(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasData(new  
                {
                    Id = CarrieFisher.Id,
                    Name = CarrieFisher.Name,
                    Surname = CarrieFisher.Surname,
                    Age = CarrieFisher.Age,
                    PictureUrl = CarrieFisher.PictureUrl,
                    DirectedMovies = CarrieFisher.DirectedMovies,
                    ActedInMovies = CarrieFisher.ActedInMovies
                });
        }

        public static void SeedMovie(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieEntity>()
                .HasData(new  
                {
                    Id = StarWars.Id,
                    OriginalTitle = StarWars.OriginalTitle,
                    CzechTitle = StarWars.CzechTitle,
                    Genre = StarWars.Genre,
                    PosterImageUrl = StarWars.PosterImageUrl,
                    CountryOfOrigin = StarWars.CountryOfOrigin,
                    Length = StarWars.Length,
                    Description = StarWars.Description,
                    Actors = StarWars.Actors,
                    Directors = StarWars.Directors,
                    Ratings = StarWars.Ratings
                });
        }

     
        public static void SeedRating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RatingEntity>()
                .HasData(new  
                {
                    Id = RatingMovie1.Id,
                    RatedMovieId = RatingMovie1.RatedMovieId,
                    Nick = RatingMovie1.Nick,
                    NumericEvaluation = RatingMovie1.NumericEvaluation, 
                    Review = RatingMovie1.Review
                });
        }

        public static void SeedDirectors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesPersonDirectorEntity>()
                .HasData(new
                {
                    Id = GeorgeLucas.DirectedMovies.First().Id,
                    DirectorId = GeorgeLucas.DirectedMovies.First().DirectorId,
                    MovieId = GeorgeLucas.DirectedMovies.First().MovieId
                });
        }

        public static void SeedActors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesPersonActorEntity>()
                .HasData(new
                {
                    Id = MarkHamill.ActedInMovies.First().Id,
                    ActorId = MarkHamill.ActedInMovies.First().ActorId,
                    MovieId = MarkHamill.ActedInMovies.First().MovieId
                });

            modelBuilder.Entity<MoviesPersonActorEntity>()
                .HasData(new
                {
                    Id = CarrieFisher.ActedInMovies.First().Id,
                    ActorId = CarrieFisher.ActedInMovies.First().ActorId,
                    MovieId = CarrieFisher.ActedInMovies.First().MovieId
                });
        }

    }
}
