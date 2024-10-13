using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class qlsv8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sotin",
                table: "MonHoc",
                newName: "SoTin");

            migrationBuilder.RenameColumn(
                name: "MaHocPhan",
                table: "MonHoc",
                newName: "MaMonHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoTin",
                table: "MonHoc",
                newName: "Sotin");

            migrationBuilder.RenameColumn(
                name: "MaMonHoc",
                table: "MonHoc",
                newName: "MaHocPhan");
        }
    }
}
