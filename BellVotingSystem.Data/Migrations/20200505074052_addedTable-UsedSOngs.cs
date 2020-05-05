using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BellVotingSystem.WEB.Data.Migrations
{
    public partial class addedTableUsedSOngs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsedSongs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Song = table.Column<string>(nullable: true),
                    ChosenOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedSongs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedSongs");
        }
    }
}
