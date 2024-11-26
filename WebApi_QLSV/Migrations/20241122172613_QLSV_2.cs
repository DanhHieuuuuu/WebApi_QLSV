using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class QLSV_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Teacher",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Manager",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Manager");
        }
    }
}
