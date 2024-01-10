using CMS.Models;

namespace CMS.ViewModels
{
    public class CCNhanVien
    {
        public int IdNhomCC { get; set; }
        public string TenNhomCC { get; set; }
        public List<GroupChungChi> ChungChis { get; set; }
    }
    public class GroupChungChi
    {
        public string TenCC { get; set; }
        public List<CapChungChi> CapCCs { get; set; }
    }
}
