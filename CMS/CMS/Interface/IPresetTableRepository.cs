using CMS.Models;

namespace CMS.Interface
{
    public interface IPresetTableRepository
    {
        Task<List<ChucDanh>> GetAllChucDanh();
        Task<List<PhongBan>> GetAllPhongBan();
        Task<List<NhomChungChi>> GetAllNhomCC();
        Task<List<LoaiDaoTao>> GetAllLoaiDT();
    }
}
