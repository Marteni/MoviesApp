using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesApp.DAL.Migrations
{
    public partial class AddTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_ActedInMovieId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Movies_DirectedMovieId",
                table: "Directors");

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CountryOfOrigin", "CzechTitle", "Description", "Genre", "Length", "OriginalTitle", "PosterImageUrl" },
                values: new object[] { new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129"), "USA", "Star Wars: Epizoda IV - Nová naděje", "Rytíři Jedi byli vyhlazeni a Impérium vládne galaxii pevnou rukou. Malá skupina povstalců se odváží vzdorovat a ukradne plány k nejmocnější zbrani Impéria, Hvězdě smrti. Imperátorův nejvěrnější služebník, Darth Vader, musí najít plány a skrytou základnu povstalců. Zpráva o princezně Lei a vůdci rebelů se dostane až k obyčejnému farmáři, Lukovi Skywalkerovi. Ten se řídí svým osudem, zachraňuje princeznu a pomáhá povstalcům svrhnout Impérium společně s takovými nezapomenutelnými spojenci jako: Obi-Wan Kenobi, domýšlivý Han Solo, loajální Chewbacca a droidové R2-D2 a C3PO.", 14, new TimeSpan(0, 2, 1, 0, 0), "Star Wars: Episode IV - New Hope", "https://img.csfd.cz/files/images/film/posters/162/398/162398464_ac2bec.jpg?h180" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Age", "Name", "PictureUrl", "Surname" },
                values: new object[,]
                {
                    { new Guid("14858480-c954-4424-a549-16e2b0302397"), 75, "George", "https://img.csfd.cz/files/images/creator/photos/000/269/269670_1f4cd0.jpg?w100h132crop", "George" },
                    { new Guid("ed74ba50-f208-49ca-a71a-7bfdca3e1469"), 68, "Mark", "https://img.csfd.cz/files/images/creator/photos/163/523/163523956_9edcf8.jpg?w100h132crop", "Hamill" },
                    { new Guid("cab2ca13-0f8b-4839-a10d-bbedeab84565"), 60, "Carrie", "https://img.csfd.cz/files/images/creator/photos/164/132/164132451_95e211.jpg?w100h132crop", "Fisher" }
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "ActedInMovieId", "ActorId", "MovieId" },
                values: new object[,]
                {
                    { new Guid("12e1ce4d-2c8c-4bce-b610-129a784eb03b"), null, new Guid("ed74ba50-f208-49ca-a71a-7bfdca3e1469"), new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129") },
                    { new Guid("7648ebfa-9e58-4835-a237-e9e7b6387262"), null, new Guid("cab2ca13-0f8b-4839-a10d-bbedeab84565"), new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129") }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "DirectedMovieId", "DirectorId", "MovieId" },
                values: new object[] { new Guid("19e3745e-9259-471a-890b-f6db55d48f24"), null, new Guid("14858480-c954-4424-a549-16e2b0302397"), new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129") });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Nick", "NumericEvaluation", "RatedMovieId", "Review" },
                values: new object[] { new Guid("595fe374-060a-4e06-9201-56c1d61d30a2"), "SithJedi54", 10, new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129"), "Twas AMAZING!" });

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_ActedInMovieId",
                table: "Actors",
                column: "ActedInMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Movies_DirectedMovieId",
                table: "Directors",
                column: "DirectedMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_ActedInMovieId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Movies_DirectedMovieId",
                table: "Directors");

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("12e1ce4d-2c8c-4bce-b610-129a784eb03b"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("7648ebfa-9e58-4835-a237-e9e7b6387262"));

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("19e3745e-9259-471a-890b-f6db55d48f24"));

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: new Guid("595fe374-060a-4e06-9201-56c1d61d30a2"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("0302a349-ffc2-429f-bc1c-8ad64fb77129"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("14858480-c954-4424-a549-16e2b0302397"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("cab2ca13-0f8b-4839-a10d-bbedeab84565"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("ed74ba50-f208-49ca-a71a-7bfdca3e1469"));

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_ActedInMovieId",
                table: "Actors",
                column: "ActedInMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Movies_DirectedMovieId",
                table: "Directors",
                column: "DirectedMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
