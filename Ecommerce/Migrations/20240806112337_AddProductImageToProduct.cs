using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class AddProductImageToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Products",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");

         
        }
    }
}
