using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTW_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Profile",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocialNet",
                table: "Profile",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "SocialNet",
                table: "Profile");
        }
    }
}
