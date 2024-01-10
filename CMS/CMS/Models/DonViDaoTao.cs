using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class DonViDaoTao
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string TenDonViDaoTao { get; set; }

        public List<QuyetDinh>? QuyetDinhs { get; set; }
        public List<HopDong>? HopDongs { get; set; }
        public List<GiaoVien>? GiaoViens { get; set; }
    }
}
