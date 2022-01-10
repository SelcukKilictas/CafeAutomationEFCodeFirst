using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeAutomationCodeFirst.Migrations
{
    public partial class AddTableCountInFloor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableCount",
                table: "Floors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableCount",
                table: "Floors");
        }
    }
}
