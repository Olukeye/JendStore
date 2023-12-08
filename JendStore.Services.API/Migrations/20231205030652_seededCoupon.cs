using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JendStore.Services.API.Migrations
{
    /// <inheritdoc />
    public partial class seededCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Code", "Discount", "MinAmount" },
                values: new object[] { 1, "10OFF", 10.0, 15 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1);
        }
    }
}
