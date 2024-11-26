using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStatusIdToStateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetails_Statuses_StatusId",
                table: "ProjectDetails");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ProjectDetails",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetails_StatusId",
                table: "ProjectDetails",
                newName: "IX_ProjectDetails_StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetails_Statuses_StateId",
                table: "ProjectDetails",
                column: "StateId",
                principalTable: "Statuses",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetails_Statuses_StateId",
                table: "ProjectDetails");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "ProjectDetails",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetails_StateId",
                table: "ProjectDetails",
                newName: "IX_ProjectDetails_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetails_Statuses_StatusId",
                table: "ProjectDetails",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId");
        }
    }
}
