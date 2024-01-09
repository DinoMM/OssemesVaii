using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSsemes.Migrations
{
    /// <inheritdoc />
    public partial class pridanieKuponDoRezervacie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouponId",
                table: "Rezervations",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervations_CouponId",
                table: "Rezervations",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervations_Coupons_CouponId",
                table: "Rezervations",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervations_Coupons_CouponId",
                table: "Rezervations");

            migrationBuilder.DropIndex(
                name: "IX_Rezervations_CouponId",
                table: "Rezervations");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Rezervations");
        }
    }
}
