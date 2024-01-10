using CMS.DatabaseContext;
using CMS.Interface;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repository
{
    public class PresetTableRepository : IPresetTableRepository
    {
        private readonly AppDbContext _appDbContext;
        public PresetTableRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<ChucDanh>> GetAllChucDanh()
        {
            return await _appDbContext.tblChucDanh.ToListAsync();
        }

        public async Task<List<PhongBan>> GetAllPhongBan()
        {
            return await _appDbContext.tblPhongBan.ToListAsync();
        }

        public async Task<List<NhomChungChi>> GetAllNhomCC()
        {
            return await _appDbContext.tblNhomChungChi.ToListAsync();
        }

        public async Task<List<LoaiDaoTao>> GetAllLoaiDT()
        {
            return await _appDbContext.tblLoaiDaoTao.ToListAsync();
        }
    }
}
