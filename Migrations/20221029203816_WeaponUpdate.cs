using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    public partial class WeaponUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descriptor",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriptor",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Weapons");
        }
    }
}
