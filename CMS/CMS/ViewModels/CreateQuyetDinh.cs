using CMS.Models;

namespace CMS.ViewModels
{
    public class CreateQuyetDinh
    {
        public QuyetDinh? QuyetDinh { get; set; }
        public IFormFile? PDF { get; set; }
    }
}
