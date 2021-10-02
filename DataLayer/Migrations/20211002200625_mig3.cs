using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkAmounts",
                table: "DrinkAmounts");

            migrationBuilder.RenameTable(
                name: "DrinkAmounts",
                newName: "DrinkAvailabilities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkAvailabilities",
                table: "DrinkAvailabilities",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkAvailabilities",
                table: "DrinkAvailabilities");

            migrationBuilder.RenameTable(
                name: "DrinkAvailabilities",
                newName: "DrinkAmounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkAmounts",
                table: "DrinkAmounts",
                column: "Id");
        }
    }
}
