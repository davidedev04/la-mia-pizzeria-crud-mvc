using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_crud_mvc.Migrations
{
    public partial class CreateBridgeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizze_ingredients_IngredientsId",
                table: "Pizze");

            migrationBuilder.DropIndex(
                name: "IX_Pizze_IngredientsId",
                table: "Pizze");

            migrationBuilder.DropColumn(
                name: "IngredientsId",
                table: "Pizze");

            migrationBuilder.CreateTable(
                name: "IngredientsPizza",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    PizzasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsPizza", x => new { x.IngredientsId, x.PizzasId });
                    table.ForeignKey(
                        name: "FK_IngredientsPizza_ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsPizza_Pizze_PizzasId",
                        column: x => x.PizzasId,
                        principalTable: "Pizze",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsPizza_PizzasId",
                table: "IngredientsPizza",
                column: "PizzasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientsPizza");

            migrationBuilder.AddColumn<int>(
                name: "IngredientsId",
                table: "Pizze",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pizze_IngredientsId",
                table: "Pizze",
                column: "IngredientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizze_ingredients_IngredientsId",
                table: "Pizze",
                column: "IngredientsId",
                principalTable: "ingredients",
                principalColumn: "Id");
        }
    }
}
