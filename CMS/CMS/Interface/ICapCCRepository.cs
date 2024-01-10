using CMS.Models;

namespace CMS.Interface
{
    public interface ICapCCRepository
    {
        Task CreateCapCC(CapChungChi capCC, string filePath);
        Task<List<CapChungChi>> GetAllCapCC(int idkh);
        Task<CapChungChi> GetCapCCById(int idkh, int idnv);
        Task EditCapCC(CapChungChi capCC, string filePath);
        Task EditCapCC(CapChungChi capCC);
    }
}
