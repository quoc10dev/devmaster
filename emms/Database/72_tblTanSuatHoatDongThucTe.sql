
CREATE TABLE [dbo].[tblTanSuatHoatDongThucTe](
	[IDTrangThietBi] [int] NOT NULL,
	[NgayHoatDong] [smalldatetime] NOT NULL,
	[SoLuong] [float] NOT NULL,
 CONSTRAINT [PK_tblTanSuatHoatDongThucTe] PRIMARY KEY CLUSTERED 
(
	[IDTrangThietBi] ASC,
	[NgayHoatDong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


