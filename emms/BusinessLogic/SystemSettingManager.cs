using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SystemSettingGroup
    {
        public const string Equipment_Management = "equipment_list_management";
        public const string Maintenance_Management = "maintenance_management";
    }

    public class SystemSettingParameter
    {
        public const string Equipment_Management_SoNgayCanhBaoDangKiem = "equipment_management_SoNgayCanhBaoDangKiem";
        public const string Maintenance_Management_SoNgayCanhBaoBaoDuong = "maintenance_management_SoNgayCanhBaoBaoDuong";
        public const string Maintenance_Management_SoNgayTinhTanSuat = "maintenance_management_SoNgayNhapGanNhatTinhTanSuat";
    }

    public class SystemSettingManager
    {
        public static List<tblSystemSetting> GetAll()
        {
            //Lấy tất cả các chức năng của người dùng để bind menu
            EMMEntities data = new EMMEntities();
            var settingList = data.tblSystemSettings.ToList();
            var settingItems = from e in settingList
                               select new tblSystemSetting
                               {
                                   Name = e.Name,
                                   Value = e.Value,
                                   SettingGroup = e.SettingGroup
                               };
            List<tblSystemSetting> result = new List<tblSystemSetting>(settingItems.ToList());
            return result;
        }

        public static List<tblSystemSetting> GetItemByGroup(string settingGroup)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblSystemSettings
                         where p.SettingGroup == settingGroup
                         select p).ToList();
            return query;
        }

        public static tblSystemSetting GetItemByName(string name)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblSystemSettings
                         where p.Name == name
                         select p).FirstOrDefault();
            return query;
        }

        public static tblSystemSetting InsertItem(tblSystemSetting item)
        {
            tblSystemSetting result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblSystemSetting
                {
                    Name = item.Name,
                    Value = item.Value,
                    SettingGroup = item.SettingGroup
                };
                result = db.tblSystemSettings.Add(item);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateItem(tblSystemSetting item)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblSystemSettings.Find(item.Name); //tìm dòng trong bảng tblSystemSettings tương ứng với khóa
                if (result != null)
                {
                    result.Name = item.Name;
                    result.Value = item.Value;
                    result.SettingGroup = item.SettingGroup;

                    db.tblSystemSettings.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static int DeleteItem(string name)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblSystemSettings.Find(name);
                if (result != null)
                {
                    db.tblSystemSettings.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }
    }
}
