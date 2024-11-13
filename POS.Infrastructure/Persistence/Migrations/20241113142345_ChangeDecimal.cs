using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesDetail_Invoices_InvoiceId",
                table: "InvoicesDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesDetail_ProductService_ProductServiceId",
                table: "InvoicesDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductService_Categories_CategoryId",
                table: "ProductService");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductService_Units_UnitId",
                table: "ProductService");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_ProductService_ProductServiceId",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteDetails_ProductService_ProductServiceId",
                table: "QuoteDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_ProductService_ProductServiceId",
                table: "SaleDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductService",
                table: "ProductService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoicesDetail",
                table: "InvoicesDetail");

            migrationBuilder.RenameTable(
                name: "ProductService",
                newName: "ProductServices");

            migrationBuilder.RenameTable(
                name: "InvoicesDetail",
                newName: "InvoicesDetails");

            migrationBuilder.RenameIndex(
                name: "IX_ProductService_UnitId",
                table: "ProductServices",
                newName: "IX_ProductServices_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductService_CategoryId",
                table: "ProductServices",
                newName: "IX_ProductServices_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicesDetail_ProductServiceId",
                table: "InvoicesDetails",
                newName: "IX_InvoicesDetails_ProductServiceId");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercent",
                table: "Customers",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditLimit",
                table: "Customers",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditInterestRate",
                table: "Customers",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductServices",
                table: "ProductServices",
                column: "ProductServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoicesDetails",
                table: "InvoicesDetails",
                columns: new[] { "InvoiceId", "ProductServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetails_Invoices_InvoiceId",
                table: "InvoicesDetails",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetails_ProductServices_ProductServiceId",
                table: "InvoicesDetails",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "ProductServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductServices_Categories_CategoryId",
                table: "ProductServices",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductServices_Units_UnitId",
                table: "ProductServices",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_ProductServices_ProductServiceId",
                table: "PurchaseDetails",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "ProductServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteDetails_ProductServices_ProductServiceId",
                table: "QuoteDetails",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "ProductServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_ProductServices_ProductServiceId",
                table: "SaleDetails",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "ProductServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesDetails_Invoices_InvoiceId",
                table: "InvoicesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesDetails_ProductServices_ProductServiceId",
                table: "InvoicesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductServices_Categories_CategoryId",
                table: "ProductServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductServices_Units_UnitId",
                table: "ProductServices");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_ProductServices_ProductServiceId",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteDetails_ProductServices_ProductServiceId",
                table: "QuoteDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_ProductServices_ProductServiceId",
                table: "SaleDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductServices",
                table: "ProductServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoicesDetails",
                table: "InvoicesDetails");

            migrationBuilder.RenameTable(
                name: "ProductServices",
                newName: "ProductService");

            migrationBuilder.RenameTable(
                name: "InvoicesDetails",
                newName: "InvoicesDetail");

            migrationBuilder.RenameIndex(
                name: "IX_ProductServices_UnitId",
                table: "ProductService",
                newName: "IX_ProductService_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductServices_CategoryId",
                table: "ProductService",
                newName: "IX_ProductService_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicesDetails_ProductServiceId",
                table: "InvoicesDetail",
                newName: "IX_InvoicesDetail_ProductServiceId");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercent",
                table: "Customers",
                type: "numeric(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditLimit",
                table: "Customers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditInterestRate",
                table: "Customers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductService",
                table: "ProductService",
                column: "ProductServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoicesDetail",
                table: "InvoicesDetail",
                columns: new[] { "InvoiceId", "ProductServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetail_Invoices_InvoiceId",
                table: "InvoicesDetail",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetail_ProductService_ProductServiceId",
                table: "InvoicesDetail",
                column: "ProductServiceId",
                principalTable: "ProductService",
                principalColumn: "ProductServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductService_Categories_CategoryId",
                table: "ProductService",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductService_Units_UnitId",
                table: "ProductService",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_ProductService_ProductServiceId",
                table: "PurchaseDetails",
                column: "ProductServiceId",
                principalTable: "ProductService",
                principalColumn: "ProductServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteDetails_ProductService_ProductServiceId",
                table: "QuoteDetails",
                column: "ProductServiceId",
                principalTable: "ProductService",
                principalColumn: "ProductServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_ProductService_ProductServiceId",
                table: "SaleDetails",
                column: "ProductServiceId",
                principalTable: "ProductService",
                principalColumn: "ProductServiceId");
        }
    }
}
