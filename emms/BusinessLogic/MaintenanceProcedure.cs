using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BusinessLogic
{
    public class MaintenanceProcedure
    {
        public static void LoadAllMaintenanceProcedure(DropDownList dlQuyTrinhBaoDuong)
        {
            List<tblQuyTrinhBaoDuong> itemList = GetAllMaintenanceProcedure();
            dlQuyTrinhBaoDuong.DataSource = itemList;
            dlQuyTrinhBaoDuong.DataTextField = "MaQuyTrinh";
            dlQuyTrinhBaoDuong.DataValueField = "ID";
            dlQuyTrinhBaoDuong.DataBind();

            dlQuyTrinhBaoDuong.SelectedIndex = 0; //mặc định chọn AGS
        }

        public static List<tblQuyTrinhBaoDuong> GetAllMaintenanceProcedure()
        {
            EMMEntities data = new EMMEntities();
            var quyTrinhList = data.tblQuyTrinhBaoDuongs.ToList();
            var quyTrinhItems = from e in quyTrinhList
                                select new tblQuyTrinhBaoDuong
                                {
                                    ID = e.ID,
                                    MaQuyTrinh = e.MaQuyTrinh,
                                    GiaTri = e.GiaTri
                                };
            List<tblQuyTrinhBaoDuong> result = new List<tblQuyTrinhBaoDuong>(quyTrinhItems.ToList());
            return result;
        }

        public static List<tblQuyTrinhBaoDuong> GetMaintenanceProcedureByIDLoaiBaoDuong(int idLoaiBaoDuong)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblQuyTrinhBaoDuongs join e in db.tblCapBaoDuongs on p.IDCapBaoDuong equals e.ID
                         where e.IDLoaiBaoDuong == idLoaiBaoDuong
                         select p).ToList<tblQuyTrinhBaoDuong>();
            return query;
        }
    }
}
