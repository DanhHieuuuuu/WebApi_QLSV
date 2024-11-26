using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class QLSV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    BlockId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenBlock = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KiHocId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.BlockId);
                });

            migrationBuilder.CreateTable(
                name: "CauHoi",
                columns: table => new
                {
                    CauHoiId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoiDungCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<bool>(type: "bit", nullable: false),
                    MaxDiem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoi", x => x.CauHoiId);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    KhoaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruongKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.KhoaId);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Cccd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NienKhoa = table.Column<int>(type: "int", nullable: false),
                    LopQLId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cccd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "BoMon",
                columns: table => new
                {
                    BoMonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenBoMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruongBoMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoBoMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongGV = table.Column<int>(type: "int", nullable: false),
                    KhoaId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoMon", x => x.BoMonId);
                    table.ForeignKey(
                        name: "FK_BoMon_Khoa_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoa",
                        principalColumn: "KhoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nganh",
                columns: table => new
                {
                    NganhId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TruongNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhoaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SumClass = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nganh", x => x.NganhId);
                    table.ForeignKey(
                        name: "FK_Nganh_Khoa_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoa",
                        principalColumn: "KhoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    MaMonHoc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTin = table.Column<int>(type: "int", nullable: false),
                    BoMonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.MaMonHoc);
                    table.ForeignKey(
                        name: "FK_MonHoc_BoMon_BoMonId",
                        column: x => x.BoMonId,
                        principalTable: "BoMon",
                        principalColumn: "BoMonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenGiangVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Cccd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoMonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teacher_BoMon_BoMonId",
                        column: x => x.BoMonId,
                        principalTable: "BoMon",
                        principalColumn: "BoMonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LopQL",
                columns: table => new
                {
                    LopQLId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLopQL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NganhId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaxStudent = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    LopTruongId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LopPhoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopQL", x => x.LopQLId);
                    table.ForeignKey(
                        name: "FK_LopQL_Nganh_NganhId",
                        column: x => x.NganhId,
                        principalTable: "Nganh",
                        principalColumn: "NganhId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LopQL_Student_LopQLId",
                        column: x => x.LopQLId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teacher_MonHoc",
                columns: table => new
                {
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaMonHoc = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher_MonHoc", x => new { x.TeacherId, x.MaMonHoc });
                    table.ForeignKey(
                        name: "FK_Teacher_MonHoc_MonHoc_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teacher_MonHoc_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoMon_KhoaId",
                table: "BoMon",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_LopQL_NganhId",
                table: "LopQL",
                column: "NganhId");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_BoMonId",
                table: "MonHoc",
                column: "BoMonId");

            migrationBuilder.CreateIndex(
                name: "IX_Nganh_KhoaId",
                table: "Nganh",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_BoMonId",
                table: "Teacher",
                column: "BoMonId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_MonHoc_MaMonHoc",
                table: "Teacher_MonHoc",
                column: "MaMonHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "CauHoi");

            migrationBuilder.DropTable(
                name: "LopQL");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Teacher_MonHoc");

            migrationBuilder.DropTable(
                name: "Nganh");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "MonHoc");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "BoMon");

            migrationBuilder.DropTable(
                name: "Khoa");
        }
    }
}
