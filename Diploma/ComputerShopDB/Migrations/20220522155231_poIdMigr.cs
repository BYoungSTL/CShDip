using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShopDB.Migrations
{
    public partial class poIdMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductsOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsOrders_OrderId",
                table: "ProductsOrders",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductsOrders_OrderId",
                table: "ProductsOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductsOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders",
                columns: new[] { "OrderId", "ProductId" });
        }
    }
}
