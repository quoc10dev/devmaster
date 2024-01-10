using CMS.DatabaseContext;
using CMS.Interface;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repository
{
    public class GiaoVienRepository : IGiaoVienRepository
    {
        private readonly AppDbContext _appDbContext;

        public GiaoVienRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task EditGiaoVien(GiaoVien giaovien)
        {
            var edit = await _appDbContext.tblGiaoVien.FirstOrDefaultAsync(gv=>gv.Id == giaovien.Id);
            if (edit != null)
            {
                edit.TenGV = giaovien.TenGV;
                edit.IdDonViDaoTao = giaovien.IdDonViDaoTao;
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateGiaoVien(GiaoVien gv)
        {
            var newGiaoVien = new GiaoVien
            {
                TenGV = gv.TenGV,
                IdDonViDaoTao = gv.IdDonViDaoTao
            };
            await _appDbContext.AddAsync(newGiaoVien);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<GiaoVien>> GetAllGiaoVien()
        {
            return await _appDbContext.tblGiaoVien.ToListAsync();
        }
    }
}
