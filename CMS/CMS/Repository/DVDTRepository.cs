using CMS.DatabaseContext;
using CMS.Interface;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repository
{
    public class DVDTRepository : IDVDTRepository
    {
        private readonly AppDbContext  _appDbContext;

        public DVDTRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task EditDVDT(DonViDaoTao dvdt)
        {
            var edit = await _appDbContext.tblDonViDaoTao.FirstOrDefaultAsync(dvdt=>dvdt.Id == dvdt.Id);
            if (edit != null)
            {
                edit.TenDonViDaoTao = dvdt.TenDonViDaoTao;
            }
            await _appDbContext.SaveChangesAsync();
        }
        public async Task CreateDVDT(DonViDaoTao dvdt)
        {
            var newDVDT = new DonViDaoTao
            {
                TenDonViDaoTao = dvdt.TenDonViDaoTao
            };
            await _appDbContext.AddAsync(newDVDT);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<DonViDaoTao>> GetAllDVDT()
        {
            return await _appDbContext.tblDonViDaoTao.ToListAsync();
        }
    }
}
