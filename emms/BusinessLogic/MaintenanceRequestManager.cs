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
    public class MaintenanceRequestState
    {
        public const string NhapMoi = "nhap_moi";
        public const string HoanThanh = "hoan_thanh";
    }

    public class MaintenanceRequestValue
    {
        public const string NhapMoi = "Đang xử lý";
        public const string HoanThanh = "Đã hoàn thành";
    }

    public class MaintenanceRequestManager
    {
        #region Maintenance Request

        public static string GetStateOfMaintenanceRequest(string state)
        {
            if (state.Equals(MaintenanceRequestState.NhapMoi))
                return MaintenanceRequestValue.NhapMoi;
            else if (state.Equals(MaintenanceRequestState.HoanThanh))
                return MaintenanceRequestValue.HoanThanh;
            return string.Empty;
        }

        public static tblTheBaoDuong GetMaintenanceRequestById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblTheBaoDuongs
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblTheBaoDuong InsertMaintenanceRequest(tblTheBaoDuong item)
        {
            tblTheBaoDuong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblTheBaoDuong
                {
                    ID = item.ID,
                    MaThe = item.MaThe,
                    IDTrangThietBi = item.IDTrangThietBi,
                    IDHuHong = item.IDHuHong,
                    NguoiBaoDuong = item.NguoiBaoDuong,
                    NgayBaoDuong = item.NgayBaoDuong,
                    TrangThaiKhaiThac = item.TrangThaiKhaiThac,
                    IDCapBaoDuong = item.IDCapBaoDuong,
                    NguoiNhap = item.NguoiNhap,
                    NgayNhap = item.NgayNhap,
                    SoGioHoacKm = item.SoGioHoacKm,
                    NgayVaoXuong = item.NgayVaoXuong,
                    NgayXuatXuong = item.NgayXuatXuong
                };
                result = db.tblTheBaoDuongs.Add(item);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateMaintenanceRequest(tblTheBaoDuong item)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblTheBaoDuongs.Find(item.ID);
                if (result != null)
                {
                    result.MaThe = item.MaThe;
                    result.IDTrangThietBi = item.IDTrangThietBi;
                    result.IDHuHong = item.IDHuHong;
                    result.NguoiBaoDuong = item.NguoiBaoDuong;
                    result.NgayBaoDuong = item.NgayBaoDuong;
                    result.TrangThaiKhaiThac = item.TrangThaiKhaiThac;
                    result.IDCapBaoDuong = item.IDCapBaoDuong;
                    result.NgaySua = item.NgaySua;
                    result.NguoiSua = item.NguoiSua;
                    result.SoGioHoacKm = item.SoGioHoacKm;
                    result.NgayVaoXuong = item.NgayVaoXuong;
                    result.NgayXuatXuong = item.NgayXuatXuong;
                    result.PathOfFileUpload = item.PathOfFileUpload;

                    db.tblTheBaoDuongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteMaintenanceRequest(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblTheBaoDuongs.Find(id);
                if (result != null)
                {
                    //Xóa trong bảng tblChiTietBaoDuongCongViec
                    List<tblChiTietBaoDuong> obj = db.tblChiTietBaoDuongs.Where(w => w.IDTheBaoDuong == id).ToList<tblChiTietBaoDuong>();
                    foreach (tblChiTietBaoDuong item in obj)
                        DeleteMaintenanceDetailTask(item.ID);
                    db.tblChiTietBaoDuongs.RemoveRange(obj); //Xóa trong bảng tblChiTietBaoDuong
                    db.tblTheBaoDuongs.Remove(result); //Xóa trong bảng tblTheoBaoDuong
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchMaintenanceRequest(DateTime ngayNhapTuNgay, DateTime ngayNhapDenNgay,
                                                DateTime ngayBaoDuongTuNgay, DateTime ngayBaoDuongDenNgay,
                                                 int idCongTy, int idKieuBaoDuong, int idNhomTrangThietBi, int idLoaiTrangThietBi, 
                                                 string maThe, string bienSo, string trangThai,
                                                int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_TheBaoDuong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);
                if (idKieuBaoDuong > 0)
                    cmd.Parameters.AddWithValue("@IDKieuBaoDuong", idKieuBaoDuong);
                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);
                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@MaThe", maThe);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);

                cmd.Parameters.AddWithValue("@NgayNhapTuNgay", ngayNhapTuNgay);
                cmd.Parameters.AddWithValue("@NgayNhapDenNgay", ngayNhapDenNgay);
                cmd.Parameters.AddWithValue("@NgayBaoDuongTuNgay", ngayBaoDuongTuNgay);
                cmd.Parameters.AddWithValue("@NgayBaoDuongDenNgay", ngayBaoDuongDenNgay);

                cmd.Parameters.AddWithValue("@TrangThai", trangThai);

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

        public static string GetTaskNameListForPrint()
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetTaskListForPrint", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paraTaskNameList = new SqlParameter("@TaskNameList", System.Data.SqlDbType.NVarChar, 4000);
                paraTaskNameList.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paraTaskNameList);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                if (paraTaskNameList.Value != null)
                    result = paraTaskNameList.Value.ToString();
            }
            return result;
        }

        public static DataTable ReportMaintenanceRequest(DateTime ngayNhapTuNgay, DateTime ngayNhapDenNgay,
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

        public static void CapNhatThongTinBaoDuong(int idTheBaoDuong)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateThongTinBaoDuongVaoTrangThietBi", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDTheBaoDuong", idTheBaoDuong);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        #endregion

        #region Maintenance Request Detail

        public static tblChiTietBaoDuong GetMaintenanceDetailById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblChiTietBaoDuongs
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblChiTietBaoDuong GetMaintenanceBillDetail(int idTheBaoDuong, int idHangMucBaoDuong)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblChiTietBaoDuongs
                         where p.IDTheBaoDuong == idTheBaoDuong && p.IDHangMucBaoDuong == idHangMucBaoDuong
                         select p).FirstOrDefault();
            return query;
        }

        public static tblChiTietBaoDuong InsertMaintenanceDetail(tblChiTietBaoDuong item)
        {
            tblChiTietBaoDuong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblChiTietBaoDuong
                {
                    IDTheBaoDuong = item.IDTheBaoDuong,
                    IDHangMucBaoDuong = item.IDHangMucBaoDuong,
                    IsChecked = item.IsChecked,
                    IsRequiresRepair = item.IsRequiresRepair,
                    GhiChu = item.GhiChu
                };
                result = db.tblChiTietBaoDuongs.Add(item);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateMaintenanceDetail(tblChiTietBaoDuong item)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblChiTietBaoDuongs.Find(item.ID);
                if (result != null)
                {
                    result.IDTheBaoDuong = item.IDTheBaoDuong;
                    result.IDHangMucBaoDuong = item.IDHangMucBaoDuong;
                    result.IsChecked = item.IsChecked;
                    result.IsRequiresRepair = item.IsRequiresRepair;
                    result.GhiChu = item.GhiChu;

                    db.tblChiTietBaoDuongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteMaintenanceDetail(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblChiTietBaoDuongs.Find(id);
                if (result != null)
                {
                    db.tblChiTietBaoDuongs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable GetMaintenanceForTicket()
        {
            //totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LayDanhSachHangMucBaoDuong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataSet GetMaintenanceDetail(int idMaintenance)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetMaintenanceDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idMaintenance", idMaintenance);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            return ds;
        }

        public static DataSet GetMaintenanceFromEqupmentType(int idLoaiTrangThietBi)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LayNoiDungBaoDuongTheoLoaiXe", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            return ds;
        }

        #endregion

        #region Maintenance Request Detail - Task

        public static tblChiTietBaoDuongCongViec GetMaintenanceTaskById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblChiTietBaoDuongCongViecs
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblChiTietBaoDuongCongViec InsertMaintenanceTask(tblChiTietBaoDuongCongViec item)
        {
            tblChiTietBaoDuongCongViec result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblChiTietBaoDuongCongViec
                {
                    ID = item.ID,
                    IDChiTietBaoDuong = item.IDChiTietBaoDuong,
                    IDCongViec = item.IDCongViec
                };
                result = db.tblChiTietBaoDuongCongViecs.Add(item);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateMaintenanceTask(tblChiTietBaoDuongCongViec item)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblChiTietBaoDuongCongViecs.Find(item.ID);
                if (result != null)
                {
                    result.IDChiTietBaoDuong = item.IDChiTietBaoDuong;
                    result.IDCongViec = item.IDCongViec;

                    db.tblChiTietBaoDuongCongViecs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteMaintenanceTask(long id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblChiTietBaoDuongCongViecs.Find(id);
                if (result != null)
                {
                    db.tblChiTietBaoDuongCongViecs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static int DeleteMaintenanceDetailTask(long idChiTietBaoDuong)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                List<tblChiTietBaoDuongCongViec> obj = db.tblChiTietBaoDuongCongViecs.Where(w => w.IDChiTietBaoDuong == idChiTietBaoDuong).ToList<tblChiTietBaoDuongCongViec>();
                db.tblChiTietBaoDuongCongViecs.RemoveRange(obj);
                record = db.SaveChanges();
            }
            return record;
        }

        public static List<tblChiTietBaoDuongCongViec> GetMaintenanceDetailTask(long idChiTietBaoDuong)
        {
            EMMEntities data = new EMMEntities();
            var equipList = data.tblChiTietBaoDuongCongViecs.ToList();
            var equipItems = from e in equipList
                             where e.IDChiTietBaoDuong == idChiTietBaoDuong
                             select new tblChiTietBaoDuongCongViec
                             {
                                 ID = e.ID,
                                 IDChiTietBaoDuong = e.IDChiTietBaoDuong,
                                 IDCongViec = e.IDCongViec
                             };
            List<tblChiTietBaoDuongCongViec> ls = new List<tblChiTietBaoDuongCongViec>(equipItems.ToList());
            return ls;
        }

        #endregion

        #region Maintenance Detail - Maintenance type

        public static DataTable SearchMaintenanceForm(int idKieuBaoDuong, int idLoaiBaoDuong, int idCapBaoDuong,
                                               int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_HangMucCapBaoDuong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idCapBaoDuong > 0)
                    cmd.Parameters.AddWithValue("@IDCapBaoDuong", idCapBaoDuong);

                if (idLoaiBaoDuong > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiBaoDuong", idLoaiBaoDuong);

                if (idKieuBaoDuong > 0)
                    cmd.Parameters.AddWithValue("@IDKieuBaoDuong", idKieuBaoDuong);

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

        #endregion

        #region Maintenance Request Form

        public static DataTable GetMaintenanceRequestFormForCreateForm(string maintenanceList)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LayHangMucBaoDuongDeTaoMauForm", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maintenanceList", maintenanceList);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataTable GetMaintenanceByEquipmentType(int idLoaiTrangThietBi)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LayHangMucBaoDuongTheoLoaiTrangThietBi", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static int DeleteMaintenanceRequestForm(int idLoaiTrangThietBi)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                List<tblHangMucLoaiTrangThietBi> obj = db.tblHangMucLoaiTrangThietBis.Where(w => w.IDLoaiTrangThietBi == idLoaiTrangThietBi).ToList<tblHangMucLoaiTrangThietBi>();

                foreach (tblHangMucLoaiTrangThietBi item in obj)
                {
                    DeleteTaskInMaintenance(item.ID);
                }

                db.tblHangMucLoaiTrangThietBis.RemoveRange(obj);
                record = db.SaveChanges();
            }
            return record;
        }

        public static int DeleteMaintenanceRequestForm(int idMaintenance, int idLoaiTrangThietBi)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                List<tblHangMucLoaiTrangThietBi> obj = db.tblHangMucLoaiTrangThietBis.Where(w => w.IDHangMuc == idMaintenance
                                                    && w.IDLoaiTrangThietBi == idLoaiTrangThietBi).ToList<tblHangMucLoaiTrangThietBi>();

                foreach (tblHangMucLoaiTrangThietBi item in obj)
                {
                    DeleteTaskInMaintenance(item.ID);
                }

                db.tblHangMucLoaiTrangThietBis.RemoveRange(obj);
                record = db.SaveChanges();
            }
            return record;
        }

        public static int DeleteTaskInMaintenance(int idHangMucTrangThietBi)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var obj = db.tblCapBaoDuongCongViecs.Where(w => w.IDHangMucLoaiTrangThietBi == idHangMucTrangThietBi).ToList<tblCapBaoDuongCongViec>();
                db.tblCapBaoDuongCongViecs.RemoveRange(obj);
                record = db.SaveChanges();
            }
            return record;
        }

        #endregion

        #region Cấp bảo dưỡng - công việc

        public static tblCapBaoDuongCongViec InsertCapBaoDuongCongViec(tblCapBaoDuongCongViec item)
        {
            tblCapBaoDuongCongViec result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblCapBaoDuongCongViec
                {
                    IDHangMucLoaiTrangThietBi = item.IDHangMucLoaiTrangThietBi,
                    IDCapBaoDuong = item.IDCapBaoDuong,
                    IDCongViec = item.IDCongViec
                };
                result = db.tblCapBaoDuongCongViecs.Add(item);
                db.SaveChanges();

                return result;
            };
        }

        public static List<tblCapBaoDuongCongViec> GetMaintenanceLevelTask(int idHangMucLoaiTTB, int idCapBaoDuong)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblCapBaoDuongCongViecs
                         where p.IDHangMucLoaiTrangThietBi == idHangMucLoaiTTB && p.IDCapBaoDuong == idCapBaoDuong
                         select p).ToList<tblCapBaoDuongCongViec>();
            return query;
        }

        public static List<tblCapBaoDuongCongViec> GetMaintenanceLevelTask(int idHangMucLoaiTTB, int idCapBaoDuong, int idCongViec)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblCapBaoDuongCongViecs
                         where p.IDHangMucLoaiTrangThietBi == idHangMucLoaiTTB && p.IDCapBaoDuong == idCapBaoDuong && p.IDCongViec == idCongViec
                         select p).ToList<tblCapBaoDuongCongViec>();
            return query;
        }

        public static int DeleteMaintenanceLevelTask(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblCapBaoDuongCongViecs.Find(id);
                if (result != null)
                {
                    db.tblCapBaoDuongCongViecs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        #endregion
    }
}
