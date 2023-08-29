using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bangazon.Migrations
{
    public partial class produpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Products_ProductId",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_ProductId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductTypes");

            migrationBuilder.RenameColumn(
                name: "productType",
                table: "Products",
                newName: "productTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_productTypeId",
                table: "Products",
                column: "productTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_productTypeId",
                table: "Products",
                column: "productTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_productTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_productTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "productTypeId",
                table: "Products",
                newName: "productType");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductId",
                table: "ProductTypes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_Products_ProductId",
                table: "ProductTypes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
