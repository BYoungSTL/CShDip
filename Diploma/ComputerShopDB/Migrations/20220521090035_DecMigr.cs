using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShopDB.Migrations
{
    public partial class DecMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Order");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Product",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Order",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id");
        }
    }
}
