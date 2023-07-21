using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Mark.Database.Migrations
{
    /// <inheritdoc />
    public partial class addedbalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Vendors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Treasury",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Taxes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Customers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Treasury");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Customers");
        }
    }
}
