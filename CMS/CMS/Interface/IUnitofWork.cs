namespace CMS.Interface
{
    public interface IUnitofWork
    {
        INhanVienRepository NhanVien { get; }
        IPresetTableRepository PresetTable { get; }
        IDVDTRepository DVDT { get; }
        IQuyetDinhRepository QuyetDinh { get; }
        IChungChiRepository ChungChi { get; }
        IKhoaHocRepository KhoaHoc { get; }
        IGiaoVienRepository GiaoVien { get; }
        ICapCCRepository CapCC { get; }
        IUserRepository User { get; }
    }
}
