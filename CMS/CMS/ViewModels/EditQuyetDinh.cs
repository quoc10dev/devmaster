using CMS.Models;

namespace CMS.ViewModels
{
    public class EditQuyetDinh
    {
        public QuyetDinh? QuyetDinh { get; set; }
        public bool isRemoveFile { get; set; } = false;
        public IFormFile? AnhQD { get; set; }
    }
}
