using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Torrnox.Migrations.Npgsql.Entity.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Adult = table.Column<bool>(type: "boolean", nullable: false),
                    BackdropPath = table.Column<string>(type: "text", nullable: false),
                    OriginalLanguage = table.Column<string>(type: "text", nullable: false),
                    OriginalTitle = table.Column<string>(type: "text", nullable: false),
                    Overview = table.Column<string>(type: "text", nullable: false),
                    Popularity = table.Column<double>(type: "double precision", nullable: false),
                    PosterPath = table.Column<string>(type: "text", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Video = table.Column<bool>(type: "boolean", nullable: false),
                    VoteAverage = table.Column<double>(type: "double precision", nullable: false),
                    VoteCount = table.Column<int>(type: "integer", nullable: false),
                    ExternalId = table.Column<int>(type: "integer", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "movies_genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MovieEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExternalId = table.Column<int>(type: "integer", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies_genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movies_genres_movies_MovieEntityId",
                        column: x => x.MovieEntityId,
                        principalTable: "movies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_movies_ExternalId_Language",
                table: "movies",
                columns: new[] { "ExternalId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movies_genres_ExternalId_Language",
                table: "movies_genres",
                columns: new[] { "ExternalId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movies_genres_MovieEntityId",
                table: "movies_genres",
                column: "MovieEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movies_genres");

            migrationBuilder.DropTable(
                name: "movies");
        }
    }
}
