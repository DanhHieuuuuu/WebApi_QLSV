using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class updatemgs5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diem");

            migrationBuilder.RenameColumn(
                name: "KhoaId",
                table: "Teacher",
                newName: "Cccd");

            migrationBuilder.RenameColumn(
                name: "Khoa",
                table: "Manager",
                newName: "Cccd");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Manager",
                newName: "ManagerId");

            migrationBuilder.RenameColumn(
                name: "TenCauHoi",
                table: "CauHoi",
                newName: "NoiDungCauHoi");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Teacher",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "GioiTinh",
                table: "Teacher",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Manager",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "GioiTinh",
                table: "Manager",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MonId",
                table: "LopHP",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Diem",
                table: "ClassStudent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LopHP_MonId",
                table: "LopHP",
                column: "MonId");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHP_MonHoc_MonId",
                table: "LopHP",
                column: "MonId",
                principalTable: "MonHoc",
                principalColumn: "MonId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHP_MonHoc_MonId",
                table: "LopHP");

            migrationBuilder.DropIndex(
                name: "IX_LopHP_MonId",
                table: "LopHP");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Manager");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "Manager");

            migrationBuilder.DropColumn(
                name: "MonId",
                table: "LopHP");

            migrationBuilder.DropColumn(
                name: "Diem",
                table: "ClassStudent");

            migrationBuilder.RenameColumn(
                name: "Cccd",
                table: "Teacher",
                newName: "KhoaId");

            migrationBuilder.RenameColumn(
                name: "Cccd",
                table: "Manager",
                newName: "Khoa");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Manager",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "NoiDungCauHoi",
                table: "CauHoi",
                newName: "TenCauHoi");

            migrationBuilder.CreateTable(
                name: "Diem",
                columns: table => new
                {
                    DiemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KiHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diem", x => x.DiemId);
                    table.ForeignKey(
                        name: "FK_Diem_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diem_StudentId",
                table: "Diem",
                column: "StudentId");
        }
    }
}
