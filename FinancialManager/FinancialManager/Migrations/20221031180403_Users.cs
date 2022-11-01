using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialManager.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsUsers",
                table: "UsUsers");

            migrationBuilder.RenameTable(
                name: "UsUsers",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UsUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsUsers",
                table: "UsUsers",
                column: "Id");
        }
    }
}
