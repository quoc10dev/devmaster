using CMS.DatabaseContext;
using CMS.Interface;
using Microsoft.AspNetCore.Identity;

namespace CMS.Repository
{
    public class UnitofWork: IUnitofWork
    {
        private AppDbContext _appDbContext;
        private AuthDbContext _authDbContext;

        public UnitofWork(AppDbContext appDbContext, AuthDbContext authDbContext, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _appDbContext = appDbContext;
            _authDbContext = authDbContext;
            this.NhanVien = new NhanVienRepository(appDbContext);
            this.PresetTable = new PresetTableRepository(appDbContext);
            this.DVDT = new DVDTRepository(appDbContext);
            this.QuyetDinh = new QuyetDinhRepository(appDbContext);
            this.ChungChi = new ChungChiRepository(appDbContext);
            this.KhoaHoc = new KhoaHocRepository(appDbContext);
            this.GiaoVien = new GiaoVienRepository(appDbContext);
            this.CapCC = new CapCCRepository(appDbContext);
            this.User = new UserRepository(authDbContext,signInManager,userManager);
        }

        public INhanVienRepository NhanVien { get; private set; }
        public IPresetTableRepository PresetTable { get; private set; }
        public IDVDTRepository DVDT { get; private set; }
        public IQuyetDinhRepository QuyetDinh { get; private set; }
        public IChungChiRepository ChungChi { get; private set; }
        public IKhoaHocRepository KhoaHoc { get; private set; }
        public IGiaoVienRepository GiaoVien { get; private set; }
        public ICapCCRepository CapCC { get; private set; }
        public IUserRepository User { get; private set; }
    }
}
