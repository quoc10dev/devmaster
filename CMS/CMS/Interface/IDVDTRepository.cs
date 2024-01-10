using CMS.Models;

namespace CMS.Interface
{
    public interface IDVDTRepository
    {
        Task<List<DonViDaoTao>> GetAllDVDT();
        Task CreateDVDT(DonViDaoTao dvdt);
        Task EditDVDT(DonViDaoTao dvdt);
    }
}
