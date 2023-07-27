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
    public class MaintenanceLevelManager
    {
        public static tblCapBaoDuong GetMaintenanceLevelById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblCapBaoDuongs
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static List<tblCapBaoDuong> GetMaintenanceChildByIdLoaiBaoDuong(int idLoaiBaoDuong)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblCapBaoDuongs
                         where p.IDLoaiBaoDuong == idLoaiBaoDuong
                         select p).ToList<tblCapBaoDuong>();
            return query;
        }

        public static tblCapBaoDuong InsertMaintenanceLevel(tblCapBaoDuong maintenanceLevel)
        {
            tblCapBaoDuong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblCapBaoDuong
                {
                    ID = maintenanceLevel.ID,
                    Ten = maintenanceLevel.Ten,
                    SoLuongMoc = maintenanceLevel.SoLuongMoc,
                    GhiChu = maintenanceLevel.GhiChu,
                    IDLoaiBaoDuong = maintenanceLevel.IDLoaiBaoDuong,
                    ShowInPrintPosition1 = maintenanceLevel.ShowInPrintPosition1,
                    ShowInPrintPosition2 = maintenanceLevel.ShowInPrintPosition2,
                    LevelInPrint = maintenanceLevel.LevelInPrint
                };
                result = db.tblCapBaoDuongs.Add(maintenanceLevel);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateMaintenanceLevel(tblCapBaoDuong maintenanceLevel)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblCapBaoDuongs.Find(maintenanceLevel.ID);
                if (result != null)
                {
                    result.Ten = maintenanceLevel.Ten;
                    result.GhiChu = maintenanceLevel.GhiChu;
                    result.IDLoaiBaoDuong = maintenanceLevel.IDLoaiBaoDuong;
                    result.SoLuongMoc = maintenanceLevel.SoLuongMoc;
                    result.ShowInPrintPosition1 = maintenanceLevel.ShowInPrintPosition1;
                    result.ShowInPrintPosition2 = maintenanceLevel.ShowInPrintPosition2;
                    result.LevelInPrint = maintenanceLevel.LevelInPrint;

                    db.tblCapBaoDuongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteMaintenanceLevel(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblCapBaoDuongs.Find(id);
                if (result != null)
                {
                    db.tblCapBaoDuongs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchMaintenanceLevel(int idKieuBaoDuong, int idLoaiBaoDuong, string name, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchMaintenanceLevel", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDKieuBaoDuong", idKieuBaoDuong);
                cmd.Parameters.AddWithValue("@IDLoaiBaoDuong", idLoaiBaoDuong);
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
    }
}

