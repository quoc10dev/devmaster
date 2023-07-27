using BusinessLogic.Helper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class EquipmentManager
    {
        public static List<tblTrangThietBi> GetEquipmentByIDEquimentType(int idEquipmentType)
        {
            EMMEntities data = new EMMEntities();
            var equipList = data.tblTrangThietBis.ToList();
            var equipItems = from e in equipList
                             where e.IDLoaiTrangThietBi == idEquipmentType
                             select new tblTrangThietBi
                             {
                                 ID = e.ID,
                                 MaTaiSan = e.MaTaiSan,
                                 Ten = string.Format("{0}    ||    {1}", e.BienSo, e.Ten)
                             };
            List<tblTrangThietBi> ls = new List<tblTrangThietBi>(equipItems.ToList());
            return ls;
        }

        public static List<tblTrangThietBi> GetEquipmentByIDEquimentTypeForSelect(int idEquipmentType)
        {
            EMMEntities data = new EMMEntities();
            var equipList = data.tblTrangThietBis.ToList();
            var equipItems = from e in equipList
                             where e.IDLoaiTrangThietBi == idEquipmentType
                             select new tblTrangThietBi
                             {
                                 ID = e.ID,
                                 MaTaiSan = e.MaTaiSan,
                                 Ten = e.Ten,
                                 BienSo = e.BienSo,
                                 SoKhung = e.SoKhung,
                                 SoMay = e.SoMay,
                                 LoaiMay = e.LoaiMay
                             };
            List<tblTrangThietBi> ls = new List<tblTrangThietBi>(equipItems.ToList());
            return ls;
        }

        public static tblTrangThietBi GetEquipmentById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblTrangThietBis
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblTrangThietBi GetEquipmentByAssetCode(string assetCode)
        {
            //Tìm TTB theo mã tài sản
            //asset code: mã tài sản
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblTrangThietBis
                         where p.MaTaiSan == assetCode
                         select p).ToList();
            return (query != null && query.Count > 0) ? query[0] : null;
        }

        public static tblTrangThietBi GetEquipmentByAssetCode(int idExists, string assetCode)
        {
            //Tìm TTB theo biển số xe
            //idExists: id TTB đã insert
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblTrangThietBis
                         where p.ID != idExists && p.MaTaiSan == assetCode
                         select p).ToList();
            return (query != null && query.Count > 0) ? query[0] : null;
        }

        public static tblTrangThietBi GetEquipmentByLicensePlate(string licensePlate)
        {
            //Tìm TTB theo biển số xe
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblTrangThietBis
                         where p.BienSo == licensePlate
                         select p).ToList();
            return (query != null && query.Count > 0) ? query[0] : null;
        }

        public static tblTrangThietBi GetEquipmentByLicensePlate(int idExists, string licensePlate)
        {
            //Tìm TTB theo biển số xe
            //idExists: id TTB đã insert
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblTrangThietBis
                         where p.ID != idExists && p.BienSo == licensePlate
                         select p).ToList();
            return (query != null && query.Count > 0) ? query[0] : null;
        }


        public static tblTrangThietBi InsertEquipment(tblTrangThietBi equipment)
        {
            tblTrangThietBi result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblTrangThietBi
                {
                    ID = equipment.ID,
                    MaTaiSan = equipment.MaTaiSan,
                    Ten = equipment.Ten,
                    NamSanXuat = equipment.NamSanXuat,
                    NuocSanXuat = equipment.NuocSanXuat,
                    NgayDangKiemLanDau = equipment.NgayDangKiemLanDau,
                    SoThangDangKiemDinhKy = equipment.SoThangDangKiemDinhKy,
                    BienSo = equipment.BienSo,
                    SoKhung = equipment.SoKhung,
                    LoaiMay = equipment.LoaiMay,
                    SoMay = equipment.SoMay,
                    GhiChu = equipment.GhiChu,
                    IDCongTy = equipment.IDCongTy,
                    IDLoaiTrangThietBi = equipment.IDLoaiTrangThietBi,
                    NgayBaoDuongGanNhat = equipment.NgayBaoDuongGanNhat,
                    SoKmBaoDuongGanNhat = equipment.SoKmBaoDuongGanNhat,
                    SoGioBaoDuongGanNhat = equipment.SoGioBaoDuongGanNhat,
                    IDQuyTrinhBaoDuong = equipment.IDQuyTrinhBaoDuong,
                    CanhBaoDangKiem = equipment.CanhBaoDangKiem,
                    BaoDuongTheo = equipment.BaoDuongTheo
                };
                result = db.tblTrangThietBis.Add(equipment);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateEquipment(tblTrangThietBi equipment)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblTrangThietBis.Find(equipment.ID);
                if (result != null)
                {
                    result.MaTaiSan = equipment.MaTaiSan;
                    result.Ten = equipment.Ten;
                    result.NamSanXuat = equipment.NamSanXuat;
                    result.NuocSanXuat = equipment.NuocSanXuat;
                    result.NgayDangKiemLanDau = equipment.NgayDangKiemLanDau;
                    result.SoThangDangKiemDinhKy = equipment.SoThangDangKiemDinhKy;
                    result.BienSo = equipment.BienSo;
                    result.SoKhung = equipment.SoKhung;
                    result.LoaiMay = equipment.LoaiMay;
                    result.SoMay = equipment.SoMay;
                    result.GhiChu = equipment.GhiChu;
                    result.IDCongTy = equipment.IDCongTy;
                    result.IDLoaiTrangThietBi = equipment.IDLoaiTrangThietBi;
                    result.NgayBaoDuongGanNhat = equipment.NgayBaoDuongGanNhat;
                    result.SoKmBaoDuongGanNhat = equipment.SoKmBaoDuongGanNhat;
                    result.SoGioBaoDuongGanNhat = equipment.SoGioBaoDuongGanNhat;
                    result.IDQuyTrinhBaoDuong = equipment.IDQuyTrinhBaoDuong;
                    result.CanhBaoDangKiem = equipment.CanhBaoDangKiem;
                    result.BaoDuongTheo = equipment.BaoDuongTheo;

                    db.tblTrangThietBis.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteEquipment(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblTrangThietBis.Find(id);
                if (result != null)
                {
                    db.tblTrangThietBis.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchEquipment(int idCongTy, int idNhomTrangThietBi,  int idLoaiTrangThietBi, 
                                            string bienSo, string ten,
                                            int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_TrangThietBi", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);
                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);
                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);
                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@PageNo", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SortColumn", sortExpression);
                cmd.Parameters.AddWithValue("@SortOrder", softOrder);

                SqlParameter paraTotalRecord = new SqlParameter("@TotalRecord", System.Data.SqlDbType.Int);
                paraTotalRecord.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paraTotalRecord);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();

                if (paraTotalRecord.Value != null)
                    int.TryParse(paraTotalRecord.Value.ToString(), out totalRecord);
            }
            return dt;
        }

        public static DataTable SearchEquipmentToExport(int idCongTy, int idNhomTrangThietBi, int idLoaiTrangThietBi,
                                            string bienSo, string ten)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString(); 
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_TrangThietBi_Export", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);
                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);
                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);
                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@Ten", ten);
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataTable ReportCanhBaoNgayDangKiemTiepTheo(int idCongTy, int idNhomTrangThietBi, int idLoaiTrangThietBi, 
                                            string bienSo, string tenXe, int soNgayPhamViCanhBao)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_CanhBaoNgayDangKiemKeTiep", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@TenXe", tenXe);
                cmd.Parameters.AddWithValue("@SoNgayPhamViCanhBao", soNgayPhamViCanhBao);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataTable ReportTinhDinhMucNhienLieu(int idCongTy, int idNhomTrangThietBi, int idLoaiTrangThietBi, DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoDinhMucNhienLieu", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }
    }
}
