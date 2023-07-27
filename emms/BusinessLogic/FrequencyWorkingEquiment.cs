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
    public class FrequencyWorkingEquiment
    {
        public static tblTanSuatHoatDong GetFrequencyById(int idFrequency)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblTanSuatHoatDongs
                         where p.ID == idFrequency
                         select p).FirstOrDefault();
            return query;
        }

        public static tblTanSuatHoatDong InsertFrequency(tblTanSuatHoatDong frequency)
        {
            tblTanSuatHoatDong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblTanSuatHoatDong
                {
                    IDTrangThietBi = frequency.IDTrangThietBi,
                    SoLuongTanSuat = frequency.SoLuongTanSuat,
                    NgayBatDauApDung = frequency.NgayBatDauApDung
                };
                result = db.tblTanSuatHoatDongs.Add(frequency);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateFrequency(tblTanSuatHoatDong frequency)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblTanSuatHoatDongs.Find(frequency.ID); //tìm dòng trong bảng tblTanSuatHoatDong tương ứng với khóa
                if (result != null)
                {
                    result.IDTrangThietBi = frequency.IDTrangThietBi;
                    result.SoLuongTanSuat = frequency.SoLuongTanSuat;
                    result.NgayBatDauApDung = frequency.NgayBatDauApDung;
                    
                    db.tblTanSuatHoatDongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static int DeleteFrequency(int idFrequency)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblTanSuatHoatDongs.Find(idFrequency);
                if (result != null)
                {
                    db.tblTanSuatHoatDongs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchFrequency(int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo, 
                                                int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchFrequency", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

                cmd.Parameters.AddWithValue("@BienSo", bienSo);
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
