using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSsemes.Migrations
{
    /// <inheritdoc />
    public partial class pridanieRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IDroom",
                table: "Rezervations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervations_HRooms_IDroom",
                table: "Rezervations");

            migrationBuilder.DropIndex(
                name: "IX_Rezervations_IDroom",
                table: "Rezervations");

            migrationBuilder.AlterColumn<string>(
                name: "IDroom",
                table: "Rezervations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
