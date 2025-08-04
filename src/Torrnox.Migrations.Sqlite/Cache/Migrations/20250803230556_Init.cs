using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Torrnox.Migrations.Sqlite.Cache.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cache",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    ExpireAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cache", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cache_Key",
                table: "cache",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cache");
        }
    }
}
