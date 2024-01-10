using CMS.DatabaseContext;
using CMS.Interface;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repository
{
    public class KhoaHocRepository: IKhoaHocRepository
    {
        private readonly AppDbContext _appDbContext;

        public KhoaHocRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task EditKhoaHoc(KhoaHoc edited)
        {
            var khoahoc = await _appDbContext.tblKhoaHoc.FirstOrDefaultAsync(kh=> kh.Id == edited.Id);
            if(khoahoc != null)
            {
                khoahoc.NgayBatDau = edited.NgayBatDau;
                khoahoc.NgayKetThuc = edited.NgayKetThuc;
                khoahoc.IdQuyetDinh = edited.IdQuyetDinh;
                khoahoc.IdChungChi = edited.IdChungChi;
                khoahoc.IdGiaoVien = edited.IdGiaoVien;
                khoahoc.DiaDiem = edited.DiaDiem;
                khoahoc.ExpMonth = edited.ExpMonth;
                khoahoc.GhiChu = edited.GhiChu;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<KhoaHoc>> GetAllKhoaHoc()
        {
            return await _appDbContext.tblKhoaHoc.ToListAsync();
        }

        public async Task CreateKhoaHoc(KhoaHoc kh)
        {
            var newKhoaHoc = new KhoaHoc
            {
                IdQuyetDinh = kh.IdQuyetDinh,
                IdChungChi = kh.IdChungChi,
                NgayBatDau = kh.NgayBatDau,
                NgayKetThuc = kh.NgayKetThuc,
                IdGiaoVien = kh.IdGiaoVien,
                ExpMonth = kh.ExpMonth,
                DiaDiem = kh.DiaDiem,
                GhiChu = kh.GhiChu
            };
            await _appDbContext.AddAsync(newKhoaHoc);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<KhoaHoc> GetKhoaHocById(int id)
        {
            return await _appDbContext.tblKhoaHoc.FirstOrDefaultAsync(kh => kh.Id == id);
        }
    }
}
