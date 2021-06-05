using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisApp.Data.Migrations
{
    public partial class MatchDependenciesNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_Dep1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_Dep2Id",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "Dep2Id",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Dep1Id",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "Dep2Id",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Dep1Id",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_Dep1Id",
                table: "Matches",
                column: "Dep1Id",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_Dep2Id",
                table: "Matches",
                column: "Dep2Id",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
