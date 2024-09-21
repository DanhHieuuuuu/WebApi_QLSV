using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class updatemgs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonHocs_CTKhung_CTKhungId",
                table: "MonHocs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonHocs",
                table: "MonHocs");

            migrationBuilder.RenameTable(
                name: "MonHocs",
                newName: "MonHoc");

            migrationBuilder.RenameIndex(
                name: "IX_MonHocs_CTKhungId",
                table: "MonHoc",
                newName: "IX_MonHoc_CTKhungId");

            migrationBuilder.AddColumn<int>(
                name: "Sotin",
                table: "MonHoc",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonHoc",
                table: "MonHoc",
                column: "MonId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonHoc_CTKhung_CTKhungId",
                table: "MonHoc",
                column: "CTKhungId",
                principalTable: "CTKhung",
                principalColumn: "CTKhungId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonHoc_CTKhung_CTKhungId",
                table: "MonHoc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonHoc",
                table: "MonHoc");

            migrationBuilder.DropColumn(
                name: "Sotin",
                table: "MonHoc");

            migrationBuilder.RenameTable(
                name: "MonHoc",
                newName: "MonHocs");

            migrationBuilder.RenameIndex(
                name: "IX_MonHoc_CTKhungId",
                table: "MonHocs",
                newName: "IX_MonHocs_CTKhungId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonHocs",
                table: "MonHocs",
                column: "MonId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonHocs_CTKhung_CTKhungId",
                table: "MonHocs",
                column: "CTKhungId",
                principalTable: "CTKhung",
                principalColumn: "CTKhungId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
