using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class updatemgs3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nganh_CTKhung_NganhId",
                table: "Nganh");

            migrationBuilder.AlterColumn<string>(
                name: "CTKhungId",
                table: "Nganh",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Nganh_CTKhungId",
                table: "Nganh",
                column: "CTKhungId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nganh_CTKhung_CTKhungId",
                table: "Nganh",
                column: "CTKhungId",
                principalTable: "CTKhung",
                principalColumn: "CTKhungId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nganh_CTKhung_CTKhungId",
                table: "Nganh");

            migrationBuilder.DropIndex(
                name: "IX_Nganh_CTKhungId",
                table: "Nganh");

            migrationBuilder.AlterColumn<string>(
                name: "CTKhungId",
                table: "Nganh",
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
