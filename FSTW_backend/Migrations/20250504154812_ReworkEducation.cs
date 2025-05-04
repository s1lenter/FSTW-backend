using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTW_backend.Migrations
{
    /// <inheritdoc />
    public partial class ReworkEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearOfGraduation",
                table: "Education",
                newName: "EndYear");

            migrationBuilder.RenameColumn(
                name: "Institute",
                table: "Education",
                newName: "Specialization");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Education",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Education",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Education");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Education",
                newName: "Institute");

            migrationBuilder.RenameColumn(
                name: "EndYear",
                table: "Education",
                newName: "YearOfGraduation");
        }
    }
}
