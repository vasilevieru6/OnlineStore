using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Models.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Products",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
