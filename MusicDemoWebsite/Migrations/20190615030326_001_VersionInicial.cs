using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicDemoWebsite.Migrations
{
    public partial class _001_VersionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtistTable",
                columns: table => new
                {
                    ArtistID = table.Column<Guid>(nullable: false),
                    ArtistName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistTable", x => x.ArtistID);
                });

            migrationBuilder.CreateTable(
                name: "SongTable",
                columns: table => new
                {
                    SongID = table.Column<Guid>(nullable: false),
                    SongName = table.Column<string>(nullable: true),
                    ArtistID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongTable", x => x.SongID);
                    table.ForeignKey(
                        name: "FK_SongTable_ArtistTable_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "ArtistTable",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongTable_ArtistID",
                table: "SongTable",
                column: "ArtistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongTable");

            migrationBuilder.DropTable(
                name: "ArtistTable");
        }
    }
}
