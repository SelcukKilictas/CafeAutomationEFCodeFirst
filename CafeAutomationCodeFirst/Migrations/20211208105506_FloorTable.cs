using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeAutomationCodeFirst.Migrations
{
    public partial class FloorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorId",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_FloorId",
                table: "Tables",
                column: "FloorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Floors_FloorId",
                table: "Tables",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Floors_FloorId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_FloorId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "Tables");
        }
    }
}
