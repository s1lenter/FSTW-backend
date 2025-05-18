using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTW_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeys1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatHistory_ResumeId",
                table: "ChatHistory");

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_ResumeId",
                table: "ChatHistory",
                column: "ResumeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatHistory_ResumeId",
                table: "ChatHistory");

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_ResumeId",
                table: "ChatHistory",
                column: "ResumeId",
                unique: true);
        }
    }
}
