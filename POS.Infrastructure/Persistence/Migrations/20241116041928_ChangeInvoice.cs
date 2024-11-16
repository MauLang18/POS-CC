using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyIVA",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Invoices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplyIVA",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Invoices",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Invoices",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Invoices",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
