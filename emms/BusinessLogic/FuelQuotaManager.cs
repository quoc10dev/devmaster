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
    public class FuelQuotaManager
    {
        public static tblDinhMucNhienLieu GetFuelQuotaById(int id)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblDinhMucNhienLieux
                         where p.ID == id
                         select p).FirstOrDefault();
            return query;
        }

        public static tblDinhMucNhienLieu GetFuelQuotaByIdEquipment(int idEquipment)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblDinhMucNhienLieux
                         where p.IDTrangThietBi == idEquipment
                         select p).FirstOrDefault();
            return query;
        }

        public static tblDinhMucNhienLieu InsertFuelQuota(tblDinhMucNhienLieu fuelQuota)
        {
            tblDinhMucNhienLieu result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblDinhMucNhienLieu
                {
                    ID = fuelQuota.ID,
                    IDTrangThietBi = fuelQuota.IDTrangThietBi,
                    SoLuongDinhMuc = fuelQuota.SoLuongDinhMuc,
                    NgayBatDauApDung = fuelQuota.NgayBatDauApDung
                };
                result = db.tblDinhMucNhienLieux.Add(fuelQuota);
                db.SaveChanges();
                return result;
            };
        }

        public static int UpdateFuelQuota(tblDinhMucNhienLieu fuelQuota)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblDinhMucNhienLieux.Find(fuelQuota.ID);
                if (result != null)
                {
                    result.IDTrangThietBi = fuelQuota.IDTrangThietBi;
                    result.SoLuongDinhMuc = fuelQuota.SoLuongDinhMuc;
                    result.NgayBatDauApDung = fuelQuota.NgayBatDauApDung;

                    db.tblDinhMucNhienLieux.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges();
                }
            };
            return records;
        }

        public static int DeleteFuelQuota(int id)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblDinhMucNhienLieux.Find(id);
                if (result != null)
                {
                    db.tblDinhMucNhienLieux.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }

        public static DataTable SearchEquipment(int idCongTy, int idNhomTrangThietBi, int idLoaiTrangThietBi, string bienSo, string ten,
                                    int pageIndex, int pageSize, string sortExpression, string softOrder, out int totalRecord)
        {
            totalRecord = 0;
            DataTable dt = new DataTable();
            string connectionString = SecurityHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Search_DinhMucNhienLieu", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

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
    }
}
