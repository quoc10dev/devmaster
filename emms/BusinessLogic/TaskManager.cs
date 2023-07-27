using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class TaskManager
    {
        public static List<tblCongViec> GetAllTask()
        {
            EMMEntities data = new EMMEntities();
            var taskList = data.tblCongViecs.ToList();
            var taskItems = from e in taskList
                            select new tblCongViec
                            {
                                ID = e.ID,
                                TenCongViec = string.Format("{0} - {1}", e.MaCongViec, e.TenCongViec)
                               };
            List<tblCongViec> result = new List<tblCongViec>(taskList.ToList());
            return result;
        }

        public static tblCongViec GetTaskById(int idTask)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblCongViecs
                         where p.ID == idTask
                         select p).FirstOrDefault();
            return query;
        }
    }
}
