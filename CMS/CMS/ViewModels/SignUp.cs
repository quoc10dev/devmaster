using System.ComponentModel.DataAnnotations;

namespace CMS.ViewModels
{
    public class SignUp
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage ="Mật khẩu xác nhận không khớp!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
