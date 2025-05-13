using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTW_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddArchiveToInternship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isArchive",
                table: "Internship",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isArchive",
                table: "Internship");
        }
    }
}
