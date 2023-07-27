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
    public class FailureManager
    {
        public static tblLoaiHuHong GetFailureById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblLoaiHuHongs
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblLoaiHuHong InsertFailure(tblLoaiHuHong failure)
        {
            tblLoaiHuHong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblLoaiHuHong
                {
                    Ten = failure.Ten,
                    SoThuTuHienThi = failure.SoThuTuHienThi
                };
                result = db.tblLoaiHuHongs.Add(failure);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateFailure(tblLoaiHuHong failure)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblLoaiHuHongs.Find(failure.ID); //tìm dòng trong bảng tblLoaiHuHong tương ứng với khóa
                if (result != null)
                {
                    result.Ten = failure.Ten;
                    result.SoThuTuHienThi = failure.SoThuTuHienThi;
                    

                    db.tblLoaiHuHongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static int DeleteFailure(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblLoaiHuHongs.Find(id);
                if (result != null)
                {
                    db.tblLoaiHuHongs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchFailure(string name, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchFailure", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

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
