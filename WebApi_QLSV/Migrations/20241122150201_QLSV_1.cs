using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class QLSV_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopQL_Student_LopQLId",
                table: "LopQL");

            migrationBuilder.AlterColumn<string>(
                name: "LopQLId",
                table: "Student",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Student_LopQLId",
                table: "Student",
                column: "LopQLId");

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

            migrationBuilder.DropIndex(
                name: "IX_Student_LopQLId",
                table: "Student");

            migrationBuilder.AlterColumn<string>(
                name: "LopQLId",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_LopQL_Student_LopQLId",
                table: "LopQL",
                column: "LopQLId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
