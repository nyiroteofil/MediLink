using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLink_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DataSheetInstitutionFKOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStaffDataSheets_Institutions_InstitutionID",
                table: "MedicalStaffDataSheets");

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionID",
                table: "MedicalStaffDataSheets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStaffDataSheets_Institutions_InstitutionID",
                table: "MedicalStaffDataSheets",
                column: "InstitutionID",
                principalTable: "Institutions",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStaffDataSheets_Institutions_InstitutionID",
                table: "MedicalStaffDataSheets");

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionID",
                table: "MedicalStaffDataSheets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStaffDataSheets_Institutions_InstitutionID",
                table: "MedicalStaffDataSheets",
                column: "InstitutionID",
                principalTable: "Institutions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
