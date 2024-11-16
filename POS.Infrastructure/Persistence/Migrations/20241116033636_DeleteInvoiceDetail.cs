using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteInvoiceDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicesDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicesDetails",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    ProductServiceId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesDetails", x => new { x.InvoiceId, x.ProductServiceId });
                    table.ForeignKey(
                        name: "FK_InvoicesDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoceId");
                    table.ForeignKey(
                        name: "FK_InvoicesDetails_ProductServices_ProductServiceId",
                        column: x => x.ProductServiceId,
                        principalTable: "ProductServices",
                        principalColumn: "ProductServiceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesDetails_ProductServiceId",
                table: "InvoicesDetails",
                column: "ProductServiceId");
        }
    }
}
