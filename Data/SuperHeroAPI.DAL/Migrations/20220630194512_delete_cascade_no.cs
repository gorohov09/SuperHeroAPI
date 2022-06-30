using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    public partial class delete_cascade_no : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes",
                column: "SuperHeroTeamId",
                principalTable: "SuperHeroTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes",
                column: "SuperHeroTeamId",
                principalTable: "SuperHeroTeams",
                principalColumn: "Id");
        }
    }
}
