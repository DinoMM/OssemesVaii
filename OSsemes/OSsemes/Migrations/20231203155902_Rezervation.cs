using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSsemes.Migrations
{
    /// <inheritdoc />
    public partial class Rezervation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberGuest",
                table: "Rezervations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberGuest",
                table: "Rezervations");
        }
    }
}
