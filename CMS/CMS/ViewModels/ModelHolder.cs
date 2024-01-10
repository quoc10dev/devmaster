using CMS.Models;

namespace CMS.ViewModels
{
    public class ModelHolder
    {
        public CreateNhanVien? CreateNhanVien { get; set; }
        public EditNhanVien? EditNhanVien { get; set; }
        public CreateQuyetDinh? CreateQuyetDinh { get; set; }
        public CreateCapCC? CreateCapCC { get; set; }
        public EditCapCC? EditCapCC { get; set; }
        public EditQuyetDinh? EditQuyetDinh { get; set; }
        public DonViDaoTao? DVDT { get; set; }
        public ChungChi? ChungChi { get; set; }
        public QuyetDinh? QuyetDinh { get; set; }
        public NhanVien? NhanVien { get; set; }
        public GiaoVien? GiaoVien { get; set; }
        public KhoaHoc? KhoaHoc { get; set; }
        public CapChungChi? CapCC { get; set; }
        public NhanVien? SearchNhanVien { get; set; }
        public List<ChucDanh>? ChucDanhs { get; set; }
        public List<PhongBan>? PhongBans { get; set; }
        public List<NhanVien>? NhanViens { get; set; }
        public List<DonViDaoTao>? DVDTs { get; set; }
        public List<QuyetDinh>? QuyetDinhs { get; set; }
        public List<ChungChi>? ChungChis { get; set; }
        public List<NhomChungChi>? NhomChungChis { get; set; }
        public List<GiaoVien>? GiaoViens { get; set; }
        public List<KhoaHoc>? KhoaHocs { get; set; }
        public List<CapChungChi>? CapCCs { get; set; }
        public List<LoaiDaoTao>? LoaiDaoTaos { get; set; }
    }
}
