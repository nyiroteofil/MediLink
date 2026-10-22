using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLink_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class FixedTableTypoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalAsisstantSpecialistDoctor");

            migrationBuilder.CreateTable(
                name: "MedicalAssistantSpecialistDoctor",
                columns: table => new
                {
                    AttendingDoctorsID = table.Column<int>(type: "int", nullable: false),
                    MedicalAsisstantsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAssistantSpecialistDoctor", x => new { x.AttendingDoctorsID, x.MedicalAsisstantsID });
                    table.ForeignKey(
                        name: "FK_MedicalAssistantSpecialistDoctor_MedicalAsisstants_MedicalAsisstantsID",
                        column: x => x.MedicalAsisstantsID,
                        principalTable: "MedicalAsisstants",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedicalAssistantSpecialistDoctor_SpecialistDoctors_AttendingDoctorsID",
                        column: x => x.AttendingDoctorsID,
                        principalTable: "SpecialistDoctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAssistantSpecialistDoctor_MedicalAsisstantsID",
                table: "MedicalAssistantSpecialistDoctor",
                column: "MedicalAsisstantsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalAssistantSpecialistDoctor");

            migrationBuilder.CreateTable(
                name: "MedicalAsisstantSpecialistDoctor",
                columns: table => new
                {
                    AttendingDoctorsID = table.Column<int>(type: "int", nullable: false),
                    MedicalAsisstantsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAsisstantSpecialistDoctor", x => new { x.AttendingDoctorsID, x.MedicalAsisstantsID });
                    table.ForeignKey(
                        name: "FK_MedicalAsisstantSpecialistDoctor_MedicalAsisstants_MedicalAsisstantsID",
                        column: x => x.MedicalAsisstantsID,
                        principalTable: "MedicalAsisstants",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedicalAsisstantSpecialistDoctor_SpecialistDoctors_AttendingDoctorsID",
                        column: x => x.AttendingDoctorsID,
                        principalTable: "SpecialistDoctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAsisstantSpecialistDoctor_MedicalAsisstantsID",
                table: "MedicalAsisstantSpecialistDoctor",
                column: "MedicalAsisstantsID");
        }
    }
}
