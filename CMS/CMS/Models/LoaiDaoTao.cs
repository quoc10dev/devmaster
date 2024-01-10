using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class LoaiDaoTao
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string TenLoaiDaoTao { get; set; }

        public List<CapChungChi>? CapChungChis { get; set; }
    }
}
