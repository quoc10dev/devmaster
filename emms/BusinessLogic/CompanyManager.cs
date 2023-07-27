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
    public class CompanyManager
    {
        public static void LoadCompanyForSearch(DropDownList dlCompany)
        {
            List<tblCongTy> companyList = CompanyManager.GetAllCompany();
            tblCongTy findAllItem = new tblCongTy();
            findAllItem.ID = 0;
            findAllItem.TenVietTat = "--- Select All ---";
            companyList.Insert(0, findAllItem);

            dlCompany.DataSource = companyList;
            dlCompany.DataTextField = "TenVietTat";
            dlCompany.DataValueField = "ID";
            dlCompany.DataBind();

            dlCompany.SelectedIndex = 1; //mặc định chọn AGS
        }

        public static void LoadCompanyForSearchNotAll(DropDownList dlCompany)
        {
            List<tblCongTy> companyList = CompanyManager.GetAllCompany();
            dlCompany.DataSource = companyList;
            dlCompany.DataTextField = "TenVietTat";
            dlCompany.DataValueField = "ID";
            dlCompany.DataBind();

            dlCompany.SelectedIndex = 0; //mặc định chọn AGS
        }

        public static List<tblCongTy> GetAllCompany()
        {
            EMMEntities data = new EMMEntities();
            var companyList = data.tblCongTies.ToList();
            var companyItems = from e in companyList
                               select new tblCongTy
                             {
                                 ID = e.ID,
                                 TenVietTat = e.TenVietTat,
                                 TenDayDu = e.TenDayDu
                             };
            List<tblCongTy> result = new List<tblCongTy>(companyItems.ToList());
            return result;
        }

        public static tblCongTy GetCompanyById(int idCompany)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblCongTies
                         where p.ID == idCompany
                         select p).FirstOrDefault();
            return query;
        }

        public static tblCongTy InsertCompany(tblCongTy company)
        {
            tblCongTy result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblCongTy
                {
                    TenDayDu = company.TenDayDu,
                    TenVietTat = company.TenVietTat,
                    DiaChi = company.DiaChi,
                    DienThoai = company.DienThoai,
                    Email = company.Email
                };
                result = db.tblCongTies.Add(company);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateCompany(tblCongTy company)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblCongTies.Find(company.ID); //tìm dòng trong bảng tblCongTy tương ứng với khóa
                if (result != null)
                {
                    result.TenDayDu = company.TenDayDu;
                    result.TenVietTat = company.TenVietTat;
                    result.DiaChi = company.DiaChi;
                    result.DienThoai = company.DienThoai;
                    result.Email = company.Email;

                    db.tblCongTies.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static int DeleteCompany(int idCompany)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblCongTies.Find(idCompany);
                if (result != null)
                {
                    db.tblCongTies.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchCompany(string shortName, string fullName, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchCompany", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TenVietTat", shortName);
                cmd.Parameters.AddWithValue("@TenDayDu", fullName);
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

        public static void InsertDataFromSkySoft(int idTrangThietBi, string bienSo, DateTime ngayHoatDong, double soPhut, double soKm)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SkySoft_GetData", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTrangThietBi", idTrangThietBi);
                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@NgayHoatDong", ngayHoatDong);
                cmd.Parameters.AddWithValue("@SoPhut", soPhut);
                cmd.Parameters.AddWithValue("@SoKm", soKm);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
}
