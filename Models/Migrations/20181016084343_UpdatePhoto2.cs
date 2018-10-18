using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Models.Migrations
{
    public partial class UpdatePhoto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Photos",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "Photos",
                newName: "Path");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Photos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Photos",
                newName: "Extension");
        }
    }
}
