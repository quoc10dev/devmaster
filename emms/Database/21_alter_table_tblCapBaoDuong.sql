
-- update tblCapBaoDuong
GO
ALTER TABLE [dbo].[tblCapBaoDuong] drop CONSTRAINT FK_tblCapBaoDuong_tblKieuBaoDuong

GO
DROP TABLE tblCapBaoDuong

CREATE TABLE [dbo].[tblCapBaoDuong](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](100) NULL,
	[GhiChu] [nvarchar](max) NULL,
	[IDLoaiBaoDuong] [int] NOT NULL,
	[SoLuongMoc] [float] NOT NULL,
 CONSTRAINT [PK_tblCapBaoDuong] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblCapBaoDuong]  WITH CHECK ADD  CONSTRAINT [FK_tblCapBaoDuong_tblLoaiBaoDuong] FOREIGN KEY([IDLoaiBaoDuong])
REFERENCES [dbo].[tblLoaiBaoDuong] ([IDLoaiBaoDuong])
GO

ALTER TABLE [dbo].[tblCapBaoDuong] CHECK CONSTRAINT [FK_tblCapBaoDuong_tblLoaiBaoDuong]
GO


--- update tblTrangThietBi
GO
alter table tblTrangThietBi add IDLoaiBaoDuong int null 

GO
ALTER TABLE tblTrangThietBi ADD CONSTRAINT FK_tblLoaiBaoDuong_tblTrangThietBi
FOREIGN KEY (IDLoaiBaoDuong) REFERENCES tblLoaiBaoDuong(IDLoaiBaoDuong);
