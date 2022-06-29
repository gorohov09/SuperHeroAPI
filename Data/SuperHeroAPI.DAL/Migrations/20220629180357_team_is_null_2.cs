using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    public partial class team_is_null_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes");

            migrationBuilder.AlterColumn<int>(
                name: "SuperHeroTeamId",
                table: "SuperHeroes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes",
                column: "SuperHeroTeamId",
                principalTable: "SuperHeroTeams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes");

            migrationBuilder.AlterColumn<int>(
                name: "SuperHeroTeamId",
                table: "SuperHeroes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes",
                column: "SuperHeroTeamId",
                principalTable: "SuperHeroTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
