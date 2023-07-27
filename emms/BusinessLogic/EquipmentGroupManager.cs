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
    public class EquipmentGroupManager
    {
        public static List<tblNhomTrangThietBi> GetAllEquipmentGroup()
        {
            EMMEntities data = new EMMEntities();
            var equipGroupList = data.tblNhomTrangThietBis.ToList();
            var equipItems = from e in equipGroupList
                             select new tblNhomTrangThietBi
                             {
                                 ID = e.ID,
                                 TenNhom = e.TenNhom,
                                 TenNhomEng = e.TenNhomEng
                             };
            List<tblNhomTrangThietBi> ls = new List<tblNhomTrangThietBi>(equipItems.ToList());
            return ls;
        }

        public static void LoadEquipmentGroupForSearch(DropDownList dropdownList)
        {
            List<tblNhomTrangThietBi> equipGroupList = EquipmentGroupManager.GetAllEquipmentGroup();
            tblNhomTrangThietBi findAllItem = new tblNhomTrangThietBi();
            findAllItem.ID = 0;
            findAllItem.TenNhom = "--- Select All ---";
            equipGroupList.Insert(0, findAllItem);

            dropdownList.DataSource = equipGroupList;
            dropdownList.DataTextField = "TenNhom";
            dropdownList.DataValueField = "ID";
            dropdownList.DataBind();
        }

        public static tblNhomTrangThietBi GetEquipmentGroupById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblNhomTrangThietBis
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblNhomTrangThietBi InsertEquipmentGroup(tblNhomTrangThietBi equipmentGroup)
        {
            tblNhomTrangThietBi result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblNhomTrangThietBi
                {
                    ID = equipmentGroup.ID,
                    TenNhom = equipmentGroup.TenNhom,
                    TenNhomEng = equipmentGroup.TenNhomEng,
                    GhiChu = equipmentGroup.GhiChu,
                };
                result = db.tblNhomTrangThietBis.Add(equipmentGroup);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateEquipmentGroup(tblNhomTrangThietBi equipmentGroup)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblNhomTrangThietBis.Find(equipmentGroup.ID);
                if (result != null)
                {
                    result.TenNhom = equipmentGroup.TenNhom;
                    result.TenNhomEng = equipmentGroup.TenNhomEng;
                    result.GhiChu = equipmentGroup.GhiChu;

                    db.tblNhomTrangThietBis.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteEquipmentGroup(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblNhomTrangThietBis.Find(id);
                if (result != null)
                {
                    db.tblNhomTrangThietBis.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchEquipmentGroup(string name, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_NhomTrangThietBi", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TenNhom", name);
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
