using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Mark.Database.Migrations
{
    /// <inheritdoc />
    public partial class addedTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserAccounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Treasury",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Taxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Journals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Currencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "ChartOfAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Banks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransactionOperations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    OperationCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionOperations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionOperations");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Treasury");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "ChartOfAccount");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
