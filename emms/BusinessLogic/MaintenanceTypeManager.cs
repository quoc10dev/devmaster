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
using System.Web.UI.WebControls;

namespace BusinessLogic
{
    public class KieuBaoDuong
    {
        public const string Thang = "thang";
        public const string Km = "km";
        public const string Gio = "gio";
    }

    public class MaintenanceTypeManager
    {
        public static string GetNameOfMaintenanceType(string value)
        {
            if (value.Equals(KieuBaoDuong.Thang))
                return "Tháng";
            else if (value.Equals(KieuBaoDuong.Km))
                return "Km";
            else if (value.Equals(KieuBaoDuong.Gio))
                return "Giờ";

            return string.Empty;
        }

        public static tblKieuBaoDuong GetMaintenanceTypeById(int idKieuBaoDuong)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblKieuBaoDuongs
                         where p.ID == idKieuBaoDuong
                         select p).FirstOrDefault();
            return query;
        }

        public static List<tblKieuBaoDuong> GetAllMaintenanceType()
        {
            EMMEntities data = new EMMEntities();
            var maintenanceTypeList = data.tblKieuBaoDuongs.ToList();
            var maintenanceTypeItems = from e in maintenanceTypeList
                                       select new tblKieuBaoDuong
                                       {
                                           ID = e.ID,
                                           Ten = e.Ten,
                                           Ma = e.Ma
                                       };
            List<tblKieuBaoDuong> result = new List<tblKieuBaoDuong>(maintenanceTypeItems.ToList()); 
            return result;
        }

        public static void GetAllMaintenanceTypeForSearch(DropDownList dl)
        {
            List<tblKieuBaoDuong> result = GetAllMaintenanceType();
            tblKieuBaoDuong findAllItem = new tblKieuBaoDuong();
            findAllItem.ID = 0;
            findAllItem.Ten = "--- Select All ---";
            result.Insert(0, findAllItem);

            dl.DataSource = result;
            dl.DataTextField = "Ten";
            dl.DataValueField = "ID";
            dl.DataBind();

            dl.SelectedIndex = 0;
        }

        public static List<tblLoaiBaoDuong> GetMaintenanceChildTypeByIdKieuBaoDuong(int idKieuBaoDuong)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblLoaiBaoDuongs
                         where p.IDKieuBaoDuong == idKieuBaoDuong
                         select p).ToList<tblLoaiBaoDuong>();
            return query;
        }

        public static tblLoaiBaoDuong GetMaintenanceChildTypeById(int idLoaiBaoDuong)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblLoaiBaoDuongs
                         where p.IDLoaiBaoDuong == idLoaiBaoDuong
                         select p).FirstOrDefault();
            return query;
        }

        public static tblLoaiBaoDuong InsertMaintenanceChildType(tblLoaiBaoDuong maintenanceChildType)
        {
            tblLoaiBaoDuong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblLoaiBaoDuong
                {
                    IDLoaiBaoDuong = maintenanceChildType.IDLoaiBaoDuong,
                    IDKieuBaoDuong = maintenanceChildType.IDKieuBaoDuong,
                    Ten = maintenanceChildType.Ten,
                    SoLuongBaoDuongDinhKy = maintenanceChildType.SoLuongBaoDuongDinhKy
                };
                result = db.tblLoaiBaoDuongs.Add(maintenanceChildType);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateMaintenanceChildType(tblLoaiBaoDuong maintenanceChildType)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblLoaiBaoDuongs.Find(maintenanceChildType.IDLoaiBaoDuong); 
                if (result != null)
                {
                    result.IDKieuBaoDuong = maintenanceChildType.IDKieuBaoDuong;
                    result.Ten = maintenanceChildType.Ten;
                    result.SoLuongBaoDuongDinhKy = maintenanceChildType.SoLuongBaoDuongDinhKy;

                    db.tblLoaiBaoDuongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static int DeleteMaintenanceChildType(int idLoaiBaoDuong)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblLoaiBaoDuongs.Find(idLoaiBaoDuong);
                if (result != null)
                {
                    db.tblLoaiBaoDuongs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchMaintenanceChildType(int idKieuBaoDuong, string tenLoaiBaoDuong, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchLoaiBaoDuong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDKieuBaoDuong", idKieuBaoDuong);
                cmd.Parameters.AddWithValue("@TenLoaiBaoDuong", tenLoaiBaoDuong);
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

        public static DataTable WarningMaintenanceOld(int idCompany, int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo, int soNgayPhamViCanhBao)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_CanhBaoBaoDuongTheoThangCacXe", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDCongTy", idCompany);

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);
                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@SoNgayPhamViCanhBao", soNgayPhamViCanhBao);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataTable WarningMaintenance(int idCompany, int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo, int soNgayPhamViCanhBao)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoDuKienBaoDuong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDCongTy", idCompany);

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);
                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@SoNgayPhamViCanhBao", soNgayPhamViCanhBao);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataTable ReportMaintenancePlanOld(int idCompany, int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo, int soNgayPhamViCanhBao,
                                                        DateTime dinhMucTuNgay, DateTime dinhMucDenNgay, DateTime baoDuongTuNgay, DateTime baoDuongDenNgay)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoDuKienBaoDuongCacXe", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDCongTy", idCompany);

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@SoNgayPhamViCanhBao", soNgayPhamViCanhBao);

                cmd.Parameters.AddWithValue("@DinhMucTuNgay", dinhMucTuNgay);
                cmd.Parameters.AddWithValue("@DinhMucDenNgay", dinhMucDenNgay);
                cmd.Parameters.AddWithValue("@BaoDuongTuNgay", baoDuongTuNgay);
                cmd.Parameters.AddWithValue("@BaoDuongDenNgay", baoDuongDenNgay);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataTable ReportMaintenancePlan(int idCompany, int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo, 
                                                        int soNgayPhamViCanhBao, int soNgayGanNhatTinhTanSuat,
                                                        DateTime baoDuongTuNgay, DateTime baoDuongDenNgay)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoDuKienBaoDuong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDCongTy", idCompany);

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@SoNgayPhamViCanhBao", soNgayPhamViCanhBao);
                cmd.Parameters.AddWithValue("@SoNgayGanNhatTinhTanSuat", soNgayGanNhatTinhTanSuat);
                cmd.Parameters.AddWithValue("@BaoDuongTuNgay", baoDuongTuNgay);
                cmd.Parameters.AddWithValue("@BaoDuongDenNgay", baoDuongDenNgay);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }
    }
}
