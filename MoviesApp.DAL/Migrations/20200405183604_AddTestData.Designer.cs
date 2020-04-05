﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesApp.DAL;

namespace MoviesApp.DAL.Migrations
{
    [DbContext(typeof(MoviesAppDbContext))]
    [Migration("20200405183604_AddTestData")]
    partial class AddTestData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoviesApp.DAL.Entities.MovieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryOfOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CzechTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Length")
                        .HasColumnType("time");

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129"),
                            CountryOfOrigin = "USA",
                            CzechTitle = "Star Wars: Epizoda IV - Nová naděje",
                            Description = "Rytíři Jedi byli vyhlazeni a Impérium vládne galaxii pevnou rukou. Malá skupina povstalců se odváží vzdorovat a ukradne plány k nejmocnější zbrani Impéria, Hvězdě smrti. Imperátorův nejvěrnější služebník, Darth Vader, musí najít plány a skrytou základnu povstalců. Zpráva o princezně Lei a vůdci rebelů se dostane až k obyčejnému farmáři, Lukovi Skywalkerovi. Ten se řídí svým osudem, zachraňuje princeznu a pomáhá povstalcům svrhnout Impérium společně s takovými nezapomenutelnými spojenci jako: Obi-Wan Kenobi, domýšlivý Han Solo, loajální Chewbacca a droidové R2-D2 a C3PO.",
                            Genre = 14,
                            Length = new TimeSpan(0, 2, 1, 0, 0),
                            OriginalTitle = "Star Wars: Episode IV - New Hope",
                            PosterImageUrl = "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180"
                        });
                });

            modelBuilder.Entity("MoviesApp.DAL.Entities.MoviesPersonActorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActedInMovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ActedInMovieId");

                    b.HasIndex("ActorId");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12e1ce4d-2c8c-4bce-b610-129a784eb03b"),
                            ActorId = new Guid("ed74ba50-f208-49ca-a71a-7bfdca3e1469"),
                            MovieId = new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129")
                        },
                        new
                        {
                            Id = new Guid("7648ebfa-9e58-4835-a237-e9e7b6387262"),
                            ActorId = new Guid("cab2ca13-0f8b-4839-a10d-bbedeab84565"),
                            MovieId = new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129")
                        });
                });

            modelBuilder.Entity("MoviesApp.DAL.Entities.MoviesPersonDirectorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DirectedMovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DirectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DirectedMovieId");

                    b.HasIndex("DirectorId");

                    b.ToTable("Directors");

                    b.HasData(
                        new
                        {
                            Id = new Guid("19e3745e-9259-471a-890b-f6db55d48f24"),
                            DirectorId = new Guid("14858480-c954-4424-a549-16e2b0302397"),
                            MovieId = new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129")
                        });
                });

            modelBuilder.Entity("MoviesApp.DAL.Entities.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = new Guid("14858480-c954-4424-a549-16e2b0302397"),
                            Age = 75,
                            Name = "George",
                            PictureUrl = "https://img.csfd.cz/files/images/creator/photos/000/269/269670_1f4cd0.jpg?w100h132crop",
                            Surname = "George"
                        },
                        new
                        {
                            Id = new Guid("ed74ba50-f208-49ca-a71a-7bfdca3e1469"),
                            Age = 68,
                            Name = "Mark",
                            PictureUrl = "https://img.csfd.cz/files/images/creator/photos/163/523/163523956_9edcf8.jpg?w100h132crop",
                            Surname = "Hamill"
                        },
                        new
                        {
                            Id = new Guid("cab2ca13-0f8b-4839-a10d-bbedeab84565"),
                            Age = 60,
                            Name = "Carrie",
                            PictureUrl = "https://img.csfd.cz/files/images/creator/photos/164/132/164132451_95e211.jpg?w100h132crop",
                            Surname = "Fisher"
                        });
                });

            modelBuilder.Entity("MoviesApp.DAL.Entities.RatingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nick")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumericEvaluation")
                        .HasColumnType("int");

                    b.Property<Guid>("RatedMovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Review")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RatedMovieId");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("595fe374-060a-4e06-9201-56c1d61d30a2"),
                            Nick = "SithJedi54",
                            NumericEvaluation = 10,
                            RatedMovieId = new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129"),
                            Review = "Twas AMAZING!"
                        });
                });

            modelBuilder.Entity("MoviesApp.DAL.Entities.MoviesPersonActorEntity", b =>
                {
                    b.HasOne("MoviesApp.DAL.Entities.MovieEntity", "ActedInMovie")
                        .WithMany("Actors")
                        .HasForeignKey("ActedInMovieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoviesApp.DAL.Entities.PersonEntity", "Actor")
                        .WithMany("ActedInMovies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoviesApp.DAL.Entities.MoviesPersonDirectorEntity", b =>
                {
                    b.HasOne("MoviesApp.DAL.Entities.MovieEntity", "DirectedMovie")
                        .WithMany("Directors")
                        .HasForeignKey("DirectedMovieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoviesApp.DAL.Entities.PersonEntity", "Director")
                        .WithMany("DirectedMovies")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoviesApp.DAL.Entities.RatingEntity", b =>
                {
                    b.HasOne("MoviesApp.DAL.Entities.MovieEntity", "RatedMovie")
                        .WithMany("Ratings")
                        .HasForeignKey("RatedMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
