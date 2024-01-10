using CMS.Models;

namespace CMS.Interface
{
    public interface IKhoaHocRepository
    {
        Task CreateKhoaHoc(KhoaHoc kh);
        Task<List<KhoaHoc>> GetAllKhoaHoc();
        Task<KhoaHoc> GetKhoaHocById(int id);
        Task EditKhoaHoc(KhoaHoc edited);
    }
}
