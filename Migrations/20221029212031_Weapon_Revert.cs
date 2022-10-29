using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    public partial class Weapon_Revert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriptor",
                table: "Weapons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descriptor",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
