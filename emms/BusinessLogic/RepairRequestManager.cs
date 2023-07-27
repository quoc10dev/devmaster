using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Helper;
using DataAccess;

namespace BusinessLogic
{
   
    public class RepairRequestManager
    {
        #region Repair Request

        public static tblPhieuSuaChua GetRepairRequestById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblPhieuSuaChuas
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblPhieuSuaChua InsertRepairRequest(tblPhieuSuaChua item)
        {
            tblPhieuSuaChua result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblPhieuSuaChua
                {
                    ID = item.ID,
                    MaPhieu = item.MaPhieu,
                    IDTrangThietBi = item.IDTrangThietBi,
                    NguoiSuaChua = item.NguoiSuaChua,
                    NgaySuaChua = item.NgaySuaChua,
                    NguoiNhap = item.NguoiNhap,
                    NgayNhap = item.NgayNhap,
                    SoGioHoacKm = item.SoGioHoacKm,
                    NgayVaoXuong = item.NgayVaoXuong,
                    NgayXuatXuong = item.NgayXuatXuong
                };
                result = db.tblPhieuSuaChuas.Add(item);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateRepairRequest(tblPhieuSuaChua item)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblPhieuSuaChuas.Find(item.ID);
                if (result != null)
                {
                    result.MaPhieu = item.MaPhieu;
                    result.IDTrangThietBi = item.IDTrangThietBi;
                    result.NguoiSuaChua = item.NguoiSuaChua;
                    result.NgaySuaChua = item.NgaySuaChua;
                    result.NgaySua = item.NgaySua;
                    result.NguoiSua = item.NguoiSua;
                    result.SoGioHoacKm = item.SoGioHoacKm;
                    result.NgayVaoXuong = item.NgayVaoXuong;
                    result.NgayXuatXuong = item.NgayXuatXuong;
                    result.PathOfFileUpload = item.PathOfFileUpload;
                    db.tblPhieuSuaChuas.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteRepairRequest(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblPhieuSuaChuas.Find(id);
                if (result != null)
                {
                    db.tblPhieuSuaChuas.Remove(result); 
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchRepairRequest(DateTime ngayNhapTuNgay, DateTime ngayNhapDenNgay,
                                                DateTime ngayBaoDuongTuNgay, DateTime ngayBaoDuongDenNgay,
                                                 int idCongTy, int idKieuBaoDuong, int idNhomTrangThietBi, int idLoaiTrangThietBi, 
                                                 string maThe, string bienSo,
                                                int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_PhieuSuaChua", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);
                if (idKieuBaoDuong > 0)
                    cmd.Parameters.AddWithValue("@IDKieuBaoDuong", idKieuBaoDuong);
                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);
                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@MaPhieu", maThe);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);

                cmd.Parameters.AddWithValue("@NgayNhapTuNgay", ngayNhapTuNgay);
                cmd.Parameters.AddWithValue("@NgayNhapDenNgay", ngayNhapDenNgay);
                cmd.Parameters.AddWithValue("@NgaySuaChuaTuNgay", ngayBaoDuongTuNgay);
                cmd.Parameters.AddWithValue("@NgaySuaChuaDenNgay", ngayBaoDuongDenNgay);

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

        public static DataTable ReportRepairRequest(DateTime ngayNhapTuNgay, DateTime ngayNhapDenNgay,
                                                DateTime ngayBaoDuongTuNgay, DateTime ngayBaoDuongDenNgay,
                                                 int idCongTy, int idKieuBaoDuong, int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo,
                                                 string trangThai)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoDanhSachBaoDuong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);
                if (idKieuBaoDuong > 0)
                    cmd.Parameters.AddWithValue("@IDKieuBaoDuong", idKieuBaoDuong);
                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);
                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);

                cmd.Parameters.AddWithValue("@NgayNhapTuNgay", ngayNhapTuNgay);
                cmd.Parameters.AddWithValue("@NgayNhapDenNgay", ngayNhapDenNgay);
                cmd.Parameters.AddWithValue("@NgayBaoDuongTuNgay", ngayBaoDuongTuNgay);
                cmd.Parameters.AddWithValue("@NgayBaoDuongDenNgay", ngayBaoDuongDenNgay);

                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
               
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataTable ReportRepairRequest(DateTime ngayNhapTuNgay, DateTime ngayNhapDenNgay,
                                                DateTime ngayBaoDuongTuNgay, DateTime ngayBaoDuongDenNgay,
                                                 int idCongTy, int idKieuBaoDuong, int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoLichSuSuaChua", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);
                if (idKieuBaoDuong > 0)
                    cmd.Parameters.AddWithValue("@IDKieuBaoDuong", idKieuBaoDuong);
                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);
                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);

                cmd.Parameters.AddWithValue("@NgayNhapTuNgay", ngayNhapTuNgay);
                cmd.Parameters.AddWithValue("@NgayNhapDenNgay", ngayNhapDenNgay);
                cmd.Parameters.AddWithValue("@NgaySuaChuaTuNgay", ngayBaoDuongTuNgay);
                cmd.Parameters.AddWithValue("@NgaySuaChuaDenNgay", ngayBaoDuongDenNgay);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        #endregion
    }
}
