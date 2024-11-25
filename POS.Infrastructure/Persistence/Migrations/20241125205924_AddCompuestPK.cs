using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace POS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCompuestPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDetails",
                table: "ProjectDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDetails_ProjectId",
                table: "ProjectDetails");

            migrationBuilder.RenameColumn(
                name: "ProjectDetailId",
                table: "ProjectDetails",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProjectDetails",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDetails",
                table: "ProjectDetails",
                columns: new[] { "ProjectId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDetails",
                table: "ProjectDetails");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProjectDetails",
                newName: "ProjectDetailId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectDetailId",
                table: "ProjectDetails",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDetails",
                table: "ProjectDetails",
                column: "ProjectDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProjectId",
                table: "ProjectDetails",
                column: "ProjectId");
        }
    }
}
