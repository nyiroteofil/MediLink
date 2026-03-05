using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLink_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DataModels_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiagnosisId",
                table: "Medications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    SpecialistDoctorId = table.Column<int>(type: "int", nullable: false),
                    ReasonOfRequest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReasonOfDenial = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SpecialistDoctorId = table.Column<int>(type: "int", nullable: false),
                    ReasonOfVisit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceOfVisitId = table.Column<int>(type: "int", nullable: false),
                    AdministratorId = table.Column<int>(type: "int", nullable: true),
                    MedicalAsisstantId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Administrators_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Administrators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Institutions_PlaceOfVisitId",
                        column: x => x.PlaceOfVisitId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdministratorId = table.Column<int>(type: "int", nullable: true),
                    MedicalAsisstantId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    SpecialistDoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomEvents_Administrators_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Administrators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ContactInfoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomEventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_ContactInfo_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_CustomEvents_CustomEventId",
                        column: x => x.CustomEventId,
                        principalTable: "CustomEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataSheetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_PatientDataSheets_DataSheetId",
                        column: x => x.DataSheetId,
                        principalTable: "PatientDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialistDoctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataSheetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialistDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialistDoctors_MedicalStaffDataSheets_DataSheetId",
                        column: x => x.DataSheetId,
                        principalTable: "MedicalStaffDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialistDoctors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAsisstants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AttendingDoctorId = table.Column<int>(type: "int", nullable: false),
                    DataSheetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAsisstants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAsisstants_MedicalStaffDataSheets_DataSheetId",
                        column: x => x.DataSheetId,
                        principalTable: "MedicalStaffDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalAsisstants_SpecialistDoctors_AttendingDoctorId",
                        column: x => x.AttendingDoctorId,
                        principalTable: "SpecialistDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalAsisstants_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientSpecialistDoctor",
                columns: table => new
                {
                    PatientsId = table.Column<int>(type: "int", nullable: false),
                    SpecialistDoctorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSpecialistDoctor", x => new { x.PatientsId, x.SpecialistDoctorsId });
                    table.ForeignKey(
                        name: "FK_PatientSpecialistDoctor_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientSpecialistDoctor_SpecialistDoctors_SpecialistDoctorsId",
                        column: x => x.SpecialistDoctorsId,
                        principalTable: "SpecialistDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Referals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssuingDoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ReferedInstititionId = table.Column<int>(type: "int", nullable: false),
                    CauseDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    ConsiliumQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsiliumAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CausesInabilityToWork = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referals_Diagnoses_CauseDiagnosisId",
                        column: x => x.CauseDiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Referals_Institutions_ReferedInstititionId",
                        column: x => x.ReferedInstititionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Referals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Referals_SpecialistDoctors_IssuingDoctorId",
                        column: x => x.IssuingDoctorId,
                        principalTable: "SpecialistDoctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    DiagnosisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symptoms_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicationReminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    AdministratorOfMedicineId = table.Column<int>(type: "int", nullable: false),
                    AdministratorId = table.Column<int>(type: "int", nullable: true),
                    MedicalAsisstantId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    SpecialistDoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationReminders_Administrators_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Administrators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicationReminders_Events_Id",
                        column: x => x.Id,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationReminders_MedicalAsisstants_MedicalAsisstantId",
                        column: x => x.MedicalAsisstantId,
                        principalTable: "MedicalAsisstants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicationReminders_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationReminders_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicationReminders_SpecialistDoctors_SpecialistDoctorId",
                        column: x => x.SpecialistDoctorId,
                        principalTable: "SpecialistDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicationReminders_Users_AdministratorOfMedicineId",
                        column: x => x.AdministratorOfMedicineId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DiagnosisId",
                table: "Medications",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRequests_PatientId",
                table: "AppointmentRequests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRequests_SpecialistDoctorId",
                table: "AppointmentRequests",
                column: "SpecialistDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AdministratorId",
                table: "Appointments",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalAsisstantId",
                table: "Appointments",
                column: "MedicalAsisstantId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PlaceOfVisitId",
                table: "Appointments",
                column: "PlaceOfVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SpecialistDoctorId",
                table: "Appointments",
                column: "SpecialistDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEvents_AdministratorId",
                table: "CustomEvents",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEvents_MedicalAsisstantId",
                table: "CustomEvents",
                column: "MedicalAsisstantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEvents_PatientId",
                table: "CustomEvents",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEvents_SpecialistDoctorId",
                table: "CustomEvents",
                column: "SpecialistDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_PatientId",
                table: "Diagnoses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAsisstants_AttendingDoctorId",
                table: "MedicalAsisstants",
                column: "AttendingDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAsisstants_DataSheetId",
                table: "MedicalAsisstants",
                column: "DataSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_AdministratorId",
                table: "MedicationReminders",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_AdministratorOfMedicineId",
                table: "MedicationReminders",
                column: "AdministratorOfMedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_MedicalAsisstantId",
                table: "MedicationReminders",
                column: "MedicalAsisstantId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_MedicationId",
                table: "MedicationReminders",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_PatientId",
                table: "MedicationReminders",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_SpecialistDoctorId",
                table: "MedicationReminders",
                column: "SpecialistDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DataSheetId",
                table: "Patients",
                column: "DataSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSpecialistDoctor_SpecialistDoctorsId",
                table: "PatientSpecialistDoctor",
                column: "SpecialistDoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Referals_CauseDiagnosisId",
                table: "Referals",
                column: "CauseDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Referals_IssuingDoctorId",
                table: "Referals",
                column: "IssuingDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Referals_PatientId",
                table: "Referals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Referals_ReferedInstititionId",
                table: "Referals",
                column: "ReferedInstititionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialistDoctors_DataSheetId",
                table: "SpecialistDoctors",
                column: "DataSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_DiagnosisId",
                table: "Symptoms",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactInfoId",
                table: "Users",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomEventId",
                table: "Users",
                column: "CustomEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Diagnoses_DiagnosisId",
                table: "Medications",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Users_Id",
                table: "Administrators",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRequests_Patients_PatientId",
                table: "AppointmentRequests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRequests_SpecialistDoctors_SpecialistDoctorId",
                table: "AppointmentRequests",
                column: "SpecialistDoctorId",
                principalTable: "SpecialistDoctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Events_Id",
                table: "Appointments",
                column: "Id",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MedicalAsisstants_MedicalAsisstantId",
                table: "Appointments",
                column: "MedicalAsisstantId",
                principalTable: "MedicalAsisstants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SpecialistDoctors_SpecialistDoctorId",
                table: "Appointments",
                column: "SpecialistDoctorId",
                principalTable: "SpecialistDoctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomEvents_Events_Id",
                table: "CustomEvents",
                column: "Id",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomEvents_MedicalAsisstants_MedicalAsisstantId",
                table: "CustomEvents",
                column: "MedicalAsisstantId",
                principalTable: "MedicalAsisstants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomEvents_Patients_PatientId",
                table: "CustomEvents",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomEvents_SpecialistDoctors_SpecialistDoctorId",
                table: "CustomEvents",
                column: "SpecialistDoctorId",
                principalTable: "SpecialistDoctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Diagnoses_DiagnosisId",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Users_Id",
                table: "Administrators");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalAsisstants_Users_Id",
                table: "MedicalAsisstants");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Users_Id",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialistDoctors_Users_Id",
                table: "SpecialistDoctors");

            migrationBuilder.DropTable(
                name: "AppointmentRequests");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "MedicationReminders");

            migrationBuilder.DropTable(
                name: "PatientSpecialistDoctor");

            migrationBuilder.DropTable(
                name: "Referals");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "CustomEvents");

            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "MedicalAsisstants");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "SpecialistDoctors");

            migrationBuilder.DropIndex(
                name: "IX_Medications_DiagnosisId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "Medications");
        }
    }
}
