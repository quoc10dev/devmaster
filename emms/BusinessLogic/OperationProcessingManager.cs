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
    public class OperationProcessingManager
    {
        public static tblNhatKyHoatDong GetOperationProcessingById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblNhatKyHoatDongs
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblNhatKyHoatDong GetOperationProcessingByIdEquipmentAndDate(int idEquipment, DateTime ngayHoatDong)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblNhatKyHoatDongs
                         where p.IDTrangThietBi == idEquipment && p.NgayHoatDong == ngayHoatDong
                         select p).FirstOrDefault();
            return query;
        }

        public static tblNhatKyHoatDong InsertOperationProcessing(tblNhatKyHoatDong item)
        {
            tblNhatKyHoatDong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblNhatKyHoatDong
                {
                    IDTrangThietBi = item.IDTrangThietBi,
                    SoGio = item.SoGio,
                    SoKm = item.SoKm,
                    NgayHoatDong = item.NgayHoatDong,
                    GhiChu = item.GhiChu
                };
                result = db.tblNhatKyHoatDongs.Add(item);
                db.SaveChanges();
                return result;
            };
        }

        public static int UpdateOperationProcessing(tblNhatKyHoatDong item)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblNhatKyHoatDongs.Find(item.ID);
                if (result != null)
                {
                    result.IDTrangThietBi = item.IDTrangThietBi;
                    result.NgayHoatDong = item.NgayHoatDong;
                    result.SoGio = item.SoGio;
                    result.SoKm = item.SoKm;
                    result.GhiChu = item.GhiChu;

                    db.tblNhatKyHoatDongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteOperationProcessing(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblNhatKyHoatDongs.Find(id);
                if (result != null)
                {
                    db.tblNhatKyHoatDongs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchOperationProcessing(DateTime tuNgay, DateTime denNgay, int idCongTy, int idNhomTrangThietBi, int idLoaiTrangThietBi, 
                                    string bienSo, string ten, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_NhatKyHoatDong", cnn);
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

        public static DataTable SearchOperationProcessingTest(DateTime tuNgay, DateTime denNgay, int idCongTy, 
                                    int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_NhatKyHoatDong", cnn);
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

        public static DataTable SearchActivityHistory(DateTime tuNgay, DateTime denNgay, int idCongTy,
                                    int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_NhatKyHoatDongThucTe", cnn);
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

        public static void TinhTanSuatHoatDongThucTe(DateTime tuNgay, DateTime denNgay)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_TinhTanSuatHoatDongThucTe", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (tuNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);

                if (denNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public static void UpdateNhatKyHoatDong(int idTrangThietBi, string ngayHoatDong, double? soGio_SoKm)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateNhatKyHoatDong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTrangThietBi", idTrangThietBi);
                cmd.Parameters.AddWithValue("@NgayHoatDong", ngayHoatDong);
                cmd.Parameters.AddWithValue("@SoGio_SoKm", soGio_SoKm);
               
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public static DataTable GetDataFromGPS(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ReportSkySoftData", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (tuNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@StartDate", tuNgay);

                if (denNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@EndDate", denNgay);

                /*
                if (idCongTy > 0)
                    cmd.Parameters.AddWithValue("@IDCongTy", idCongTy);

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                */

                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static void TransferDataFromGPS(DateTime tuNgay, DateTime denNgay)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_TransferDataFromGPS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (tuNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@StartDate", tuNgay);

                if (denNgay != DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@EndDate", denNgay);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
}
