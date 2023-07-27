


CREATE TABLE [dbo].[tblSkySoft](
	[ID] [bigint] NOT NULL,
	[IdTrangThietBi] [int] NOT NULL,
	[BienSo] [nvarchar](50) NOT NULL,
	[NgayHoatDong] [smalldatetime] NOT NULL,
	[SoPhut] [float] NOT NULL,
	[SoKm] [float] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tblSkySoft] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


