using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class qlsv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nganh_CTKhung_NganhId",
                table: "Nganh");

            migrationBuilder.AlterColumn<string>(
                name: "NganhId",
                table: "CTKhung",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CTKhung_NganhId",
                table: "CTKhung",
                column: "NganhId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CTKhung_Nganh_NganhId",
                table: "CTKhung",
                column: "NganhId",
                principalTable: "Nganh",
                principalColumn: "NganhId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CTKhung_Nganh_NganhId",
                table: "CTKhung");

            migrationBuilder.DropIndex(
                name: "IX_CTKhung_NganhId",
                table: "CTKhung");

            migrationBuilder.AlterColumn<string>(
                name: "NganhId",
                table: "CTKhung",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Nganh_CTKhung_NganhId",
                table: "Nganh",
                column: "NganhId",
                principalTable: "CTKhung",
                principalColumn: "CTKhungId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
