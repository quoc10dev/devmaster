using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.DatabaseContext
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CapChungChi>().HasKey(tbl => new
            {
                tbl.IdKhoaHoc,
                tbl.IdNhanVien
            });
            modelBuilder.Entity<KhoaHoc>()
                .HasOne(khoa => khoa.GiaoVien)
                .WithMany(gv => gv.KhoaHocs)
                .HasForeignKey(khoa => khoa.IdGiaoVien).IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KhoaHoc>()
                .HasOne(khoa => khoa.ChungChi)
                .WithMany(cc => cc.KhoaHocs)
                .HasForeignKey(khoa => khoa.IdChungChi)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KhoaHoc>()
                .HasOne(kh => kh.QuyetDinh)
                .WithMany(qd => qd.KhoaHocs)
                .HasForeignKey(kh => kh.IdQuyetDinh)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CapChungChi>()
                .HasOne(capcc => capcc.LoaiDaoTao)
                .WithMany(loaidt => loaidt.CapChungChis)
                .HasForeignKey(capcc => capcc.IdLoaiDaoTao)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChungChi>()
                .HasOne(cc => cc.NhomChungChi)
                .WithMany(nhomcc => nhomcc.ChungChis)
                .HasForeignKey(cc => cc.IdNhomChungChi)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CapChungChi>()
                .HasOne(capCC => capCC.KhoaHoc)
                .WithMany(khoa => khoa.CapChungChis)
                .HasForeignKey(capCC => capCC.IdKhoaHoc)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CapChungChi>()
                .HasOne(capCC => capCC.NhanVien)
                .WithMany(nv => nv.CapChungChis)
                .HasForeignKey(capCC => capCC.IdNhanVien);

            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.PhongBan)
                .WithMany(pb => pb.NhanViens)
                .HasForeignKey(nv => nv.IdPhongBan)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.ChucDanh)
                .WithMany(cd => cd.NhanViens)
                .HasForeignKey(nv => nv.IdChucDanh)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuyetDinh>()
                .HasOne(qd => qd.DonViDaoTao)
                .WithMany(dvdt => dvdt.QuyetDinhs)
                .HasForeignKey(qd => qd.IdDonViDaoTao)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GiaoVien>()
                .HasOne(gv => gv.DonViDaoTao)
                .WithMany(dvdt => dvdt.GiaoViens)
                .HasForeignKey(gv => gv.IdDonViDaoTao)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HopDong>()
                .HasOne(hd => hd.DonViDaoTao)
                .WithMany(dvdt => dvdt.HopDongs)
                .HasForeignKey(hd => hd.IdDonViDaoTao)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<KhoaHoc> tblKhoaHoc { get; set; }
        public DbSet<QuyetDinh> tblQuyetDinh { get; set; }
        public DbSet<CapChungChi> tblCapChungChi { get; set; }
        public DbSet<ChungChi> tblChungChi { get; set; }
        public DbSet<NhanVien> tblNhanVien { get; set; }
        public DbSet<GiaoVien> tblGiaoVien { get; set; }
        public DbSet<DonViDaoTao> tblDonViDaoTao { get; set; }
        public DbSet<HopDong> tblHopDong { get; set; }
        public DbSet<NhomChungChi> tblNhomChungChi { get; set; }
        public DbSet<LoaiDaoTao> tblLoaiDaoTao { get; set; }
        public DbSet<ChucDanh> tblChucDanh { get; set; }
        public DbSet<PhongBan> tblPhongBan { get; set; }
    }
}
