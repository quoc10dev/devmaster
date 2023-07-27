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

namespace BusinessLogic.Security
{
    public class RoleManager
    {
        public static tblRole GetRoleByIdRole(int idRole)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblRoles
                         where p.ID == idRole
                         select p).FirstOrDefault();
            return query;
        }

        public static tblRole GetRoleByRoleName(string roleName)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblRoles
                         where p.RoleName == roleName
                         select p).FirstOrDefault();
            return query;
        }

        public static tblRole InsertRole(tblRole role)
        {
            tblRole result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblRole
                {
                    RoleName = role.RoleName,
                    Description = role.Description,
                    Status = role.Status
                };
                result = db.tblRoles.Add(role);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateRole(tblRole role)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblRoles.Find(role.ID); //tìm dòng trong bảng tblRole tương ứng với khóa
                if (result != null)
                {
                    result.RoleName = role.RoleName;
                    result.Description = role.Description;
                    result.Status = role.Status;

                    db.tblRoles.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static int DeleteRole(int idRole)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                //Xóa các right của role trong bảng tblRightsInRoles trước khi xóa tblRoles
                db.tblRightsInRoles.RemoveRange(db.tblRightsInRoles.Where(x => x.IdRole == idRole));
                db.SaveChanges();

                var result = db.tblRoles.Find(idRole);
                if (result != null)
                {
                    db.tblRoles.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchRole(string roleName, string description, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchRole", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RoleName", roleName);
                cmd.Parameters.AddWithValue("@Description", description);
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

        public static List<tblRole> GetAllRole(bool isActive)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblRoles
                         where p.Status == isActive
                         select p).ToList<tblRole>();
            return query;
        }

        public static tblUserRole InsertUserRole(tblUserRole userRole)
        {
            tblUserRole result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblUserRole
                {
                    IDRole = userRole.IDRole,
                    IDUser = userRole.IDUser
                };
                result = db.tblUserRoles.Add(userRole);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateUserRole(tblUserRole userRole)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblUserRoles.Find(userRole.ID); 
                if (result != null)
                {
                    result.IDRole = userRole.IDRole;
                    result.IDUser = userRole.IDUser;
                   
                    db.tblUserRoles.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static List<tblUserRole> GetUserRoleByIdUser(int idUser)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblUserRoles
                         where p.IDUser == idUser
                         select p).ToList<tblUserRole>();
            return query;
        }

        public static List<tblUserRole> GetUserRoleByIdRole(int idRole)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblUserRoles
                         where p.IDRole == idRole
                         select p).ToList<tblUserRole>();
            return query;
        }

        public static List<tblUserRole> GetUserRoleByIdUserAndIdRole(int idUser, int idRole)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblUserRoles
                         where p.IDUser == idUser && p.IDRole == idRole
                         select p).ToList<tblUserRole>();
            return query;
        }

        public static int DeleteRoleRoleByIdUserAndIdRole(int idUser, int idRole)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                db.tblUserRoles.RemoveRange(db.tblUserRoles.Where(x => x.IDUser == idUser && x.IDRole == idRole));
                records = db.SaveChanges();
            }
            return records;
        }
    }
}
