using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Quotes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_PaymentMethodId",
                table: "Quotes",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_PaymentMethods_PaymentMethodId",
                table: "Quotes",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "PaymentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_PaymentMethods_PaymentMethodId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_PaymentMethodId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Customers");
        }
    }
}
