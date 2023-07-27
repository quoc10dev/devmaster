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
    public class MaintenanceGroupManager
    {
        public static void LoadMaintenanceGroupForSearch(DropDownList dropdownlist)
        {
            /*
            List<tblNhomHangMucBaoDuong> listItem = GetAllMaintenanceGroup();
            tblNhomHangMucBaoDuong findAllItem = new tblNhomHangMucBaoDuong();
            findAllItem.ID = 0;
            findAllItem.Ten = "--- Select All ---";
            listItem.Insert(0, findAllItem);

            dropdownlist.DataSource = listItem;
            dropdownlist.DataTextField = "Ten";
            dropdownlist.DataValueField = "ID";
            dropdownlist.DataBind();
            */

            DataTable dt = MaintenanceGroupManager.GetMaintenanceParentGroupForAddMaintenance();
            DataRow newRow = dt.NewRow();
            newRow["ID"] = 0;
            newRow["Ten"] = "--- Select All ---";
            dt.Rows.InsertAt(newRow, 0);

            dropdownlist.DataSource = dt;
            dropdownlist.DataTextField = "Ten";
            dropdownlist.DataValueField = "ID";
            dropdownlist.DataBind();

            dropdownlist.SelectedIndex = 0; //mặc định chọn Select All
        }

        public static List<tblNhomHangMucBaoDuong> GetAllMaintenanceGroup()
        {
            EMMEntities data = new EMMEntities();
            var itemList = data.tblNhomHangMucBaoDuongs.OrderBy(x => x.SoThuTuHienThi).ToList();
            var selectItems = from e in itemList
                              select new tblNhomHangMucBaoDuong
                              {
                                  ID = e.ID,
                                  Ten = e.Ten
                              };
            List<tblNhomHangMucBaoDuong> result = new List<tblNhomHangMucBaoDuong>(selectItems.ToList());
            return result;
        }

        //public static List<tblNhomHangMucBaoDuong> GetMaintenanceParentGroup(int idCurrentItem)
        //{
        //    EMMEntities db = new EMMEntities();
        //    var query = (from p in db.tblNhomHangMucBaoDuongs
        //                 where p.IDParent == null && p.ID != idCurrentItem
        //                 select p).ToList();
        //    return query;
        //}

        public static DataTable GetMaintenanceParentGroup(int idCurrentItem)
        {
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetMaintenanceParentGroup", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdMaintenanceGroup", idCurrentItem);
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static DataTable GetMaintenanceParentGroupForAddMaintenance()
        {
            //Lấy danh sách các nhóm hạng mục --> để nhập hạng mục MaintenanceDetail.aspx
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetMaintenanceParentGroupForAddMaintenance", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();
            }
            return dt;
        }

        public static tblNhomHangMucBaoDuong GetMaintenanceGroupById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblNhomHangMucBaoDuongs
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblNhomHangMucBaoDuong GetMaintenanceGroupByIdParent(int idParent)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblNhomHangMucBaoDuongs
                         where p.IDParent == idParent
                         select p).FirstOrDefault();
            return query;
        }

        public static tblNhomHangMucBaoDuong InsertMaintenanceGroup(tblNhomHangMucBaoDuong maintenanceGroup)
        {
            tblNhomHangMucBaoDuong result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblNhomHangMucBaoDuong
                {
                    ID = maintenanceGroup.ID,
                    Ten = maintenanceGroup.Ten,
                    TenEng = maintenanceGroup.TenEng,
                    SoThuTuHienThi = maintenanceGroup.SoThuTuHienThi,
                    GhiChu = maintenanceGroup.GhiChu,
                    IDParent = maintenanceGroup.IDParent
                };
                result = db.tblNhomHangMucBaoDuongs.Add(maintenanceGroup);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateMaintenanceGroup(tblNhomHangMucBaoDuong maintenanceGroup)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblNhomHangMucBaoDuongs.Find(maintenanceGroup.ID);
                if (result != null)
                {
                    result.Ten = maintenanceGroup.Ten;
                    result.TenEng = maintenanceGroup.TenEng;
                    result.SoThuTuHienThi = maintenanceGroup.SoThuTuHienThi;
                    result.IDParent = maintenanceGroup.IDParent;
                    result.GhiChu = maintenanceGroup.GhiChu;

                    db.tblNhomHangMucBaoDuongs.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteMaintenanceGroup(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblNhomHangMucBaoDuongs.Find(id);
                if (result != null)
                {
                    db.tblNhomHangMucBaoDuongs.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static void DeleteMaintenanceGroupByID(int id)
        {
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchMaintenanceGroup", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdMaintenanceGroup", id);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public static DataTable SearchMaintenanceGroupOld(string name, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchMaintenanceGroup", cnn);
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

        public static DataTable SearchMaintenanceGroup(string name, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchMaintenanceGroup", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ten", name);

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
