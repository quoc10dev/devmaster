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
    public class FuleProcessingManager
    {
        public static tblNapNhienLieu GetFuleProcessingById(int id)
        {
            
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblNapNhienLieux
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblNapNhienLieu GetFuleProcessingByIdEquipmentAndDate(int idEquipment, DateTime ngayNap)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblNapNhienLieux
                         where p.IDTrangThietBi == idEquipment && p.NgayNap == ngayNap
                         select p).FirstOrDefault();
            return query;
        }

        public static tblNapNhienLieu InsertFuleProcessing(tblNapNhienLieu item)
        {
            tblNapNhienLieu result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblNapNhienLieu
                {
                    IDTrangThietBi = item.IDTrangThietBi,
                    NgayNap = item.NgayNap,
                    SoLuong = item.SoLuong
                };
                result = db.tblNapNhienLieux.Add(item);
                db.SaveChanges();
                return result;
            };
        }

        public static int UpdateFuleProcessing(tblNapNhienLieu item)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblNapNhienLieux.Find(item.ID);
                if (result != null)
                {
                    result.IDTrangThietBi = item.IDTrangThietBi;
                    result.NgayNap = item.NgayNap;
                    result.SoLuong = item.SoLuong;

                    db.tblNapNhienLieux.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteFuleProcessing(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblNapNhienLieux.Find(id);
                if (result != null)
                {
                    db.tblNapNhienLieux.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchFuleProcessing(DateTime tuNgay, DateTime denNgay, int idCongTy, int idNhomTrangThietBi, int idLoaiTrangThietBi,
                                   string bienSo, string ten, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_NapNhienLieu", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (tuNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);

                if (denNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

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

        public static DataTable SearchFuleProcessingEveryWeek(DateTime tuNgay, DateTime denNgay, int idCongTy, 
                                    int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DinhMucNhienLieu", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (tuNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@StartDate", tuNgay);

                if (denNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@EndDate", denNgay);

                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static void UpdateNapNhienLieu(int idTrangThietBi, string ngayNap, double? soLuong)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateNapNhienLieu", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTrangThietBi", idTrangThietBi);
                cmd.Parameters.AddWithValue("@NgayNap", ngayNap);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
}
