using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystem.Migrations
{
    /// <inheritdoc />
    public partial class initialData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arabic",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "English",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Math",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Science",
                table: "Subjects");

            migrationBuilder.AddColumn<string>(
                name: "SubjectName",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectName",
                table: "Subjects");

            migrationBuilder.AddColumn<bool>(
                name: "Arabic",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "English",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Math",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Science",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
