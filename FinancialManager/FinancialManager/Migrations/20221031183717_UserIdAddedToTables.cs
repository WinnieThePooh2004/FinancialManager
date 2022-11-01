using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialManager.Migrations
{
    public partial class UserIdAddedToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsertId",
                table: "OperationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FinancialOperations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsertId",
                table: "OperationTypes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FinancialOperations");
        }
    }
}
