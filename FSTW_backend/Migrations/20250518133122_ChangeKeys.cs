using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTW_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HelperChatHistory_UserId",
                table: "HelperChatHistory");

            migrationBuilder.DropIndex(
                name: "IX_ChatHistory_UserId",
                table: "ChatHistory");

            migrationBuilder.CreateIndex(
                name: "IX_HelperChatHistory_UserId",
                table: "HelperChatHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_UserId",
                table: "ChatHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HelperChatHistory_UserId",
                table: "HelperChatHistory");

            migrationBuilder.DropIndex(
                name: "IX_ChatHistory_UserId",
                table: "ChatHistory");

            migrationBuilder.CreateIndex(
                name: "IX_HelperChatHistory_UserId",
                table: "HelperChatHistory",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_UserId",
                table: "ChatHistory",
                column: "UserId",
                unique: true);
        }
    }
}
