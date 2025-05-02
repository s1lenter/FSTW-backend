using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTW_backend.Migrations
{
    /// <inheritdoc />
    public partial class RewriteProfileInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Resume");

            migrationBuilder.RenameColumn(
                name: "SocialNet",
                table: "Profile",
                newName: "Vk");

            migrationBuilder.RenameColumn(
                name: "Faculty",
                table: "Profile",
                newName: "Telegram");

            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Profile",
                newName: "Linkedin");

            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "Profile",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "Profile");

            migrationBuilder.RenameColumn(
                name: "Vk",
                table: "Profile",
                newName: "SocialNet");

            migrationBuilder.RenameColumn(
                name: "Telegram",
                table: "Profile",
                newName: "Faculty");

            migrationBuilder.RenameColumn(
                name: "Linkedin",
                table: "Profile",
                newName: "Course");

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "Resume",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
