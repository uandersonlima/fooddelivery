using Microsoft.EntityFrameworkCore.Migrations;

namespace fooddelivery.Migrations
{
    public partial class PatrimonioV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAppetizer",
                table: "Foods",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAppetizer",
                table: "Foods");
        }
    }
}
