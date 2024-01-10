using CMS.DatabaseContext;
using CMS.Interface;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repository
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly AppDbContext _appDbContext;
        public NhanVienRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CapChungChi> GetCapCCById(int idnv, int idkh)
        {
            var result = await _appDbContext.tblCapChungChi.FirstOrDefaultAsync(capcc => capcc.IdNhanVien == idnv && capcc.IdKhoaHoc == idkh);
            return result;
        }

        public async Task DeleteNhanVienOutKhoaHoc(int idnv, int idkh)
        {
            var result = await _appDbContext.tblCapChungChi.FirstOrDefaultAsync(capcc => capcc.IdNhanVien == idnv && capcc.IdKhoaHoc == idkh);
            _appDbContext.tblCapChungChi.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<NhanVien>> NhanViensSearchBy(NhanVien search)
        {
            var result = new List<NhanVien>();
            if(search.MaNV != null)
            {
                result = await _appDbContext.tblNhanVien.Where(nv=>nv.MaNV == search.MaNV).ToListAsync();
            }
            else
            {
                if(search.TenNV == null && search.IdPhongBan == 0 && search.IdChucDanh == 0)
                {
                    result = await _appDbContext.tblNhanVien.ToListAsync();
                }
                else if(search.TenNV != null && search.IdPhongBan == 0 && search.IdChucDanh == 0)
                {
                    result = await _appDbContext.tblNhanVien.Where(nv => nv.TenNV.Contains(search.TenNV)).ToListAsync();
                }
                else if (search.TenNV == null && search.IdPhongBan != 0 && search.IdChucDanh == 0)
                {
                    result = await _appDbContext.tblNhanVien.Where(nv => nv.IdPhongBan == search.IdPhongBan).ToListAsync();
                }
                else if (search.TenNV == null && search.IdPhongBan == 0 && search.IdChucDanh != 0)
                {
                    result = await _appDbContext.tblNhanVien.Where(nv => nv.IdChucDanh == search.IdChucDanh).ToListAsync();
                }
                else if (search.TenNV != null && search.IdPhongBan != 0 && search.IdChucDanh == 0)
                {
                    result = await _appDbContext.tblNhanVien.Where(nv => nv.TenNV.Contains(search.TenNV) && nv.IdPhongBan == search.IdPhongBan).ToListAsync();
                }
                else if(search.TenNV != null && search.IdPhongBan == 0 && search.IdChucDanh != 0)
                {
                    result = await _appDbContext.tblNhanVien.Where(nv => nv.TenNV.Contains(search.TenNV) && nv.IdChucDanh == search.IdChucDanh).ToListAsync();
                }
                else if (search.TenNV == null && search.IdPhongBan != 0 && search.IdChucDanh != 0)
                {
                    result = await _appDbContext.tblNhanVien.Where(nv => nv.IdPhongBan == search.IdPhongBan && nv.IdChucDanh == search.IdChucDanh).ToListAsync();
                }
                else if (search.TenNV != null && search.IdPhongBan != 0 && search.IdChucDanh != 0)
                {
                    result = await _appDbContext.tblNhanVien.Where(nv => nv.TenNV.Contains(search.TenNV) && nv.IdPhongBan == search.IdPhongBan && nv.IdChucDanh == search.IdChucDanh).ToListAsync();
                }
            }
            return result;
        }

        public async Task DeleteNhanVien(int id)
        { 
            var nhanvien = await _appDbContext.tblNhanVien.FirstOrDefaultAsync(nv=>nv.Id == id);
            if (nhanvien != null) { _appDbContext.tblNhanVien.Remove(nhanvien); }
            await _appDbContext.SaveChangesAsync();
        }
        public async Task EditNhanVien(NhanVien edited, string filePath)
        {
            var nhanvien = await _appDbContext.tblNhanVien.FirstOrDefaultAsync(nv=>nv.Id == edited.Id);
            if (nhanvien != null)
            {
                nhanvien.TenNV = edited.TenNV;
                nhanvien.MaNV = edited.MaNV;
                nhanvien.IdPhongBan = edited.IdPhongBan;
                nhanvien.IdChucDanh = edited.IdChucDanh;
                nhanvien.CCCD = edited.CCCD;
                nhanvien.DiaChi = edited.DiaChi;
                nhanvien.NgaySinh = edited.NgaySinh;
                nhanvien.Sdt = edited.Sdt;
                nhanvien.TrangThai = edited.TrangThai;
                nhanvien.AnhNhanVien = filePath;
            }
            await _appDbContext.SaveChangesAsync();
        }
        public async Task EditNhanVien(NhanVien edited)
        {
            var nhanvien = await _appDbContext.tblNhanVien.FirstOrDefaultAsync(nv => nv.Id == edited.Id);
            if (nhanvien != null)
            {
                nhanvien.TenNV = edited.TenNV;
                nhanvien.MaNV = edited.MaNV;
                nhanvien.IdPhongBan = edited.IdPhongBan;
                nhanvien.IdChucDanh = edited.IdChucDanh;
                nhanvien.CCCD = edited.CCCD;
                nhanvien.DiaChi = edited.DiaChi;
                nhanvien.NgaySinh = edited.NgaySinh;
                nhanvien.Sdt = edited.Sdt;
                nhanvien.TrangThai = edited.TrangThai;
            }
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<NhanVien> GetProfileNhanVien(int idnv)
        {
            var nhanvien = await _appDbContext.tblNhanVien
                .Include(nv => nv.CapChungChis).ThenInclude(capccs => capccs.KhoaHoc).ThenInclude(kh => kh.ChungChi).ThenInclude(cc=>cc.NhomChungChi)
                .Include(nv => nv.CapChungChis).ThenInclude(capccs => capccs.KhoaHoc).ThenInclude(kh=>kh.QuyetDinh)
                .Include(nv=>nv.CapChungChis).ThenInclude(capccs=>capccs.LoaiDaoTao)
                .Include(nv => nv.ChucDanh)
                .Include(nv => nv.PhongBan)
                .FirstOrDefaultAsync(nv => nv.Id == idnv);
            return nhanvien;
        }

        public async Task<List<NhanVien>> GetNhanViensNotInKhoaHoc(int idkh)
        {
            var capCCs = await _appDbContext.tblCapChungChi.Where(capcc=> capcc.IdKhoaHoc == idkh).ToListAsync();
            var idNVs = capCCs.Select(c => c.IdNhanVien).ToList();
            return await _appDbContext.tblNhanVien.Where(nv => !idNVs.Contains(nv.Id)).ToListAsync();
        }
        public async Task CreateNhanVien(CreateNhanVien nhanvien, string filePath)
        {
            var newNhanVien = new NhanVien
            {
                TenNV = nhanvien.NhanVien.TenNV,
                MaNV = nhanvien.NhanVien.MaNV,
                CCCD = nhanvien.NhanVien.CCCD,
                NgaySinh = nhanvien.NhanVien.NgaySinh,
                DiaChi = nhanvien.NhanVien.DiaChi,
                Sdt = nhanvien.NhanVien.Sdt,
                IdPhongBan = nhanvien.NhanVien.IdPhongBan,
                IdChucDanh = nhanvien.NhanVien.IdChucDanh,
                AnhNhanVien = filePath
            };
            await _appDbContext.AddAsync<NhanVien>(newNhanVien);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<NhanVien>> GetAllNhanVien()
        {
            return await _appDbContext.tblNhanVien.ToListAsync();
        }
    }
}
