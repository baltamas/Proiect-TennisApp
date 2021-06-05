using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisApp.Data.Migrations
{
    public partial class MatchDependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dep1Id",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dep2Id",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Dep1Id",
                table: "Matches",
                column: "Dep1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Dep2Id",
                table: "Matches",
                column: "Dep2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_Dep1Id",
                table: "Matches",
                column: "Dep1Id",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_Dep2Id",
                table: "Matches",
                column: "Dep2Id",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_Dep1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_Dep2Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Dep1Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Dep2Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Dep1Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Dep2Id",
                table: "Matches");
        }
    }
}
