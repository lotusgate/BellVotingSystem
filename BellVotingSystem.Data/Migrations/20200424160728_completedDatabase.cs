using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BellVotingSystem.WEB.Data.Migrations
{
    public partial class completedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ChosenOn",
                table: "Entries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsBlacklisted",
                table: "Entries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasVoted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChosenOn",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "IsBlacklisted",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "HasVoted",
                table: "AspNetUsers");
        }
    }
}
