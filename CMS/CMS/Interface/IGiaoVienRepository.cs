using CMS.Models;

namespace CMS.Interface
{
    public interface IGiaoVienRepository
    {
        Task<List<GiaoVien>> GetAllGiaoVien();
        Task CreateGiaoVien(GiaoVien gv);
        Task EditGiaoVien(GiaoVien giaovien);
    }
}
