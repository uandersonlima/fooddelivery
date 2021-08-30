using Microsoft.EntityFrameworkCore.Migrations;

namespace fooddelivery.Migrations
{
    public partial class UpdateRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1ul,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "279b3c5e-8ca7-4951-80d9-fa82c4c9f777", "Administrator" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2ul,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "d285f987-c063-45c7-b92b-40eafe0bdf43", "Common" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1ul,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "762a5ba2-b79c-4337-849f-7c0880593aed", null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2ul,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "3acc3d66-94a0-48ab-8b72-8c4f3764c389", null });
        }
    }
}
