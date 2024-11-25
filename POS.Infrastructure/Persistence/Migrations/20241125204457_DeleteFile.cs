using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetail_Projects_ProjectId",
                table: "ProjectDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetail_Statuses_StatusId",
                table: "ProjectDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDetail",
                table: "ProjectDetail");

            migrationBuilder.DropColumn(
                name: "File",
                table: "ProjectDetail");

            migrationBuilder.RenameTable(
                name: "ProjectDetail",
                newName: "ProjectDetails");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetail_StatusId",
                table: "ProjectDetails",
                newName: "IX_ProjectDetails_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetail_ProjectId",
                table: "ProjectDetails",
                newName: "IX_ProjectDetails_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDetails",
                table: "ProjectDetails",
                column: "ProjectDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetails_Projects_ProjectId",
                table: "ProjectDetails",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetails_Statuses_StatusId",
                table: "ProjectDetails",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetails_Projects_ProjectId",
                table: "ProjectDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetails_Statuses_StatusId",
                table: "ProjectDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDetails",
                table: "ProjectDetails");

            migrationBuilder.RenameTable(
                name: "ProjectDetails",
                newName: "ProjectDetail");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetails_StatusId",
                table: "ProjectDetail",
                newName: "IX_ProjectDetail_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetails_ProjectId",
                table: "ProjectDetail",
                newName: "IX_ProjectDetail_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "ProjectDetail",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDetail",
                table: "ProjectDetail",
                column: "ProjectDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetail_Projects_ProjectId",
                table: "ProjectDetail",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetail_Statuses_StatusId",
                table: "ProjectDetail",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId");
        }
    }
}
