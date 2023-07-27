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
    public class MaintenanceManager
    {
        public static tblHangMucBaoDuong GetMaintenanceById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblHangMucBaoDuongs
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblHangMucBaoDuong InsertMaintenance(tblHangMucBaoDuong maintenance)
        {
            tblHangMucBaoDuong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblHangMucBaoDuong
                {
                    ID = maintenance.ID,
                    Ten = maintenance.Ten,
                    TenEng = maintenance.TenEng,
                    SoThuTuHienThi = maintenance.SoThuTuHienThi,
                    IDNhomHangMucBaoDuong = maintenance.IDNhomHangMucBaoDuong,
                    GhiChu = maintenance.GhiChu
                };
                result = db.tblHangMucBaoDuongs.Add(maintenance);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateMaintenance(tblHangMucBaoDuong maintenance)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblHangMucBaoDuongs.Find(maintenance.ID);
                if (result != null)
                {
                    result.Ten = maintenance.Ten;
                    result.TenEng = maintenance.TenEng;
                    result.SoThuTuHienThi = maintenance.SoThuTuHienThi;
                    result.IDNhomHangMucBaoDuong = maintenance.IDNhomHangMucBaoDuong;
                    result.GhiChu = maintenance.GhiChu;

                    db.tblHangMucBaoDuongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteMaintenance(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblHangMucBaoDuongs.Find(id);
                if (result != null)
                {
                    db.tblHangMucBaoDuongs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchMaintenance(int idNhomHangMucBaoDuong, string name, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchMaintenance", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDNhomHangMucBaoDuong", idNhomHangMucBaoDuong);
                cmd.Parameters.AddWithValue("@Ten", name);
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

        public static DataTable SearchAllMaintenance(int idNhomHangMucBaoDuong, string name, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_GetAllMaintenance", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdMaintenanceGroup", idNhomHangMucBaoDuong);
                //cmd.Parameters.AddWithValue("@Ten", name);
                //cmd.Parameters.AddWithValue("@PageNo", pageIndex);
                //cmd.Parameters.AddWithValue("@PageSize", pageSize);
                //cmd.Parameters.AddWithValue("@SortColumn", sortExpression);
                //cmd.Parameters.AddWithValue("@SortOrder", softOrder);

                //SqlParameter paraTotalRecord = new SqlParameter("@TotalRecord", System.Data.SqlDbType.Int);
                //paraTotalRecord.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(paraTotalRecord);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();

                //if (paraTotalRecord.Value != null)
                //    int.TryParse(paraTotalRecord.Value.ToString(), out totalRecord);
            }
            return dt;
        }

        public static DataTable GetAllMaintenanceForCreateMaintenanceForm()
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllMaintenance", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }
    }
}
