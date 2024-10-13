using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    /// <inheritdoc />
    public partial class qlsv : Migration
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
                name: "CTKhung",
                columns: table => new
                {
                    CTKhungId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TongSoTin = table.Column<int>(type: "int", nullable: false),
                    NganhId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTKhung", x => x.CTKhungId);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    KhoaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruongKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Cccd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "BoMon",
                columns: table => new
                {
                    BoMonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenBoMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruongBoMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoBoMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongGV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoMon", x => x.BoMonId);
                    table.ForeignKey(
                        name: "FK_BoMon_Khoa_BoMonId",
                        column: x => x.BoMonId,
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
                    InfoNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhoaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SumClass = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nganh", x => x.NganhId);
                    table.ForeignKey(
                        name: "FK_Nganh_CTKhung_NganhId",
                        column: x => x.NganhId,
                        principalTable: "CTKhung",
                        principalColumn: "CTKhungId",
                        onDelete: ReferentialAction.Restrict);
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
                    MaHocPhan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sotin = table.Column<int>(type: "int", nullable: false),
                    BoMonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.MaHocPhan);
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
                    BoMonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    TenLopQL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NganhId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaxStudent = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    LopTruong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LopPho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuNhiem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopQL", x => x.TenLopQL);
                    table.ForeignKey(
                        name: "FK_LopQL_Nganh_NganhId",
                        column: x => x.NganhId,
                        principalTable: "Nganh",
                        principalColumn: "NganhId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LopHP",
                columns: table => new
                {
                    LopHPId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLopHP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KiHocNamHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxStudent = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlockId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MonId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHP", x => x.LopHPId);
                    table.ForeignKey(
                        name: "FK_LopHP_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Block",
                        principalColumn: "BlockId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LopHP_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
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
                    TenLopQL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cccd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Student_LopQL_TenLopQL",
                        column: x => x.TenLopQL,
                        principalTable: "LopQL",
                        principalColumn: "TenLopQL",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassStudent",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LopHPId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiemQT = table.Column<double>(type: "float", nullable: true),
                    DiemKT = table.Column<double>(type: "float", nullable: true),
                    DiemMH = table.Column<double>(type: "float", nullable: true),
                    TienMonHoc = table.Column<int>(type: "int", nullable: false),
                    Nop = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStudent", x => new { x.StudentId, x.LopHPId });
                    table.ForeignKey(
                        name: "FK_ClassStudent_LopHP_LopHPId",
                        column: x => x.LopHPId,
                        principalTable: "LopHP",
                        principalColumn: "LopHPId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassStudent_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudent_LopHPId",
                table: "ClassStudent",
                column: "LopHPId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHP_BlockId",
                table: "LopHP",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHP_TeacherId",
                table: "LopHP",
                column: "TeacherId");

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
                name: "IX_Student_TenLopQL",
                table: "Student",
                column: "TenLopQL");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_BoMonId",
                table: "Teacher",
                column: "BoMonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHoi");

            migrationBuilder.DropTable(
                name: "ClassStudent");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "MonHoc");

            migrationBuilder.DropTable(
                name: "LopHP");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "LopQL");

            migrationBuilder.DropTable(
                name: "BoMon");

            migrationBuilder.DropTable(
                name: "Nganh");

            migrationBuilder.DropTable(
                name: "CTKhung");

            migrationBuilder.DropTable(
                name: "Khoa");
        }
    }
}
