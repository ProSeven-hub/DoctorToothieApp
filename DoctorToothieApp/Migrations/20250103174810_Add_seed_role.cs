using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoctorToothieApp.Migrations
{
    /// <inheritdoc />
    public partial class Add_seed_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_CreatedById",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_DoctorId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_PatientId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Locations_LocationId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_ProcedureTypes_ProcedureTypeId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Rooms_RoomId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_RoomId",
                table: "Reservations",
                newName: "IX_Reservations_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_ProcedureTypeId",
                table: "Reservations",
                newName: "IX_Reservations_ProcedureTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_PatientId",
                table: "Reservations",
                newName: "IX_Reservations_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_LocationId",
                table: "Reservations",
                newName: "IX_Reservations_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_DoctorId",
                table: "Reservations",
                newName: "IX_Reservations_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_CreatedById",
                table: "Reservations",
                newName: "IX_Reservations_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1E2B8D51-DA03-4920-B675-E0504ED8E7FF", "19B13701-0451-412B-B80E-6FD559437F53", "Admin", "ADMIN" },
                    { "9F753729-3A0F-4AC0-8130-11E1133A8DF6", "1CA2811D-872D-49AD-8F4B-4364B7D23FBC", "Doctor", "DOCTOR" },
                    { "B3CCBED4-1866-4323-A06B-ED7D3BBDB3C4", "27D16A8F-8C32-4F46-BFEE-2CB8A3AA8B10", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_CreatedById",
                table: "Reservations",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_DoctorId",
                table: "Reservations",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_PatientId",
                table: "Reservations",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Locations_LocationId",
                table: "Reservations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ProcedureTypes_ProcedureTypeId",
                table: "Reservations",
                column: "ProcedureTypeId",
                principalTable: "ProcedureTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_CreatedById",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_DoctorId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_PatientId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Locations_LocationId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ProcedureTypes_ProcedureTypeId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E2B8D51-DA03-4920-B675-E0504ED8E7FF");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9F753729-3A0F-4AC0-8130-11E1133A8DF6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B3CCBED4-1866-4323-A06B-ED7D3BBDB3C4");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservation",
                newName: "IX_Reservation_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ProcedureTypeId",
                table: "Reservation",
                newName: "IX_Reservation_ProcedureTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PatientId",
                table: "Reservation",
                newName: "IX_Reservation_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_LocationId",
                table: "Reservation",
                newName: "IX_Reservation_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_DoctorId",
                table: "Reservation",
                newName: "IX_Reservation_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_CreatedById",
                table: "Reservation",
                newName: "IX_Reservation_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_CreatedById",
                table: "Reservation",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_DoctorId",
                table: "Reservation",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_PatientId",
                table: "Reservation",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Locations_LocationId",
                table: "Reservation",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_ProcedureTypes_ProcedureTypeId",
                table: "Reservation",
                column: "ProcedureTypeId",
                principalTable: "ProcedureTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Rooms_RoomId",
                table: "Reservation",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
