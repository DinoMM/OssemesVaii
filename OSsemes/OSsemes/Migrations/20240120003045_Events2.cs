using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSsemes.Migrations
{
    /// <inheritdoc />
    public partial class Events2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHEvents_Events_IdEvent",
                table: "UserHEvents");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "UserHEvents");

            migrationBuilder.RenameColumn(
                name: "IdEvent",
                table: "UserHEvents",
                newName: "HEventID");

            migrationBuilder.RenameIndex(
                name: "IX_UserHEvents_IdEvent",
                table: "UserHEvents",
                newName: "IX_UserHEvents_HEventID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHEvents_Events_HEventID",
                table: "UserHEvents",
                column: "HEventID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHEvents_Events_HEventID",
                table: "UserHEvents");

            migrationBuilder.RenameColumn(
                name: "HEventID",
                table: "UserHEvents",
                newName: "IdEvent");

            migrationBuilder.RenameIndex(
                name: "IX_UserHEvents_HEventID",
                table: "UserHEvents",
                newName: "IX_UserHEvents_IdEvent");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "UserHEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHEvents_Events_IdEvent",
                table: "UserHEvents",
                column: "IdEvent",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
