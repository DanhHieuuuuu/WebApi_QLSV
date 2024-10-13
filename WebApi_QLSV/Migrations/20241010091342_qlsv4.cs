using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class qlsv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LopTruong",
                table: "LopQL",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "LopPho",
                table: "LopQL",
                newName: "LopTruongId");

            migrationBuilder.RenameColumn(
                name: "ChuNhiem",
                table: "LopQL",
                newName: "LopPhoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "LopQL",
                newName: "LopTruong");

            migrationBuilder.RenameColumn(
                name: "LopTruongId",
                table: "LopQL",
                newName: "LopPho");

            migrationBuilder.RenameColumn(
                name: "LopPhoId",
                table: "LopQL",
                newName: "ChuNhiem");
        }
    }
}
