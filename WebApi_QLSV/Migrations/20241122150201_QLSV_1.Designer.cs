﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi_QLSV.DbContexts;

#nullable disable

namespace WebApi_QLSV.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241122150201_QLSV_1")]
    partial class QLSV_1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi_QLSV.Entities.Block", b =>
                {
                    b.Property<string>("BlockId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("KetThuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("KiHocId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamHoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenBlock")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlockId");

                    b.ToTable("Block");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.BoMon", b =>
                {
                    b.Property<string>("BoMonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("KhoaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("NgayThanhLap")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoBoMon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuongGV")
                        .HasColumnType("int");

                    b.Property<string>("TenBoMon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TruongBoMon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BoMonId");

                    b.HasIndex("KhoaId");

                    b.ToTable("BoMon");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.CauHoi", b =>
                {
                    b.Property<string>("CauHoiId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MaxDiem")
                        .HasColumnType("int");

                    b.Property<string>("NoiDungCauHoi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Role")
                        .HasColumnType("bit");

                    b.HasKey("CauHoiId");

                    b.ToTable("CauHoi");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.ClassFd.LopQL", b =>
                {
                    b.Property<string>("LopQLId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LopPhoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LopTruongId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxStudent")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("NganhId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TeacherId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenLopQL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LopQLId");

                    b.HasIndex("NganhId");

                    b.ToTable("LopQL");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Khoa", b =>
                {
                    b.Property<string>("KhoaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("NgayThanhLap")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoKhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TruongKhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KhoaId");

                    b.ToTable("Khoa");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Manager", b =>
                {
                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cccd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueQuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ManagerId");

                    b.ToTable("Manager");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.MonHoc", b =>
                {
                    b.Property<string>("MaMonHoc")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BoMonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SoTin")
                        .HasColumnType("int");

                    b.Property<string>("TenMon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaMonHoc");

                    b.HasIndex("BoMonId");

                    b.ToTable("MonHoc");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Nganh", b =>
                {
                    b.Property<string>("NganhId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("KhoaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgayThanhLap")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoNganh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SumClass")
                        .HasColumnType("int");

                    b.Property<string>("TenNganh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TruongNganh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NganhId");

                    b.HasIndex("KhoaId");

                    b.ToTable("Nganh");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cccd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("GioiTinh")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LopQLId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NienKhoa")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueQuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("LopQLId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Teacher", b =>
                {
                    b.Property<string>("TeacherId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("BoMonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cccd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueQuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenGiangVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.HasIndex("BoMonId");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Teacher_MonHoc", b =>
                {
                    b.Property<string>("TeacherId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaMonHoc")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TeacherId", "MaMonHoc");

                    b.HasIndex("MaMonHoc");

                    b.ToTable("Teacher_MonHoc");
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.BoMon", b =>
                {
                    b.HasOne("WebApi_QLSV.Entities.Khoa", null)
                        .WithMany()
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.ClassFd.LopQL", b =>
                {
                    b.HasOne("WebApi_QLSV.Entities.Nganh", null)
                        .WithMany()
                        .HasForeignKey("NganhId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.MonHoc", b =>
                {
                    b.HasOne("WebApi_QLSV.Entities.BoMon", null)
                        .WithMany()
                        .HasForeignKey("BoMonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Nganh", b =>
                {
                    b.HasOne("WebApi_QLSV.Entities.Khoa", null)
                        .WithMany()
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Student", b =>
                {
                    b.HasOne("WebApi_QLSV.Entities.ClassFd.LopQL", null)
                        .WithMany()
                        .HasForeignKey("LopQLId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Teacher", b =>
                {
                    b.HasOne("WebApi_QLSV.Entities.BoMon", null)
                        .WithMany()
                        .HasForeignKey("BoMonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi_QLSV.Entities.Teacher_MonHoc", b =>
                {
                    b.HasOne("WebApi_QLSV.Entities.MonHoc", null)
                        .WithMany()
                        .HasForeignKey("MaMonHoc")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApi_QLSV.Entities.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}