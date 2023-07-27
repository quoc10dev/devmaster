

GO
CREATE TABLE [dbo].[tblPhieuSuaChua](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaPhieu] [varchar](50) NULL,
	[IDTrangThietBi] [int] NOT NULL,
	[NguoiSuaChua] [nvarchar](200) NOT NULL,
	[NgaySuaChua] [smalldatetime] NOT NULL,
	[NgayNhap] [smalldatetime] NULL,
	[NguoiNhap] [nvarchar](100) NULL,
	[NgaySua] [smalldatetime] NULL,
	[NguoiSua] [nvarchar](100) NULL,
	[SoGioHoacKm] [float] NULL,
	[NgayVaoXuong] [smalldatetime] NULL,
	[NgayXuatXuong] [smalldatetime] NULL,
	[PathOfFileUpload] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblPhieuSuaChua] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tblPhieuSuaChua]  WITH CHECK ADD  CONSTRAINT [FK_tblPhieuSuaChua_tblTrangThietBi] FOREIGN KEY([IDTrangThietBi])
REFERENCES [dbo].[tblTrangThietBi] ([ID])
GO

ALTER TABLE [dbo].[tblPhieuSuaChua] CHECK CONSTRAINT [FK_tblPhieuSuaChua_tblTrangThietBi]
GO

-- Cấp bảo dưỡng
alter table tblCapBaoDuong add LevelInPrint int null

