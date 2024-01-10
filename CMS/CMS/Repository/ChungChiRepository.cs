using CMS.DatabaseContext;
using CMS.Interface;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repository
{
    public class ChungChiRepository : IChungChiRepository
    {
        private readonly AppDbContext _appDbContext;

        public ChungChiRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task EditChungChi(ChungChi chungChi)
        {
            var cc = await _appDbContext.tblChungChi.FirstOrDefaultAsync(cc=>cc.Id == chungChi.Id);
            if (cc != null)
            {
                cc.TenChungChi = chungChi.TenChungChi;
                cc.IdNhomChungChi = chungChi.IdNhomChungChi;
            }
            await _appDbContext.SaveChangesAsync();
        }
        public async Task CreateChungChi(ChungChi chungChi)
        {
            var newCC = new ChungChi
            {
                TenChungChi = chungChi.TenChungChi,
                IdNhomChungChi = chungChi.IdNhomChungChi
            };
            await _appDbContext.AddAsync(newCC);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<ChungChi>> GetAllChungChi()
        {
            return await _appDbContext.tblChungChi.ToListAsync();
        }
    }
}
