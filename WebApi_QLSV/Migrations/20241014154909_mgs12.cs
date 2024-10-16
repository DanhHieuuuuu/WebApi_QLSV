using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class mgs12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QueQuan",
                table: "Teacher",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QueQuan",
                table: "Manager",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueQuan",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "QueQuan",
                table: "Manager");
        }
    }
}
