using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class qlsv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_LopQL_TenLopQL",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "TenLopQL",
                table: "Student",
                newName: "LopQLId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_TenLopQL",
                table: "Student",
                newName: "IX_Student_LopQLId");

            migrationBuilder.RenameColumn(
                name: "TenLopQL",
                table: "LopQL",
                newName: "LopQLId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_LopQL_LopQLId",
                table: "Student",
                column: "LopQLId",
                principalTable: "LopQL",
                principalColumn: "LopQLId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_LopQL_LopQLId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "LopQLId",
                table: "Student",
                newName: "TenLopQL");

            migrationBuilder.RenameIndex(
                name: "IX_Student_LopQLId",
                table: "Student",
                newName: "IX_Student_TenLopQL");

            migrationBuilder.RenameColumn(
                name: "LopQLId",
                table: "LopQL",
                newName: "TenLopQL");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_LopQL_TenLopQL",
                table: "Student",
                column: "TenLopQL",
                principalTable: "LopQL",
                principalColumn: "TenLopQL",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
