using CMS.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class KhoaHoc
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public DateTime NgayBatDau { get; set; }
        [Required]
        public DateTime NgayKetThuc { get; set; }
        public string? DiaDiem { get; set; }
        [Required]
        public int ExpMonth { get; set; }
        public string? GhiChu { get; set; }

        [Required]
        [NotZero(ErrorMessage = "Vui lòng chọn chứng chỉ!")]
        public int IdChungChi { get; set; }
        public ChungChi? ChungChi { get; set; }

        [Required]
        [NotZero(ErrorMessage = "Vui lòng chọn quyết định!")]
        public int IdQuyetDinh { get; set; }
        public QuyetDinh? QuyetDinh { get; set; }

        [Required]
        [NotZero(ErrorMessage = "Vui lòng chọn giáo viên!")]
        public int IdGiaoVien { get; set; }
        public GiaoVien? GiaoVien { get; set; }

        public List<CapChungChi>? CapChungChis { get; set; }
    }
}
