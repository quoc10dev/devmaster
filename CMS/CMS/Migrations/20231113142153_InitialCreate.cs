using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblChucDanh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucDanh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblChucDanh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDonViDaoTao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDonViDaoTao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDonViDaoTao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblLoaiDaoTao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiDaoTao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLoaiDaoTao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblNhomChungChi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNhomChungChi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPhongBan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhongBan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPhongBan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGiaoVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenGV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdDonViDaoTao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGiaoVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblGiaoVien_tblDonViDaoTao_IdDonViDaoTao",
                        column: x => x.IdDonViDaoTao,
                        principalTable: "tblDonViDaoTao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblHopDong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoHopDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayKy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnhHopDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDonViDaoTao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblHopDong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblHopDong_tblDonViDaoTao_IdDonViDaoTao",
                        column: x => x.IdDonViDaoTao,
                        principalTable: "tblDonViDaoTao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblQuyetDinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoQuyetDinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayQuyetDinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnhQuyetDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDonViDaoTao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblQuyetDinh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblQuyetDinh_tblDonViDaoTao_IdDonViDaoTao",
                        column: x => x.IdDonViDaoTao,
                        principalTable: "tblDonViDaoTao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblChungChi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChungChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdNhomChungChi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblChungChi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblChungChi_tblNhomChungChi_IdNhomChungChi",
                        column: x => x.IdNhomChungChi,
                        principalTable: "tblNhomChungChi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblNhanVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    IdChucDanh = table.Column<int>(type: "int", nullable: false),
                    IdPhongBan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNhanVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblNhanVien_tblChucDanh_IdChucDanh",
                        column: x => x.IdChucDanh,
                        principalTable: "tblChucDanh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblNhanVien_tblPhongBan_IdPhongBan",
                        column: x => x.IdPhongBan,
                        principalTable: "tblPhongBan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblKhoaHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpMonth = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdChungChi = table.Column<int>(type: "int", nullable: false),
                    IdQuyetDinh = table.Column<int>(type: "int", nullable: false),
                    IdGiaoVien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhoaHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblKhoaHoc_tblChungChi_IdChungChi",
                        column: x => x.IdChungChi,
                        principalTable: "tblChungChi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblKhoaHoc_tblGiaoVien_IdGiaoVien",
                        column: x => x.IdGiaoVien,
                        principalTable: "tblGiaoVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblKhoaHoc_tblQuyetDinh_IdQuyetDinh",
                        column: x => x.IdQuyetDinh,
                        principalTable: "tblQuyetDinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCapChungChi",
                columns: table => new
                {
                    IdKhoaHoc = table.Column<int>(type: "int", nullable: false),
                    IdNhanVien = table.Column<int>(type: "int", nullable: false),
                    KetQua = table.Column<bool>(type: "bit", nullable: false),
                    AnhChungChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiDaoTao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCapChungChi", x => new { x.IdKhoaHoc, x.IdNhanVien });
                    table.ForeignKey(
                        name: "FK_tblCapChungChi_tblKhoaHoc_IdKhoaHoc",
                        column: x => x.IdKhoaHoc,
                        principalTable: "tblKhoaHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCapChungChi_tblLoaiDaoTao_IdLoaiDaoTao",
                        column: x => x.IdLoaiDaoTao,
                        principalTable: "tblLoaiDaoTao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCapChungChi_tblNhanVien_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "tblNhanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCapChungChi_IdLoaiDaoTao",
                table: "tblCapChungChi",
                column: "IdLoaiDaoTao");

            migrationBuilder.CreateIndex(
                name: "IX_tblCapChungChi_IdNhanVien",
                table: "tblCapChungChi",
                column: "IdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_tblChungChi_IdNhomChungChi",
                table: "tblChungChi",
                column: "IdNhomChungChi");

            migrationBuilder.CreateIndex(
                name: "IX_tblGiaoVien_IdDonViDaoTao",
                table: "tblGiaoVien",
                column: "IdDonViDaoTao");

            migrationBuilder.CreateIndex(
                name: "IX_tblHopDong_IdDonViDaoTao",
                table: "tblHopDong",
                column: "IdDonViDaoTao");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhoaHoc_IdChungChi",
                table: "tblKhoaHoc",
                column: "IdChungChi");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhoaHoc_IdGiaoVien",
                table: "tblKhoaHoc",
                column: "IdGiaoVien");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhoaHoc_IdQuyetDinh",
                table: "tblKhoaHoc",
                column: "IdQuyetDinh");

            migrationBuilder.CreateIndex(
                name: "IX_tblNhanVien_IdChucDanh",
                table: "tblNhanVien",
                column: "IdChucDanh");

            migrationBuilder.CreateIndex(
                name: "IX_tblNhanVien_IdPhongBan",
                table: "tblNhanVien",
                column: "IdPhongBan");

            migrationBuilder.CreateIndex(
                name: "IX_tblQuyetDinh_IdDonViDaoTao",
                table: "tblQuyetDinh",
                column: "IdDonViDaoTao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCapChungChi");

            migrationBuilder.DropTable(
                name: "tblHopDong");

            migrationBuilder.DropTable(
                name: "tblKhoaHoc");

            migrationBuilder.DropTable(
                name: "tblLoaiDaoTao");

            migrationBuilder.DropTable(
                name: "tblNhanVien");

            migrationBuilder.DropTable(
                name: "tblChungChi");

            migrationBuilder.DropTable(
                name: "tblGiaoVien");

            migrationBuilder.DropTable(
                name: "tblQuyetDinh");

            migrationBuilder.DropTable(
                name: "tblChucDanh");

            migrationBuilder.DropTable(
                name: "tblPhongBan");

            migrationBuilder.DropTable(
                name: "tblNhomChungChi");

            migrationBuilder.DropTable(
                name: "tblDonViDaoTao");
        }
    }
}
