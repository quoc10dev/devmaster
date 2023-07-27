GO
CREATE TABLE [dbo].[tblDinhMucTheoNgay](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTrangThietBi] [int] NOT NULL,
	[Ngay] [smalldatetime] NOT NULL,
	[DinhMuc] [float] NULL,
 CONSTRAINT [PK_tblDinhMucTheoNgay] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblDinhMucTheoNgay]  WITH CHECK ADD  CONSTRAINT [FK_tblDinhMucTheoNgay_tblTrangThietBi] FOREIGN KEY([IDTrangThietBi])
REFERENCES [dbo].[tblTrangThietBi] ([ID])
GO

ALTER TABLE [dbo].[tblDinhMucTheoNgay] CHECK CONSTRAINT [FK_tblDinhMucTheoNgay_tblTrangThietBi]
GO