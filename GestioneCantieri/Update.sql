-- Creazione Tabella DDT Fornitori
CREATE TABLE [dbo].[TblDDTFornitori](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdFornitore] [int] NOT NULL,
	[Data] [datetime] NOT NULL,
	[Protocollo] [bigint] NOT NULL,
	[NumeroDDT] [nvarchar](50) NOT NULL,
	[Articolo] [nvarchar](50) NOT NULL,
	[DescrizioneFornitore] [nvarchar](255) NULL,
	[DescrizioneMau] [nvarchar](255) NULL,
	[Qta] [int] NOT NULL,
	[Valore] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_TblDDTFornitori] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TblDDTFornitori]  WITH CHECK ADD  CONSTRAINT [FK_TblDDTFornitori_TblForitori] FOREIGN KEY([IdFornitore])
REFERENCES [dbo].[TblForitori] ([IdFornitori])
GO

ALTER TABLE [dbo].[TblDDTFornitori] CHECK CONSTRAINT [FK_TblDDTFornitori_TblForitori]
GO