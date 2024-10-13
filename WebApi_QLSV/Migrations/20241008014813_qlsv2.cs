using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class qlsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoMon_Khoa_BoMonId",
                table: "BoMon");

            migrationBuilder.AddColumn<string>(
                name: "KhoaId",
                table: "BoMon",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BoMon_KhoaId",
                table: "BoMon",
                column: "KhoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoMon_Khoa_KhoaId",
                table: "BoMon",
                column: "KhoaId",
                principalTable: "Khoa",
                principalColumn: "KhoaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoMon_Khoa_KhoaId",
                table: "BoMon");

            migrationBuilder.DropIndex(
                name: "IX_BoMon_KhoaId",
                table: "BoMon");

            migrationBuilder.DropColumn(
                name: "KhoaId",
                table: "BoMon");

            migrationBuilder.AddForeignKey(
                name: "FK_BoMon_Khoa_BoMonId",
                table: "BoMon",
                column: "BoMonId",
                principalTable: "Khoa",
                principalColumn: "KhoaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
