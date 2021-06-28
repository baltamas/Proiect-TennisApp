using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisApp.Data.Migrations
{
    public partial class AddImgUrlAndDescriptionToPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Player");
        }
    }
}
