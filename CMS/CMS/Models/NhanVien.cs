using CMS.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class NhanVien
    {
        [Key] 
        public int Id { get; set; }
        public string? MaNV { get; set; }
        [Required]
        public string TenNV { get; set; }
        public string? CCCD { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
        public string? AnhNhanVien { get; set; } = "";
        public bool TrangThai { get; set; } = true;

        [Required]
        [NotZero(ErrorMessage ="Vui lòng chọn chức danh!")]
        public int IdChucDanh { get; set; }
        public ChucDanh? ChucDanh { get; set; }

        [Required]
        [NotZero(ErrorMessage = "Vui lòng chọn phòng ban!")]
        public int IdPhongBan { get; set; }
        public PhongBan? PhongBan { get; set; }

        public List<CapChungChi>? CapChungChis { get; set; }
    }
}
