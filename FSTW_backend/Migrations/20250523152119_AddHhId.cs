using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTW_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddHhId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdFromHh",
                table: "Internship",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFromHh",
                table: "Internship");
        }
    }
}
