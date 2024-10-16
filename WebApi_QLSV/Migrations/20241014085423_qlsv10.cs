using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class qlsv10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InfoNganh",
                table: "Nganh",
                newName: "TruongNganh");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThanhLap",
                table: "Nganh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PhoNganh",
                table: "Nganh",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThanhLap",
                table: "Khoa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThanhLap",
                table: "BoMon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayThanhLap",
                table: "Nganh");

            migrationBuilder.DropColumn(
                name: "PhoNganh",
                table: "Nganh");

            migrationBuilder.DropColumn(
                name: "NgayThanhLap",
                table: "Khoa");

            migrationBuilder.DropColumn(
                name: "NgayThanhLap",
                table: "BoMon");

            migrationBuilder.RenameColumn(
                name: "TruongNganh",
                table: "Nganh",
                newName: "InfoNganh");
        }
    }
}
