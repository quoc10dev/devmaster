using System.ComponentModel.DataAnnotations;

namespace CMS.ViewModels
{
    public class ChangePass
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPass { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPass { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPass), ErrorMessage = "Mật khẩu nhập lại không khớp!")]
        public string ConfirmNewPass { get; set; }

    }
}
