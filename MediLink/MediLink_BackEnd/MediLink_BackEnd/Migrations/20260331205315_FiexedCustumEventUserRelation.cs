using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLink_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class FiexedCustumEventUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CustomEvents_CustomEventID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomEventID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CustomEventID",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "CustomEventAttendees",
                columns: table => new
                {
                    AttnedeesID = table.Column<int>(type: "int", nullable: false),
                    CustomEventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEventAttendees", x => new { x.AttnedeesID, x.CustomEventID });
                    table.ForeignKey(
                        name: "FK_CustomEventAttendees_CustomEvents_CustomEventID",
                        column: x => x.CustomEventID,
                        principalTable: "CustomEvents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomEventAttendees_Users_AttnedeesID",
                        column: x => x.AttnedeesID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomEventAttendees_CustomEventID",
                table: "CustomEventAttendees",
                column: "CustomEventID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomEventAttendees");

            migrationBuilder.AddColumn<int>(
                name: "CustomEventID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomEventID",
                table: "Users",
                column: "CustomEventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CustomEvents_CustomEventID",
                table: "Users",
                column: "CustomEventID",
                principalTable: "CustomEvents",
                principalColumn: "ID");
        }
    }
}
