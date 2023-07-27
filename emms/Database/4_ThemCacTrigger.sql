/*
DROP TRIGGER tg_tblNapNhienLieu_AfterInsert
GO
DROP TRIGGER tg_tblNapNhienLieu_AfterUpdate
GO
DROP TRIGGER tg_tblNapNhienLieu_AfterDelete
GO
DROP TRIGGER tg_tblNhatKyHoatDong_AfterInsert
GO
DROP TRIGGER tg_tblNhatKyHoatDong_AfterUpdate
GO
DROP TRIGGER tg_tblNhatKyHoatDong_AfterDelete
*/

-- Các trigger cho bảng tblNapNhienLieu
GO
CREATE TRIGGER tg_tblNapNhienLieu_AfterInsert ON tblNapNhienLieu AFTER INSERT 
AS
BEGIN
	DECLARE @IDTrangThietBi INT, @Ngay smalldatetime, @NgayTruoc smalldatetime

	SELECT @IDTrangThietBi = ins.IDTrangThietBi FROM INSERTED ins;
	SELECT @Ngay = ins.NgayNap FROM INSERTED ins;
	
	SET @NgayTruoc = DateAdd(day, -1, @Ngay)
    exec sp_TinhDinhMucTheoNgay @NgayTruoc, @IDTrangThietBi

	exec sp_TinhDinhMucTheoNgay @Ngay, @IDTrangThietBi
END

GO
CREATE TRIGGER tg_tblNapNhienLieu_AfterUpdate ON tblNapNhienLieu AFTER UPDATE 
AS
BEGIN
	DECLARE @IDTrangThietBi INT, @Ngay smalldatetime, @NgayTruoc smalldatetime

	SELECT @IDTrangThietBi = ins.IDTrangThietBi FROM INSERTED ins;
	SELECT @Ngay = ins.NgayNap FROM INSERTED ins;
	SET @NgayTruoc = DateAdd(day, -1, @Ngay)

    exec sp_TinhDinhMucTheoNgay @NgayTruoc, @IDTrangThietBi

	exec sp_TinhDinhMucTheoNgay @Ngay, @IDTrangThietBi
END

GO
CREATE TRIGGER tg_tblNapNhienLieu_AfterDelete ON tblNapNhienLieu AFTER DELETE 
AS
BEGIN
	Declare @IDTrangThietBi int, @Ngay smalldatetime, @NgayTruoc smalldatetime

	SELECT @IDTrangThietBi = del.IDTrangThietBi FROM DELETED del;
	SELECT @Ngay = del.NgayNap FROM DELETED del;
	SET @NgayTruoc = DateAdd(day, -1, @Ngay)

    Delete tblDinhMucTheoNgay where IDTrangThietBi = @IDTrangThietBi And (Ngay = @Ngay or Ngay = @NgayTruoc)
END

-- Các trigger cho bảng tblNhatKyHoatDong 
GO
CREATE TRIGGER tg_tblNhatKyHoatDong_AfterInsert ON tblNhatKyHoatDong AFTER INSERT 
AS
BEGIN
	DECLARE @IDTrangThietBi INT, @Ngay smalldatetime, @NgayTruoc smalldatetime

	SELECT @IDTrangThietBi = ins.IDTrangThietBi FROM INSERTED ins;
	SELECT @Ngay = ins.NgayHoatDong FROM INSERTED ins;
	SET @NgayTruoc = DateAdd(day, -1, @Ngay)

    exec sp_TinhDinhMucTheoNgay @NgayTruoc, @IDTrangThietBi

	exec sp_TinhDinhMucTheoNgay @Ngay, @IDTrangThietBi
END

GO
CREATE TRIGGER tg_tblNhatKyHoatDong_AfterUpdate ON tblNhatKyHoatDong AFTER UPDATE
AS
BEGIN
	DECLARE @IDTrangThietBi INT, @Ngay smalldatetime, @NgayTruoc smalldatetime

	SELECT @IDTrangThietBi = ins.IDTrangThietBi FROM INSERTED ins;
	SELECT @Ngay = ins.NgayHoatDong FROM INSERTED ins;
	SET @NgayTruoc = DateAdd(day, -1, @Ngay)

    exec sp_TinhDinhMucTheoNgay @NgayTruoc, @IDTrangThietBi

	exec sp_TinhDinhMucTheoNgay @Ngay, @IDTrangThietBi
END

GO
CREATE TRIGGER tg_tblNhatKyHoatDong_AfterDelete ON tblNhatKyHoatDong AFTER DELETE 
AS
BEGIN
	Declare @IDTrangThietBi int, @Ngay smalldatetime, @NgayTruoc smalldatetime

	SELECT @IDTrangThietBi = del.IDTrangThietBi FROM DELETED del;
	SELECT @Ngay = del.NgayHoatDong FROM DELETED del;
	SET @NgayTruoc = DateAdd(day, -1, @Ngay)

    Delete tblDinhMucTheoNgay where IDTrangThietBi = @IDTrangThietBi And (Ngay = @Ngay or Ngay = @NgayTruoc)
END


