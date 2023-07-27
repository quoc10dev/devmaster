-- Bổ sung bảng tblLoaiTrangThietBi
GO
Alter table tblLoaiTrangThietBi add DonViGhiNhanHoatDong varchar(20) null
GO
Alter table tblLoaiTrangThietBi add IDKieuBaoDuong int null
GO
ALTER TABLE [dbo].[tblLoaiTrangThietBi]  WITH CHECK ADD  CONSTRAINT [FK_tblLoaiTrangThietBi_tblKieuBaoDuong] FOREIGN KEY([IDKieuBaoDuong])
REFERENCES [dbo].[tblKieuBaoDuong] ([ID])
GO

-- bổ sung tblTrangThietBi
GO
ALTER TABLE [dbo].[tblTrangThietBi] drop CONSTRAINT FK_tblTrangThietBi_tblKieuBaoDuong
GO
Alter table tblTrangThietBi drop column IDKieuBaoDuong
GO
Alter table tblTrangThietBi drop column DonViTinhDinhMuc

--
GO
alter table tblDinhMucNhienLieu drop column DonViTinh
