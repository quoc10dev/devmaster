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

namespace BusinessLogic.Security
{
    public class UserManager
    {
        public static tblUser GetUserByUserName(string userName)
        {
            //Lấy tài khoản đầu tiên nếu có trong danh sách, nếu không có trả về null
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblUsers
                         where p.UserName == userName
                         select p).FirstOrDefault();
            return query;
        }

        public static tblUser GetUserByIdUser(int idUser)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblUsers
                         where p.ID == idUser
                         select p).FirstOrDefault();
            return query;
        }

        public static tblUser InsertUser(tblUser user)
        {
            tblUser result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblUser
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    FullName = user.FullName,
                    IsActive = user.IsActive
                };
                result = db.tblUsers.Add(user);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateUser(tblUser user)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var userResult = db.tblUsers.Find(user.ID); //tìm dòng trong bảng tblUser tương ứng với khóa
                if (userResult != null)
                {
                    userResult.UserName = user.UserName;
                    userResult.Password = user.Password;
                    userResult.FullName = user.FullName;
                    userResult.Email = user.Email;
                    userResult.IsActive = user.IsActive;

                    db.tblUsers.Attach(userResult);
                    db.Entry(userResult).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static int DeleteUser(int idUser)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                //Xóa các Role của User trong bảng tblUserRole trước khi xóa tblUser
                db.tblUserRoles.RemoveRange(db.tblUserRoles.Where(x => x.IDUser == idUser));
                db.SaveChanges();

                var userResult = db.tblUsers.Find(idUser);
                if (userResult != null)
                {
                    db.tblUsers.Remove(userResult);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchUser(string userName, string fullName, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchUser", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", userName);
                cmd.Parameters.AddWithValue("@Fullname", fullName);
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
