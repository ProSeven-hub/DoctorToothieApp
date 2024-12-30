using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorToothieApp.Migrations
{
    /// <inheritdoc />
    public partial class Reservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProcedureTypeId = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcedureNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_ProcedureTypes_ProcedureTypeId",
                        column: x => x.ProcedureTypeId,
                        principalTable: "ProcedureTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_DoctorId",
                table: "Reservation",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_LocationId",
                table: "Reservation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PatientId",
                table: "Reservation",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ProcedureTypeId",
                table: "Reservation",
                column: "ProcedureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RoomId",
                table: "Reservation",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
