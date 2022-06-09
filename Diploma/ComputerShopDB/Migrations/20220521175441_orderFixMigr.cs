using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShopDB.Migrations
{
    public partial class orderFixMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_Id",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_Id",
                table: "Order",
                column: "Id",
                unique: true);
        }
    }
}
