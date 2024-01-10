using CMS.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class CapChungChi
    {
        public int IdKhoaHoc { get; set; }
        public KhoaHoc? KhoaHoc { get; set; }
        [Required]
        [NotZero(ErrorMessage ="Vui lòng chọn nhân viên!")]
        public int IdNhanVien { get; set; }
        public NhanVien? NhanVien { get; set; }
        public bool KetQua { get; set; } = true;
        public string? AnhChungChi { get; set; } = "";
        public string? GhiChu { get; set; }

        [Required]
        [NotZero(ErrorMessage ="Vui lòng chọn loại đào tạo!")]
        public int IdLoaiDaoTao { get; set; }
        public LoaiDaoTao? LoaiDaoTao { get; set; }
    }
}
