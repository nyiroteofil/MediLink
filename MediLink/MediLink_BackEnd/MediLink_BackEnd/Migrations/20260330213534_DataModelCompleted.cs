using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLink_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DataModelCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DataSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatientDataSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TAJNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDataSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientDataSheets_DataSheets_Id",
                        column: x => x.Id,
                        principalTable: "DataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalStaffDataSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalStaffDataSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStaffDataSheets_DataSheets_Id",
                        column: x => x.Id,
                        principalTable: "DataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalStaffDataSheets_Institutions_InstitutionID",
                        column: x => x.InstitutionID,
                        principalTable: "Institutions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    DataSheetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Administrators_MedicalStaffDataSheets_DataSheetID",
                        column: x => x.DataSheetID,
                        principalTable: "MedicalStaffDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentRequests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    SpecialistDoctorID = table.Column<int>(type: "int", nullable: false),
                    ReasonOfRequest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReasonOfDenial = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRequests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    SpecialistDoctorID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ReasonOfVisit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appointments_Institutions_InstitutionID",
                        column: x => x.InstitutionID,
                        principalTable: "Institutions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomEvents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEvents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ContactInfoID = table.Column<int>(type: "int", nullable: false),
                    CustomEventID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_ContactInfo_ContactInfoID",
                        column: x => x.ContactInfoID,
                        principalTable: "ContactInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_CustomEvents_CustomEventID",
                        column: x => x.CustomEventID,
                        principalTable: "CustomEvents",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MedicalAsisstants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    DataSheetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAsisstants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedicalAsisstants_MedicalStaffDataSheets_DataSheetID",
                        column: x => x.DataSheetID,
                        principalTable: "MedicalStaffDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalAsisstants_Users_ID",
                        column: x => x.ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    DataSheetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Patients_PatientDataSheets_DataSheetID",
                        column: x => x.DataSheetID,
                        principalTable: "PatientDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Users_ID",
                        column: x => x.ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialistDoctors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    DataSheetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialistDoctors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SpecialistDoctors_MedicalStaffDataSheets_DataSheetID",
                        column: x => x.DataSheetID,
                        principalTable: "MedicalStaffDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialistDoctors_Users_ID",
                        column: x => x.ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "PatientSpecialistDoctor",
                columns: table => new
                {
                    PatientsID = table.Column<int>(type: "int", nullable: false),
                    SpecialistDoctorsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSpecialistDoctor", x => new { x.PatientsID, x.SpecialistDoctorsID });
                    table.ForeignKey(
                        name: "FK_PatientSpecialistDoctor_Patients_PatientsID",
                        column: x => x.PatientsID,
                        principalTable: "Patients",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PatientSpecialistDoctor_SpecialistDoctors_SpecialistDoctorsID",
                        column: x => x.SpecialistDoctorsID,
                        principalTable: "SpecialistDoctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicationForm = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medications_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Referals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialistDoctorID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ReferedInstititionID = table.Column<int>(type: "int", nullable: false),
                    CauseDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    ConsiliumQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsiliumAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CausesInabilityToWork = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Referals_Diagnoses_CauseDiagnosisId",
                        column: x => x.CauseDiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Referals_Institutions_ReferedInstititionID",
                        column: x => x.ReferedInstititionID,
                        principalTable: "Institutions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Referals_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referals_SpecialistDoctors_SpecialistDoctorID",
                        column: x => x.SpecialistDoctorID,
                        principalTable: "SpecialistDoctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "MedicationIngredient",
                columns: table => new
                {
                    IngredientsID = table.Column<int>(type: "int", nullable: false),
                    UsedInMedicationsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationIngredient", x => new { x.IngredientsID, x.UsedInMedicationsID });
                    table.ForeignKey(
                        name: "FK_MedicationIngredient_Ingredient_IngredientsID",
                        column: x => x.IngredientsID,
                        principalTable: "Ingredient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationIngredient_Medications_UsedInMedicationsID",
                        column: x => x.UsedInMedicationsID,
                        principalTable: "Medications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationReminders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    AdministratorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationReminders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedicationReminders_Events_ID",
                        column: x => x.ID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationReminders_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationReminders_Users_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_DataSheetID",
                table: "Administrators",
                column: "DataSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRequests_PatientID",
                table: "AppointmentRequests",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRequests_SpecialistDoctorID",
                table: "AppointmentRequests",
                column: "SpecialistDoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_InstitutionID",
                table: "Appointments",
                column: "InstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientID",
                table: "Appointments",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SpecialistDoctorID",
                table: "Appointments",
                column: "SpecialistDoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_PatientID",
                table: "Diagnoses",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserID",
                table: "Events",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAsisstants_DataSheetID",
                table: "MedicalAsisstants",
                column: "DataSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAsisstantSpecialistDoctor_MedicalAsisstantsID",
                table: "MedicalAsisstantSpecialistDoctor",
                column: "MedicalAsisstantsID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffDataSheets_InstitutionID",
                table: "MedicalStaffDataSheets",
                column: "InstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationIngredient_UsedInMedicationsID",
                table: "MedicationIngredient",
                column: "UsedInMedicationsID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_AdministratorId",
                table: "MedicationReminders",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_MedicationID",
                table: "MedicationReminders",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DiagnosisId",
                table: "Medications",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DataSheetID",
                table: "Patients",
                column: "DataSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSpecialistDoctor_SpecialistDoctorsID",
                table: "PatientSpecialistDoctor",
                column: "SpecialistDoctorsID");

            migrationBuilder.CreateIndex(
                name: "IX_Referals_CauseDiagnosisId",
                table: "Referals",
                column: "CauseDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Referals_PatientID",
                table: "Referals",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Referals_ReferedInstititionID",
                table: "Referals",
                column: "ReferedInstititionID");

            migrationBuilder.CreateIndex(
                name: "IX_Referals_SpecialistDoctorID",
                table: "Referals",
                column: "SpecialistDoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialistDoctors_DataSheetID",
                table: "SpecialistDoctors",
                column: "DataSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_DiagnosisId",
                table: "Symptoms",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactInfoID",
                table: "Users",
                column: "ContactInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomEventID",
                table: "Users",
                column: "CustomEventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Users_ID",
                table: "Administrators",
                column: "ID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRequests_Patients_PatientID",
                table: "AppointmentRequests",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRequests_SpecialistDoctors_SpecialistDoctorID",
                table: "AppointmentRequests",
                column: "SpecialistDoctorID",
                principalTable: "SpecialistDoctors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Events_ID",
                table: "Appointments",
                column: "ID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientID",
                table: "Appointments",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SpecialistDoctors_SpecialistDoctorID",
                table: "Appointments",
                column: "SpecialistDoctorID",
                principalTable: "SpecialistDoctors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomEvents_Events_ID",
                table: "CustomEvents",
                column: "ID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "AppointmentRequests");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "MedicalAsisstantSpecialistDoctor");

            migrationBuilder.DropTable(
                name: "MedicationIngredient");

            migrationBuilder.DropTable(
                name: "MedicationReminders");

            migrationBuilder.DropTable(
                name: "PatientSpecialistDoctor");

            migrationBuilder.DropTable(
                name: "Referals");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "MedicalAsisstants");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "SpecialistDoctors");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "MedicalStaffDataSheets");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "PatientDataSheets");

            migrationBuilder.DropTable(
                name: "DataSheets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "CustomEvents");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
