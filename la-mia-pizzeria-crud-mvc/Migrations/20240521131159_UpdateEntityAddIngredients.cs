using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_crud_mvc.Migrations
{
    public partial class UpdateEntityAddIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngredientsId",
                table: "Pizze",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizze_ingredients_IngredientsId",
                table: "Pizze");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Pizze_IngredientsId",
                table: "Pizze");

            migrationBuilder.DropColumn(
                name: "IngredientsId",
                table: "Pizze");
        }
    }
}
