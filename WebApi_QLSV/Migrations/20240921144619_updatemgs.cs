using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class updatemgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KiHoc",
                table: "CTKhung");

            migrationBuilder.DropColumn(
                name: "MonId",
                table: "CTKhung");

            migrationBuilder.DropColumn(
                name: "NamHoc",
                table: "CTKhung");

            migrationBuilder.DropColumn(
                name: "TenMon",
                table: "CTKhung");

            migrationBuilder.RenameColumn(
                name: "SoTin",
                table: "CTKhung",
                newName: "TongSoTin");

            migrationBuilder.CreateTable(
                name: "MonHocs",
                columns: table => new
                {
                    MonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KiHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CTKhungId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHocs", x => x.MonId);
                    table.ForeignKey(
                        name: "FK_MonHocs_CTKhung_CTKhungId",
                        column: x => x.CTKhungId,
                        principalTable: "CTKhung",
                        principalColumn: "CTKhungId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonHocs_CTKhungId",
                table: "MonHocs",
                column: "CTKhungId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonHocs");

            migrationBuilder.RenameColumn(
                name: "TongSoTin",
                table: "CTKhung",
                newName: "SoTin");

            migrationBuilder.AddColumn<string>(
                name: "KiHoc",
                table: "CTKhung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MonId",
                table: "CTKhung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NamHoc",
                table: "CTKhung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenMon",
                table: "CTKhung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
