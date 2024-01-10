using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class PhongBan
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string TenPhongBan { get; set; }
        public List<NhanVien>? NhanViens { get; set; }
    }
}
