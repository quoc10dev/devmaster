using CMS.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class ChungChi
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TenChungChi { get; set; }

        [Required]
        [NotZero(ErrorMessage ="Vui lòng chọn nhóm chứng chỉ!")]
        public int IdNhomChungChi { get; set; }
        public NhomChungChi? NhomChungChi { get; set; }

        public List<KhoaHoc>? KhoaHocs { get; set; }
    }
}
