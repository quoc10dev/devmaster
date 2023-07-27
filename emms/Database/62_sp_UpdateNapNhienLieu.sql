

GO
ALTER PROCEDURE [dbo].[sp_UpdateNapNhienLieu]
(
	@IDTrangThietBi int,
	@NgayNap varchar(10),
	@SoLuong float = null
)
AS
BEGIN
	SET NOCOUNT ON

	If exists(Select 1 from tblNapNhienLieu where NgayNap = @NgayNap And IDTrangThietBi = @IDTrangThietBi)
		Update tblNapNhienLieu Set SoLuong = @SoLuong where NgayNap = @NgayNap And IDTrangThietBi = @IDTrangThietBi
	Else
	Begin
		If @SoLuong > 0
		Begin
			Insert into tblNapNhienLieu(IDTrangThietBi, NgayNap, SoLuong) values(@IDTrangThietBi, @NgayNap, @SoLuong)
		End
	End
END