using CMS.DatabaseContext;
using CMS.Interface;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repository
{
    public class CapCCRepository : ICapCCRepository
    {
        private readonly AppDbContext  _appDbContext;

        public CapCCRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task EditCapCC(CapChungChi capCC)
        {
            var oldOne = await _appDbContext.tblCapChungChi.FirstOrDefaultAsync(cc => cc.IdKhoaHoc == capCC.IdKhoaHoc && cc.IdNhanVien == capCC.IdNhanVien);
            oldOne.IdLoaiDaoTao = capCC.IdLoaiDaoTao;
            oldOne.GhiChu = capCC.GhiChu;
            await _appDbContext.SaveChangesAsync();
        }
        public async Task EditCapCC(CapChungChi capCC, string filePath)
        {
            var oldOne = await _appDbContext.tblCapChungChi.FirstOrDefaultAsync(cc => cc.IdKhoaHoc == capCC.IdKhoaHoc && cc.IdNhanVien == capCC.IdNhanVien);
            oldOne.IdLoaiDaoTao = capCC.IdLoaiDaoTao;
            oldOne.AnhChungChi = filePath;
            oldOne.GhiChu = capCC.GhiChu;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateCapCC(CapChungChi capCC, string filePath)
        {
            var newCapCC = new CapChungChi
            {
                IdKhoaHoc = capCC.IdKhoaHoc,
                IdNhanVien = capCC.IdNhanVien,
                IdLoaiDaoTao = capCC.IdLoaiDaoTao,
                AnhChungChi = filePath,
                KetQua = true,
                GhiChu = capCC.GhiChu
            };
            await _appDbContext.AddAsync(newCapCC);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<CapChungChi>> GetAllCapCC(int idkh)
        {
            return await _appDbContext.tblCapChungChi.Where(capcc => capcc.IdKhoaHoc == idkh).Include(capcc=>capcc.NhanVien).ToListAsync();
        }

        public Task<CapChungChi> GetCapCCById(int idkh, int idnv)
        {
            throw new NotImplementedException();
        }

    }
}
