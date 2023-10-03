using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Les08.Models
{
    [Table("Category")]
    [Index(nameof(Product.Name), IsUnique = true)]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Tên loại")]
        [Required(ErrorMessage = "Tên loại không được để trống!")]
        [MinLength(6, ErrorMessage = "Tên loại ít nhất là 6 ký tự!")]
        [StringLength(100)]
        public string? Name { get; set; }
        [Display(Name = "Trạng thái")]
        [DefaultValue(1)]
        public byte Status { get; set; } = 1;
        [Display(Name = "Ngày tạo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Hình ảnh")]
        [StringLength(100)]
        [MaxLength(100, ErrorMessage = "Đường dẫn quá dài!")]
        [DataType(DataType.Upload)]
        public string? Image { get; set; }
        [Display(Name = "Nội dung")]
        [StringLength(350)]
        [MaxLength(350, ErrorMessage = "Nội dung quá dài!")]
        public string? Description { get; set; }
       // public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
