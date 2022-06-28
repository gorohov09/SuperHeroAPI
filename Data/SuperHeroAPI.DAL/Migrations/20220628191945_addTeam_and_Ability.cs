using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    public partial class addTeam_and_Ability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "SuperHeroes");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "SuperHeroes",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "SuperHeroes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "SuperHeroes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RealLastName",
                table: "SuperHeroes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "SuperHeroes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuperHeroTeamId",
                table: "SuperHeroes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "SuperHeroes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperHeroTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreation = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperHeroTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heroes_Abilities",
                columns: table => new
                {
                    AbilitiesId = table.Column<int>(type: "int", nullable: false),
                    SuperHeroesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes_Abilities", x => new { x.AbilitiesId, x.SuperHeroesId });
                    table.ForeignKey(
                        name: "FK_Heroes_Abilities_Abilities_AbilitiesId",
                        column: x => x.AbilitiesId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Heroes_Abilities_SuperHeroes_SuperHeroesId",
                        column: x => x.SuperHeroesId,
                        principalTable: "SuperHeroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperHeroes_SuperHeroTeamId",
                table: "SuperHeroes",
                column: "SuperHeroTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_Abilities_SuperHeroesId",
                table: "Heroes_Abilities",
                column: "SuperHeroesId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes",
                column: "SuperHeroTeamId",
                principalTable: "SuperHeroTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperHeroes_SuperHeroTeams_SuperHeroTeamId",
                table: "SuperHeroes");

            migrationBuilder.DropTable(
                name: "Heroes_Abilities");

            migrationBuilder.DropTable(
                name: "SuperHeroTeams");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropIndex(
                name: "IX_SuperHeroes_SuperHeroTeamId",
                table: "SuperHeroes");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "SuperHeroes");

            migrationBuilder.DropColumn(
                name: "RealLastName",
                table: "SuperHeroes");

            migrationBuilder.DropColumn(
                name: "RealName",
                table: "SuperHeroes");

            migrationBuilder.DropColumn(
                name: "SuperHeroTeamId",
                table: "SuperHeroes");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "SuperHeroes");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "SuperHeroes",
                newName: "LastName");

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "SuperHeroes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "SuperHeroes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
