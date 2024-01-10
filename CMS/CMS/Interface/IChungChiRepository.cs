using CMS.Models;

namespace CMS.Interface
{
    public interface IChungChiRepository
    {
        Task<List<ChungChi>> GetAllChungChi();
        Task CreateChungChi(ChungChi chungChi);
        Task EditChungChi(ChungChi chungChi);
    }
}
