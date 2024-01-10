using CMS.Models;
using CMS.ViewModels;

namespace CMS.Interface
{
    public interface INhanVienRepository
    {
        Task CreateNhanVien(CreateNhanVien nhanvien, string filePath);
        Task<List<NhanVien>> GetAllNhanVien();
        Task<List<NhanVien>> GetNhanViensNotInKhoaHoc(int idkh);
        Task<NhanVien> GetProfileNhanVien(int idnv);
        Task EditNhanVien(NhanVien edited, string filePath);
        Task EditNhanVien(NhanVien edited);
        Task DeleteNhanVien(int id);
        Task<List<NhanVien>> NhanViensSearchBy(NhanVien search);
        Task DeleteNhanVienOutKhoaHoc(int idnv, int idkh);
        Task<CapChungChi> GetCapCCById(int idnv, int idkh);
    }
}
