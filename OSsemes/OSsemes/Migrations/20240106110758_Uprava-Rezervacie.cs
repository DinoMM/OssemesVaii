using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSsemes.Migrations
{
    /// <inheritdoc />
    public partial class UpravaRezervacie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervations_HRooms_IDroom",
                table: "Rezervations");

            migrationBuilder.DropIndex(
                name: "IX_Rezervations_IDroom",
                table: "Rezervations");

            migrationBuilder.DropColumn(
                name: "IDroom",
                table: "Rezervations");

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "Rezervations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "CelkovaSuma",
                table: "Rezervations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervations_RoomNumber",
                table: "Rezervations",
                column: "RoomNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervations_HRooms_RoomNumber",
                table: "Rezervations",
                column: "RoomNumber",
                principalTable: "HRooms",
                principalColumn: "RoomName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervations_HRooms_RoomNumber",
                table: "Rezervations");

            migrationBuilder.DropIndex(
                name: "IX_Rezervations_RoomNumber",
                table: "Rezervations");

            migrationBuilder.DropColumn(
                name: "CelkovaSuma",
                table: "Rezervations");

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "Rezervations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "IDroom",
                table: "Rezervations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervations_IDroom",
                table: "Rezervations",
                column: "IDroom");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervations_HRooms_IDroom",
                table: "Rezervations",
                column: "IDroom",
                principalTable: "HRooms",
                principalColumn: "RoomName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
