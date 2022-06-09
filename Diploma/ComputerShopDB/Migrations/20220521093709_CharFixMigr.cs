using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShopDB.Migrations
{
    public partial class CharFixMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCharacteristics");

            migrationBuilder.DropTable(
                name: "ProductsCharacteristics");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Characteristics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ProductId",
                table: "Characteristics",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Product_ProductId",
                table: "Characteristics",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Product_ProductId",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_ProductId",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Characteristics");

            migrationBuilder.CreateTable(
                name: "ProductCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacteristicsId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsCharacteristics",
                columns: table => new
                {
                    CharacteristicsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsCharacteristics", x => new { x.CharacteristicsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductsCharacteristics_Characteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsCharacteristics_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCharacteristics_ProductsId",
                table: "ProductsCharacteristics",
                column: "ProductsId");
        }
    }
}
