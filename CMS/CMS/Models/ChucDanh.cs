using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class ChucDanh
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string TenChucDanh { get; set; }
        public List<NhanVien>? NhanViens { get; set; }
    }
}
