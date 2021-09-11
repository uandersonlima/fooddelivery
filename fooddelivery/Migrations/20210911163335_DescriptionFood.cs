using Microsoft.EntityFrameworkCore.Migrations;

namespace fooddelivery.Migrations
{
    public partial class DescriptionFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Foods",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1ul,
                column: "ConcurrencyStamp",
                value: "4b8cd98c-1cbf-45f2-a5bb-7a80eb580922");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2ul,
                column: "ConcurrencyStamp",
                value: "5cc04a5a-d88c-4821-aa47-2e16afdb3f26");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Foods");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1ul,
                column: "ConcurrencyStamp",
                value: "279b3c5e-8ca7-4951-80d9-fa82c4c9f777");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2ul,
                column: "ConcurrencyStamp",
                value: "d285f987-c063-45c7-b92b-40eafe0bdf43");
        }
    }
}
