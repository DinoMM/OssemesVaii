using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSsemes.Migrations
{
    /// <inheritdoc />
    public partial class UpravaRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomName",
                table: "HRooms",
                newName: "RoomNumber");

            migrationBuilder.AddColumn<string>(
                name: "RoomCategory",
                table: "HRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomCategory",
                table: "HRooms");

            migrationBuilder.RenameColumn(
                name: "RoomNumber",
                table: "HRooms",
                newName: "RoomName");
        }
    }
}
