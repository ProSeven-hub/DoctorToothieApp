using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorToothieApp.Migrations
{
    /// <inheritdoc />
    public partial class Add_creator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CreatedById",
                table: "Reservation",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_CreatedById",
                table: "Reservation",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_CreatedById",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CreatedById",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Reservation");
        }
    }
}
