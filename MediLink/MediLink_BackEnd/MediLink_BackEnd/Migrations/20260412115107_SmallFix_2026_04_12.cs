using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLink_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class SmallFix_2026_04_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Diagnoses_DiagnosisId",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Referals_Diagnoses_CauseDiagnosisId",
                table: "Referals");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_Diagnoses_DiagnosisId",
                table: "Symptoms");

            migrationBuilder.RenameColumn(
                name: "DiagnosisId",
                table: "Symptoms",
                newName: "DiagnosisID");

            migrationBuilder.RenameIndex(
                name: "IX_Symptoms_DiagnosisId",
                table: "Symptoms",
                newName: "IX_Symptoms_DiagnosisID");

            migrationBuilder.RenameColumn(
                name: "CauseDiagnosisId",
                table: "Referals",
                newName: "CauseDiagnosisID");

            migrationBuilder.RenameIndex(
                name: "IX_Referals_CauseDiagnosisId",
                table: "Referals",
                newName: "IX_Referals_CauseDiagnosisID");

            migrationBuilder.RenameColumn(
                name: "DiagnosisId",
                table: "Medications",
                newName: "DiagnosisID");

            migrationBuilder.RenameIndex(
                name: "IX_Medications_DiagnosisId",
                table: "Medications",
                newName: "IX_Medications_DiagnosisID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Diagnoses",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Diagnoses_DiagnosisID",
                table: "Medications",
                column: "DiagnosisID",
                principalTable: "Diagnoses",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Referals_Diagnoses_CauseDiagnosisID",
                table: "Referals",
                column: "CauseDiagnosisID",
                principalTable: "Diagnoses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_Diagnoses_DiagnosisID",
                table: "Symptoms",
                column: "DiagnosisID",
                principalTable: "Diagnoses",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Diagnoses_DiagnosisID",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Referals_Diagnoses_CauseDiagnosisID",
                table: "Referals");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_Diagnoses_DiagnosisID",
                table: "Symptoms");

            migrationBuilder.RenameColumn(
                name: "DiagnosisID",
                table: "Symptoms",
                newName: "DiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_Symptoms_DiagnosisID",
                table: "Symptoms",
                newName: "IX_Symptoms_DiagnosisId");

            migrationBuilder.RenameColumn(
                name: "CauseDiagnosisID",
                table: "Referals",
                newName: "CauseDiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_Referals_CauseDiagnosisID",
                table: "Referals",
                newName: "IX_Referals_CauseDiagnosisId");

            migrationBuilder.RenameColumn(
                name: "DiagnosisID",
                table: "Medications",
                newName: "DiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_Medications_DiagnosisID",
                table: "Medications",
                newName: "IX_Medications_DiagnosisId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Diagnoses",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Diagnoses_DiagnosisId",
                table: "Medications",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Referals_Diagnoses_CauseDiagnosisId",
                table: "Referals",
                column: "CauseDiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_Diagnoses_DiagnosisId",
                table: "Symptoms",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id");
        }
    }
}
