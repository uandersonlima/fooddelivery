using Microsoft.EntityFrameworkCore.Migrations;

namespace fooddelivery.Migrations
{
    public partial class fooddeliveryV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Images",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Images",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Images");
        }
    }
}
