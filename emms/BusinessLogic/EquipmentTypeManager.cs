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
    public class DonViGhiNhanHoatDong
    {
        public const string Km = "km";
        public const string Gio = "gio";
    }
    public class EquipmentTypeManager
    {
        public static void LoadEquipmentTypeForSearch(DropDownList dlEquipmentType)
        {
            dlEquipmentType.Items.Clear();
            List<tblLoaiTrangThietBi> equipTypeList = EquipmentTypeManager.GetAllEquipmentType();
            tblLoaiTrangThietBi findAllItem = new tblLoaiTrangThietBi();
            findAllItem.ID = 0;
            findAllItem.Ten = "--- Select All ---";
            equipTypeList.Insert(0, findAllItem);

            dlEquipmentType.DataSource = equipTypeList;
            dlEquipmentType.DataTextField = "Ten";
            dlEquipmentType.DataValueField = "ID";
            dlEquipmentType.DataBind();
        }

        public static void LoadEquipmentTypeForSearch(DropDownList dlEquipmentType, int idEquipmentGroup)
        {
            dlEquipmentType.Items.Clear();
            List<tblLoaiTrangThietBi> equipTypeList = EquipmentTypeManager.LoadEquipmentTypeByEquipmentGroup(idEquipmentGroup);
            tblLoaiTrangThietBi findAllItem = new tblLoaiTrangThietBi();
            findAllItem.ID = 0;
            findAllItem.Ten = "--- Select All ---";
            equipTypeList.Insert(0, findAllItem);

            dlEquipmentType.DataSource = equipTypeList;
            dlEquipmentType.DataTextField = "Ten";
            dlEquipmentType.DataValueField = "ID";
            dlEquipmentType.DataBind();
        }

        public static void LoadEquipmentType(DropDownList dlEquipmentType, int idEquipmentGroup)
        {
            dlEquipmentType.Items.Clear();
            List<tblLoaiTrangThietBi> equipTypeList = EquipmentTypeManager.LoadEquipmentTypeByEquipmentGroup(idEquipmentGroup);
            tblLoaiTrangThietBi findAllItem = new tblLoaiTrangThietBi();
            findAllItem.ID = 0;
            findAllItem.Ten = "--- Chọn loại xe ---";
            equipTypeList.Insert(0, findAllItem);

            dlEquipmentType.DataSource = equipTypeList;
            dlEquipmentType.DataTextField = "Ten";
            dlEquipmentType.DataValueField = "ID";
            dlEquipmentType.DataBind();
        }

        public static List<tblLoaiTrangThietBi> LoadEquipmentTypeByEquipmentGroup(int idEquipmentGroup)
        {
            EMMEntities data = new EMMEntities();
            var equipTypeList = data.tblLoaiTrangThietBis.Where(x => x.IDNhomTrangThietBi == idEquipmentGroup).ToList();
            return equipTypeList;
        }

        public static List<tblLoaiTrangThietBi> GetAllEquipmentType()
        {
            EMMEntities data = new EMMEntities();
            var equipTypeList = data.tblLoaiTrangThietBis.OrderBy(x => x.SoThuTuHienThi).ToList();
            var equipItems = from e in equipTypeList
                             select new tblLoaiTrangThietBi
                             {
                                 ID = e.ID,
                                 Ten = e.Ten,
                                 TenEng = e.TenEng
                             };
            List<tblLoaiTrangThietBi> ls = new List<tblLoaiTrangThietBi>(equipItems.ToList());
            return ls;
        }

        public static tblLoaiTrangThietBi GetEquipmentTypeById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblLoaiTrangThietBis
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static List<tblLoaiTrangThietBi> GetEquipmentTypeByIDEquipmentGroup(int idNhomTrangThietBi)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblLoaiTrangThietBis
                         where p.IDNhomTrangThietBi == idNhomTrangThietBi
                         select p).ToList();
            return query;
        }

        public static tblLoaiTrangThietBi InsertEquipmentType(tblLoaiTrangThietBi equipmentType)
        {
            tblLoaiTrangThietBi result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblLoaiTrangThietBi
                {
                    ID = equipmentType.ID,
                    Ten = equipmentType.Ten,
                    TenEng = equipmentType.TenEng,
                    SoThuTuHienThi = equipmentType.SoThuTuHienThi,
                    GhiChu = equipmentType.GhiChu,
                    DonViGhiNhanHoatDong = equipmentType.DonViGhiNhanHoatDong,
                    IDLoaiBaoDuong = equipmentType.IDLoaiBaoDuong,
                    IDNhomTrangThietBi = equipmentType.IDNhomTrangThietBi
                };
                result = db.tblLoaiTrangThietBis.Add(equipmentType);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateEquipmentType(tblLoaiTrangThietBi equipmentType)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblLoaiTrangThietBis.Find(equipmentType.ID);
                if (result != null)
                {
                    result.Ten = equipmentType.Ten;
                    result.TenEng = equipmentType.TenEng;
                    result.SoThuTuHienThi = equipmentType.SoThuTuHienThi;
                    result.GhiChu = equipmentType.GhiChu;
                    result.DonViGhiNhanHoatDong = equipmentType.DonViGhiNhanHoatDong;
                    result.IDLoaiBaoDuong = equipmentType.IDLoaiBaoDuong;
                    result.IDNhomTrangThietBi = equipmentType.IDNhomTrangThietBi;

                    db.tblLoaiTrangThietBis.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteEquipmentType(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblLoaiTrangThietBis.Find(id);
                if (result != null)
                {
                    db.tblLoaiTrangThietBis.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchEquipmentType(int idNhomTrangThietBi, string name, int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_LoaiTrangThietBi", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

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
