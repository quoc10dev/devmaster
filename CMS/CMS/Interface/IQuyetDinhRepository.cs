using CMS.Models;
using CMS.ViewModels;

namespace CMS.Interface
{
    public interface IQuyetDinhRepository
    {
        Task<List<QuyetDinh>> GetAllQuyetDinh();
        Task CreateQuyetDinh(QuyetDinh qd, string filePath);
        Task<QuyetDinh> GetQuyetDinhById(int id);
        Task<QuyetDinh> GetQuyetDinhDetail(int id);
        Task EditQuyetDinh(QuyetDinh qd, string filePath);
        Task EditQuyetDinh(QuyetDinh qd);
    }
}
