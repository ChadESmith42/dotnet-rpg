using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    public partial class SkillTypoFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DamageValue",
                table: "Skills",
                newName: "Damage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Damage",
                table: "Skills",
                newName: "DamageValue");
        }
    }
}
