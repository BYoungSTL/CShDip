using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShopDB.Migrations
{
    public partial class prodOrdersMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Order_OrdersId",
                table: "ProductsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Product_ProductsId",
                table: "ProductsOrders");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductsOrders",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "ProductsOrders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsOrders_ProductsId",
                table: "ProductsOrders",
                newName: "IX_ProductsOrders_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Order_OrderId",
                table: "ProductsOrders",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Product_ProductId",
                table: "ProductsOrders",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Order_OrderId",
                table: "ProductsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Product_ProductId",
                table: "ProductsOrders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductsOrders",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ProductsOrders",
                newName: "OrdersId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsOrders_ProductId",
                table: "ProductsOrders",
                newName: "IX_ProductsOrders_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Order_OrdersId",
                table: "ProductsOrders",
                column: "OrdersId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Product_ProductsId",
                table: "ProductsOrders",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
