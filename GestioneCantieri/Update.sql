-- Aggiunta campo idFornitore nella tabella del DDT
ALTER TABLE TblDDTMef ADD IdFornitore int NULL

-- Creazione tabella temporanea per importazione DBF
CREATE TABLE [dbo].[TblDDTMefTemp](
	[IdDDTMef] [int] IDENTITY(1,1) NOT NULL,
	[Anno] [int] NULL,
	[Data] [datetime] NULL,
	[N_DDT] [int] NULL,
	[CodArt] [nvarchar](50) NULL,
	[DescriCodArt] [nvarchar](50) NULL,
	[Qta] [int] NULL,
	[Importo] [money] NULL,
	[Acquirente] [nvarchar](50) NULL,
	[PrezzoUnitario] [money] NULL,
	[AnnoN_DDT] [int] NULL,
 CONSTRAINT [PK_TblDDTMefTemp] PRIMARY KEY CLUSTERED 
(
	[IdDDTMef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


