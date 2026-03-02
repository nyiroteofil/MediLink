using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLink_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class changedMedicineIngredientRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Medications_Medicationid",
                table: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_Medicationid",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Medicationid",
                table: "Ingredient");

            migrationBuilder.CreateTable(
                name: "IngredientMedication",
                columns: table => new
                {
                    ingredientsID = table.Column<int>(type: "int", nullable: false),
                    usedInid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMedication", x => new { x.ingredientsID, x.usedInid });
                    table.ForeignKey(
                        name: "FK_IngredientMedication_Ingredient_ingredientsID",
                        column: x => x.ingredientsID,
                        principalTable: "Ingredient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientMedication_Medications_usedInid",
                        column: x => x.usedInid,
                        principalTable: "Medications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMedication_usedInid",
                table: "IngredientMedication",
                column: "usedInid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientMedication");

            migrationBuilder.AddColumn<int>(
                name: "Medicationid",
                table: "Ingredient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Medicationid",
                table: "Ingredient",
                column: "Medicationid");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Medications_Medicationid",
                table: "Ingredient",
                column: "Medicationid",
                principalTable: "Medications",
                principalColumn: "id");
        }
    }
}
