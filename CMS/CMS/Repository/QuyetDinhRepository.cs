using CMS.DatabaseContext;
using CMS.Interface;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repository
{
    public class QuyetDinhRepository : IQuyetDinhRepository
    {
        private readonly AppDbContext _appDbContext;

        public QuyetDinhRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task EditQuyetDinh(QuyetDinh qd)
        {
            var quyetdinh = await _appDbContext.tblQuyetDinh.FirstOrDefaultAsync(qd => qd.Id == qd.Id);
            if (quyetdinh != null)
            {
                quyetdinh.SoQuyetDinh = qd.SoQuyetDinh;
                quyetdinh.NgayQuyetDinh = qd.NgayQuyetDinh;
                quyetdinh.IdDonViDaoTao = qd.IdDonViDaoTao;
            }
            await _appDbContext.SaveChangesAsync();
        }
        public async Task EditQuyetDinh(QuyetDinh qd, string filePath)
        {
            var quyetdinh = await _appDbContext.tblQuyetDinh.FirstOrDefaultAsync(qd=>qd.Id == qd.Id);
            if(quyetdinh != null)
            {
                quyetdinh.SoQuyetDinh = qd.SoQuyetDinh;
                quyetdinh.NgayQuyetDinh = qd.NgayQuyetDinh;
                quyetdinh.IdDonViDaoTao = qd.IdDonViDaoTao;
                quyetdinh.AnhQuyetDinh = filePath;
            }
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<QuyetDinh> GetQuyetDinhDetail(int id)
        {
            var quyetdinh = await _appDbContext.tblQuyetDinh
                .Include(qd=>qd.KhoaHocs).ThenInclude(khs=>khs.CapChungChis)
                .Include(qd => qd.KhoaHocs).ThenInclude(khs=>khs.ChungChi)
                .Include(qd => qd.KhoaHocs).ThenInclude(khs => khs.GiaoVien)
                .FirstOrDefaultAsync(qd=>qd.Id == id);
            return quyetdinh;
        }
        public async Task<QuyetDinh> GetQuyetDinhById(int id)
        {
            return await _appDbContext.tblQuyetDinh.FirstOrDefaultAsync(qd => qd.Id == id);
        }

        public async Task<List<QuyetDinh>> GetAllQuyetDinh()
        {
            return await _appDbContext.tblQuyetDinh.ToListAsync();
        }

        public async Task CreateQuyetDinh(QuyetDinh qd, string filePath)
        {
            var newQuyetDinh = new QuyetDinh
            {
                SoQuyetDinh = qd.SoQuyetDinh,
                NgayQuyetDinh = qd.NgayQuyetDinh,
                IdDonViDaoTao = qd.IdDonViDaoTao,
                AnhQuyetDinh = filePath
            };
            await _appDbContext.AddAsync<QuyetDinh>(newQuyetDinh);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
