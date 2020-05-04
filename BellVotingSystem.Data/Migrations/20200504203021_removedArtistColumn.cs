using Microsoft.EntityFrameworkCore.Migrations;

namespace BellVotingSystem.WEB.Data.Migrations
{
    public partial class removedArtistColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Artist",
                table: "BlacklistedSongs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Entries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "BlacklistedSongs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
