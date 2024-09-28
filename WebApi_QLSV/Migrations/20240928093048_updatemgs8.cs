using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class updatemgs8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diem",
                table: "ClassStudent");

            migrationBuilder.AddColumn<int>(
                name: "DiemKT",
                table: "ClassStudent",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiemMH",
                table: "ClassStudent",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiemQT",
                table: "ClassStudent",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiemKT",
                table: "ClassStudent");

            migrationBuilder.DropColumn(
                name: "DiemMH",
                table: "ClassStudent");

            migrationBuilder.DropColumn(
                name: "DiemQT",
                table: "ClassStudent");

            migrationBuilder.AddColumn<int>(
                name: "Diem",
                table: "ClassStudent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
