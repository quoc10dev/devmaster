using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class NhomChungChi
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string TenNhom { get; set; }
        public List<ChungChi>? ChungChis { get; set; }
    }
}
