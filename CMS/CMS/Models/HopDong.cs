using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class HopDong
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string SoHopDong { get; set; }
        [Required]
        public DateTime NgayKy { get; set; }
        [Required]
        public string AnhHopDong { get; set; }
        [Required]
        public int IdDonViDaoTao { get; set; }
        public DonViDaoTao? DonViDaoTao { get; set; }
    }
}
