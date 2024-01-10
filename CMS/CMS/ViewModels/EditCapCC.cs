using CMS.Models;

namespace CMS.ViewModels
{
    public class EditCapCC
    {
        public CapChungChi? CapCC { get; set; }
        public IFormFile? AnhCC { get; set; }
        public bool isRemoveFile { get; set; } = false;
    }
}
