using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FSTW_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChatMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "ChatHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ChatHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HelperChatHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelperChatHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelperChatHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_ResumeId",
                table: "ChatHistory",
                column: "ResumeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_UserId",
                table: "ChatHistory",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HelperChatHistory_UserId",
                table: "HelperChatHistory",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatHistory_Resume_ResumeId",
                table: "ChatHistory",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatHistory_User_UserId",
                table: "ChatHistory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatHistory_Resume_ResumeId",
                table: "ChatHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatHistory_User_UserId",
                table: "ChatHistory");

            migrationBuilder.DropTable(
                name: "HelperChatHistory");

            migrationBuilder.DropIndex(
                name: "IX_ChatHistory_ResumeId",
                table: "ChatHistory");

            migrationBuilder.DropIndex(
                name: "IX_ChatHistory_UserId",
                table: "ChatHistory");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "ChatHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatHistory");
        }
    }
}
