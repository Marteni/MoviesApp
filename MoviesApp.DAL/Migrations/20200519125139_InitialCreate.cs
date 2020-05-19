using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesApp.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActorId = table.Column<Guid>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DirectorId = table.Column<Guid>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OriginalTitle = table.Column<string>(nullable: true),
                    CzechTitle = table.Column<string>(nullable: true),
                    Genre = table.Column<int>(nullable: false),
                    PosterImageUrl = table.Column<string>(nullable: true),
                    CountryOfOrigin = table.Column<string>(nullable: true),
                    Length = table.Column<TimeSpan>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RatedMovieId = table.Column<Guid>(nullable: false),
                    Nick = table.Column<string>(nullable: true),
                    NumericEvaluation = table.Column<int>(nullable: false),
                    Review = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CountryOfOrigin", "CzechTitle", "Description", "Genre", "Length", "OriginalTitle", "PosterImageUrl" },
                values: new object[] { new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129"), "USA", "Star Wars: Epizoda IV - Nová naděje", "Rytíři Jedi byli vyhlazeni a Impérium vládne galaxii pevnou rukou. Malá skupina povstalců se odváží vzdorovat a ukradne plány k nejmocnější zbrani Impéria, Hvězdě smrti. Imperátorův nejvěrnější služebník, Darth Vader, musí najít plány a skrytou základnu povstalců. Zpráva o princezně Lei a vůdci rebelů se dostane až k obyčejnému farmáři, Lukovi Skywalkerovi. Ten se řídí svým osudem, zachraňuje princeznu a pomáhá povstalcům svrhnout Impérium společně s takovými nezapomenutelnými spojenci jako: Obi-Wan Kenobi, domýšlivý Han Solo, loajální Chewbacca a droidové R2-D2 a C3PO.", 14, new TimeSpan(0, 2, 1, 0, 0), "Star Wars: Episode IV - New Hope", "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Age", "Name", "PictureUrl", "Surname" },
                values: new object[,]
                {
                    { new Guid("14858480-c954-4424-a549-16e2b0302397"), 75, "George", "https://img.csfd.cz/files/images/creator/photos/000/269/269670_1f4cd0.jpg?w100h132crop", "Lucas" },
                    { new Guid("ed74ba50-f208-49ca-a71a-7bfdca3e1469"), 68, "Mark", "https://img.csfd.cz/files/images/creator/photos/163/523/163523956_9edcf8.jpg?w100h132crop", "Hamill" },
                    { new Guid("cab2ca13-0f8b-4839-a10d-bbedeab84565"), 60, "Carrie", "https://img.csfd.cz/files/images/creator/photos/164/132/164132451_95e211.jpg?w100h132crop", "Fisher" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Nick", "NumericEvaluation", "RatedMovieId", "Review" },
                values: new object[] { new Guid("595fe374-060a-4e06-9201-56c1d61d30a2"), "SithJedi54", 10, new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129"), "Twas AMAZING!" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Ratings");
        }
    }
}
