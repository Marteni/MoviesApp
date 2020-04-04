using MoviesApp.DAL.Entities;
using MoviesApp.DAL.Enums;
using System;
using Microsoft.EntityFrameworkCore;

namespace MoviesApp.DAL.Seed
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = CreateDbContext())
            {
                ClearDatabase(dbContext);
                SeedData(dbContext);
            }
        }

        private static void SeedData(MoviesAppDbContext dbContext)
        {

            var georgeLucas = new PersonEntity()
            {
                Id = new Guid("14858480-C954-4424-A549-16E2B0302397"),
                Name = "George",
                Surname = "Lucas",
                Age = 75,
                PictureUrl = "https://img.csfd.cz/files/images/creator/photos/000/269/269670_1f4cd0.jpg?w100h132crop",
                DirectedMovies =
                {
                    new MoviesPersonDirectorEntity()
                    {
                        Id = new Guid("19E3745E-9259-471A-890B-F6DB55D48F24"),
                        DirectorId = new Guid("14858480-C954-4424-A549-16E2B0302397"),
                        MovieId = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129")
                    },
                    new MoviesPersonDirectorEntity()
                    {
                        Id = new Guid("B9B09A92-8533-4C8E-BF5E-F371348B88DC"),
                        DirectorId = new Guid("14858480-C954-4424-A549-16E2B0302397"),
                        MovieId = new Guid("5866E0F7-DED9-4C71-9638-68EFDBEB958C")
                    }
                }
            };
            dbContext.People.Add(georgeLucas);

            var ewanMcGregor = new PersonEntity()
            {
                Id = new Guid("ED74BA50-F208-49CA-A71A-7BFDCA3E1469"),
                Name = "Ewan",
                Surname = "McGregor",
                Age = 49,
                PictureUrl = "https://img.csfd.cz/files/images/creator/photos/163/724/163724153_d6d3e3.jpg?w100h132crop",
                ActedInMovies = 
                {
                    new MoviesPersonActorEntity()
                    {
                        Id = new Guid("12E1CE4D-2C8C-4BCE-B610-129A784EB03B"),
                        ActorId = new Guid("ED74BA50-F208-49CA-A71A-7BFDCA3E1469"),
                        MovieId = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129")
                    }
                }
            };
            dbContext.People.Add(ewanMcGregor);

            var rating1 = new RatingEntity()
            {
                Id = new Guid("595FE374-060A-4E06-9201-56C1D61D30A2"),
                RatedMovieId = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
                Nick = "SithJedi54",
                NumericEvaluation = 10,
                Review = "Twas AMAZING!"
            };
            dbContext.Ratings.Add(rating1);


            dbContext.Movies.Add(
                new MovieEntity()
                {
                    Id = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129"),
                    OriginalTitle = "Star Wars: Episode IV - New Hope",
                    CzechTitle = "Star Wars: Epizoda IV - Nová naděje",
                    Genre = GenreType.ScienceFiction,
                    PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180",
                    CountryOfOrigin = "USA",
                    Length = TimeSpan.FromMinutes(121),
                    Description = "Rytíři Jedi byli vyhlazeni a Impérium vládne galaxii pevnou rukou. Malá skupina povstalců se odváží vzdorovat a ukradne plány k nejmocnější zbrani Impéria, Hvězdě smrti. Imperátorův nejvěrnější služebník, Darth Vader, musí najít plány a skrytou základnu povstalců. Zpráva o princezně Lei a vůdci rebelů se dostane až k obyčejnému farmáři, Lukovi Skywalkerovi. Ten se řídí svým osudem, zachraňuje princeznu a pomáhá povstalcům svrhnout Impérium společně s takovými nezapomenutelnými spojenci jako: Obi-Wan Kenobi, domýšlivý Han Solo, loajální Chewbacca a droidové R2-D2 a C3PO.",
                    Directors =
                    {
                        new MoviesPersonDirectorEntity()
                        {
                            DirectorId = new Guid("14858480-C954-4424-A549-16E2B0302397"),
                            MovieId = new Guid("0302A349-FFC2-429F-BC1C-8AD64FB77129")
                        }
                    }
                }
            );

            dbContext.Movies.Add(
                new MovieEntity()
                {
                    Id = new Guid("5866E0F7-DED9-4C71-9638-68EFDBEB958C"),
                    OriginalTitle = "Star Wars: Episode V - Empire Strikes Back",
                    CzechTitle = "Star Wars: Epizoda V - Impérium vrací úder",
                    Genre = GenreType.ScienceFiction,
                    PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398465_dc9ff3.jpg?h180",
                    CountryOfOrigin = "USA",
                    Length = TimeSpan.FromMinutes(124),
                    Description =
                        "Nastaly temné časy pro Povstání. I přes to, že 'Hvězda smrti' byla zničena, imperiální jednotky vyhnaly Rebely z jejich tajné základny a pronásledovaly je po celé Galaxii. Aby se vyhnula střetu s hrůzostrašnou Imperiální flotilou, skupina svobodných pilotů vedená Lukem Skywalkerem vybudovala novou tajnou základnu na opuštěné ledové planetě Hothu. Imperátorův pobočník Darth Vader, posedlý hledáním mladého Skywalkera, však vyslal tisíce sond do všech koutů vesmíru...",
                    Directors =
                    {
                        new MoviesPersonDirectorEntity()
                        {
                            DirectorId = new Guid("14858480-C954-4424-A549-16E2B0302397"),
                            MovieId = new Guid("5866E0F7-DED9-4C71-9638-68EFDBEB958C")
                        }
                    }
                }
            );
            

            dbContext.SaveChanges();
        }

        private static void ClearDatabase(MoviesAppDbContext dbContext)
        {
            dbContext.RemoveRange(dbContext.Actors);
            dbContext.RemoveRange(dbContext.Directors);
            dbContext.RemoveRange(dbContext.People);
            dbContext.RemoveRange(dbContext.Movies);
            dbContext.RemoveRange(dbContext.Ratings);
            dbContext.SaveChanges();
        }

        private static MoviesAppDbContext CreateDbContext()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            dbContextOptionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = TasksDB;MultipleActiveResultSets = True;Integrated Security = True; ");
            return new MoviesAppDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
