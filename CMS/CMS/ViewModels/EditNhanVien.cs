using CMS.Models;

namespace CMS.ViewModels
{
    public class EditNhanVien
    {
        public IFormFile? Avt { get; set; }
        public bool isRemoveFile { get; set; } = false;
    }
}
