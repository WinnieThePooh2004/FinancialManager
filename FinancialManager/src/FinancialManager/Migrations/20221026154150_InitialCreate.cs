using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialManager.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsIncome = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypes", x => x.Id);
                });

            migrationBuilder.Sql(
                "SET IDENTITY_INSERT FinancialOperations ON\r\n" +
                "INSERT INTO FinancialOperations\r\n" +
                "([ID], [DateTime], [Description], [Amount], [OperationTypeId])\r\n" +
                "VALUES\r\n(32, '2008-05-05T19:58:47', 'Got salary', 1300, 123),\r\n" +
                "(33, '2022-11-02T19:58:47', 'Played bowling with friend', 1600, 124),\r\n" +
                "(34, '2007-06-02T19:58:47', 'Sold car', 1700, 125),\r\n" +
                "(35, '2011-05-02T19:58:47', 'Bought new car', 1900, 126),\r\n" +
                "(36, '2012-07-02T19:58:47', 'Rent fee', 1981, 127)\r\n" +
                "SET IDENTITY_INSERT FinancialOperations OFF\r\n" +
                "SET IDENTITY_INSERT OperationTypes ON\r\n" +
                "INSERT INTO OperationTypes\r\n" +
                "([ID], [Name], [IsIncome])\r\n" +
                "VALUES\r\n" +
                "(123, 'Salary', 1),\r\n" +
                "(124, 'Sports', 0),\r\n" +
                "(125, 'Sold something', 1),\r\n" +
                "(126, 'Buy something', 0),\r\n" +
                "(127, 'Rent', 0)\r\n" +
                "SET IDENTITY_INSERT OperationTypes OFF\r\n"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialOperations");

            migrationBuilder.DropTable(
                name: "OperationTypes");
        }
    }
}
