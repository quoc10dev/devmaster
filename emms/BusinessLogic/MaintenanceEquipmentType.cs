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
    public class MaintenanceEquipmentType
    {
        public static tblHangMucLoaiTrangThietBi GetMaintenanceEquipmentTypeById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblHangMucLoaiTrangThietBis
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblHangMucLoaiTrangThietBi InsertMaintenanceEquipmentType(tblHangMucLoaiTrangThietBi maintenanceEquipmentType)
        {
            tblHangMucLoaiTrangThietBi result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblHangMucLoaiTrangThietBi
                {
                    ID = maintenanceEquipmentType.ID,
                    IDHangMuc = maintenanceEquipmentType.IDHangMuc,
                    IDLoaiTrangThietBi = maintenanceEquipmentType.IDLoaiTrangThietBi
                };
                result = db.tblHangMucLoaiTrangThietBis.Add(maintenanceEquipmentType);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateMaintenanceEquipmentType(tblHangMucLoaiTrangThietBi maintenanceEquipmentType)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblHangMucLoaiTrangThietBis.Find(maintenanceEquipmentType.ID);
                if (result != null)
                {
                    result.IDHangMuc = maintenanceEquipmentType.IDHangMuc;
                    result.IDLoaiTrangThietBi = maintenanceEquipmentType.IDLoaiTrangThietBi;

                    db.tblHangMucLoaiTrangThietBis.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteMaintenanceEquipmentType(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblHangMucLoaiTrangThietBis.Find(id);
                if (result != null)
                {
                    db.tblHangMucLoaiTrangThietBis.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static int DeleteMaintenanceEquipmentType(int idMaintenance, int idEquipmentType)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                List<tblHangMucLoaiTrangThietBi> obj = db.tblHangMucLoaiTrangThietBis.Where(w => w.IDHangMuc == idMaintenance &&
                                                        w.IDLoaiTrangThietBi == idEquipmentType).ToList<tblHangMucLoaiTrangThietBi>();

                //Xóa các công việc của các hạng mục
                foreach (tblHangMucLoaiTrangThietBi item in obj)
                {
                    var taskList = db.tblCapBaoDuongCongViecs.Where(w => w.IDHangMucLoaiTrangThietBi == item.ID).ToList<tblCapBaoDuongCongViec>();
                    db.tblCapBaoDuongCongViecs.RemoveRange(taskList);
                    db.SaveChanges();
                }

                db.tblHangMucLoaiTrangThietBis.RemoveRange(obj);
                record = db.SaveChanges();
            }
            return record;
        }
        
        public static DataTable SearchMaintenanceEquipmentType(int idNhomTrangThietBi,  int idLoaiTrangThietBi,
                                               int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_MauInTheBaoDuong", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idNhomTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDNhomTrangThietBi", idNhomTrangThietBi);

                if (idLoaiTrangThietBi > 0)
                    cmd.Parameters.AddWithValue("@IDLoaiTrangThietBi", idLoaiTrangThietBi);

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

        public static bool CheckExistsEquipmentType(int idLoaiTrangThietBi)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblHangMucLoaiTrangThietBis
                         where p.IDLoaiTrangThietBi == idLoaiTrangThietBi
                         select p).FirstOrDefault();
            return (query != null) ? true : false;
        }

        public static tblHangMucLoaiTrangThietBi GetMaintenanceEquipmentType(int idMaintenance, int idEquipmentType)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblHangMucLoaiTrangThietBis
                         where p.IDHangMuc == idMaintenance && p.IDLoaiTrangThietBi == idEquipmentType
                         select p).FirstOrDefault();
            return query;
        }

        public static List<tblHangMucLoaiTrangThietBi> GetMaintenanceEquipmentType(int idEquipmentType)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblHangMucLoaiTrangThietBis
                         where p.IDLoaiTrangThietBi == idEquipmentType
                         select p).ToList< tblHangMucLoaiTrangThietBi>();
            return query;
        }
    }
}
