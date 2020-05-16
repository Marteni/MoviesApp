using System;
using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL.Entities;
using MoviesApp.DAL.Enums;

namespace MoviesApp.DAL
{
    public static class Seed
    {
        public static readonly RatingEntity RatingMovie1 = new RatingEntity
        {
            Id = new Guid("595FE374-060A-4E06-9201-56C1D61D30A2"),
            RatedMovieId = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
            Nick = "SithJedi54",
            NumericEvaluation = 10,
            Review = "Twas AMAZING!"
        };


        public static readonly PersonEntity GeorgeLucas = new PersonEntity
        {
            Id = new Guid("14858480-C954-4424-A549-16E2B0302397"),
            Name = "George",
            Surname = "Lucas",
            Age = 75,
            PictureUrl = "https://img.csfd.cz/files/images/creator/photos/000/269/269670_1f4cd0.jpg?w100h132crop"
        };


        public static readonly PersonEntity MarkHamill = new PersonEntity
        {
            Id = new Guid("ED74BA50-F208-49CA-A71A-7BFDCA3E1469"),
            Name = "Mark",
            Surname = "Hamill",
            Age = 68,
            PictureUrl = "https://img.csfd.cz/files/images/creator/photos/163/523/163523956_9edcf8.jpg?w100h132crop"
        };


        public static readonly PersonEntity CarrieFisher = new PersonEntity
        {
            Id = new Guid("CAB2CA13-0F8B-4839-A10D-BBEDEAB84565"),
            Name = "Carrie",
            Surname = "Fisher",
            Age = 60,
            PictureUrl = "https://img.csfd.cz/files/images/creator/photos/164/132/164132451_95e211.jpg?w100h132crop"
        };


        public static readonly MovieEntity StarWars = new MovieEntity
        {
            Id = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
            OriginalTitle = "Star Wars: Episode IV - New Hope",
            CzechTitle = "Star Wars: Epizoda IV - Nová naděje",
            Genre = GenreType.ScienceFiction,
            PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180",
            CountryOfOrigin = "USA",
            Length = TimeSpan.FromMinutes(121),
            Description =
                "Rytíři Jedi byli vyhlazeni a Impérium vládne galaxii pevnou rukou. Malá skupina povstalců se odváží vzdorovat a ukradne plány k nejmocnější zbrani Impéria, Hvězdě smrti. Imperátorův nejvěrnější služebník, Darth Vader, musí najít plány a skrytou základnu povstalců. Zpráva o princezně Lei a vůdci rebelů se dostane až k obyčejnému farmáři, Lukovi Skywalkerovi. Ten se řídí svým osudem, zachraňuje princeznu a pomáhá povstalcům svrhnout Impérium společně s takovými nezapomenutelnými spojenci jako: Obi-Wan Kenobi, domýšlivý Han Solo, loajální Chewbacca a droidové R2-D2 a C3PO."
        };


        public static void SeedGeorge(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasData(new
                {
                    GeorgeLucas.Id,
                    GeorgeLucas.Name,
                    GeorgeLucas.Surname,
                    GeorgeLucas.Age,
                    GeorgeLucas.PictureUrl
                });
        }

        public static void SeedMark(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasData(new
                {
                    MarkHamill.Id,
                    MarkHamill.Name,
                    MarkHamill.Surname,
                    MarkHamill.Age,
                    MarkHamill.PictureUrl
                });
        }

        public static void SeedCarry(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasData(new
                {
                    CarrieFisher.Id,
                    CarrieFisher.Name,
                    CarrieFisher.Surname,
                    CarrieFisher.Age,
                    CarrieFisher.PictureUrl
                });
        }

        public static void SeedMovie(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieEntity>()
                .HasData(new
                {
                    StarWars.Id,
                    StarWars.OriginalTitle,
                    StarWars.CzechTitle,
                    StarWars.Genre,
                    StarWars.PosterImageUrl,
                    StarWars.CountryOfOrigin,
                    StarWars.Length,
                    StarWars.Description
                });
        }


        public static void SeedRating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RatingEntity>()
                .HasData(new
                {
                    RatingMovie1.Id,
                    RatingMovie1.RatedMovieId,
                    RatingMovie1.Nick,
                    RatingMovie1.NumericEvaluation,
                    RatingMovie1.Review
                });
        }

    }
}
