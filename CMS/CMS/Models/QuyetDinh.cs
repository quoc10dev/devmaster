using CMS.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class QuyetDinh
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SoQuyetDinh { get; set; }
        [Required]
        public DateTime NgayQuyetDinh { get; set; }
        public string? AnhQuyetDinh { get; set; } = "";

        [Required]
        [NotZero(ErrorMessage ="Vui lòng chọn đơn vị!")]
        public int IdDonViDaoTao { get; set; }
        public DonViDaoTao? DonViDaoTao { get; set; }

        public List<KhoaHoc>? KhoaHocs { get; set; }
    }
}
