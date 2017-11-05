USE [GestioneCantieri]
GO
/****** Object:  User [IIS APPPOOL\GestioneCantieri]    Script Date: 05/11/2017 11:07:11 ******/
CREATE USER [IIS APPPOOL\GestioneCantieri] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\GestioneCantieri]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\GestioneCantieri]
GO
/****** Object:  Table [dbo].[MAMG0]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MAMG0](
	[AA_COD] [nvarchar](7) NULL,
	[AA_SIGF] [nvarchar](3) NULL,
	[AA_CODF] [nvarchar](16) NULL,
	[AA_DES] [nvarchar](35) NULL,
	[AA_UM] [nvarchar](2) NULL,
	[AA_PZ] [float] NULL,
	[AA_IVA] [smallint] NULL,
	[AA_VAL] [nvarchar](3) NULL,
	[AA_PRZ] [float] NULL,
	[AA_CODFSS] [nvarchar](19) NULL,
	[AA_GRUPPO] [nvarchar](2) NULL,
	[AA_SCONTO1] [float] NULL,
	[AA_SCONTO2] [float] NULL,
	[AA_SCONTO3] [float] NULL,
	[AA_CFZMIN] [float] NULL,
	[AA_MGZ] [nvarchar](9) NULL,
	[AA_CUB] [float] NULL,
	[AA_PRZ1] [float] NULL,
	[AA_DATA1] [datetime] NULL,
	[AA_EAN] [nvarchar](21) NULL,
	[WOMAME] [nvarchar](3) NULL,
	[WOFOME] [nvarchar](3) NULL,
	[WOPDME] [nvarchar](16) NULL,
	[WOFMSC] [nvarchar](18) NULL,
	[WOFMST] [nvarchar](18) NULL,
	[RAME] [float] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblCantieri]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCantieri](
	[IdCantieri] [int] IDENTITY(1,1) NOT NULL,
	[IdTblClienti] [int] NULL,
	[Data] [datetime] NULL,
	[CodCant] [nvarchar](10) NULL,
	[DescriCodCAnt] [nvarchar](255) NULL,
	[Indirizzo] [nvarchar](50) NULL,
	[Città] [nvarchar](50) NULL,
	[Ricarico] [int] NULL,
	[PzzoManodopera] [money] NULL,
	[Chiuso] [bit] NULL,
	[Riscosso] [bit] NULL,
	[Numero] [nvarchar](5) NULL,
	[ValorePreventivo] [money] NULL,
	[IVA] [int] NULL,
	[Anno] [int] NULL,
	[Preventivo] [bit] NULL,
	[FasciaTblCantieri] [int] NULL,
	[DaDividere] [bit] NULL,
	[Diviso] [bit] NULL,
	[Fatturato] [bit] NULL,
	[codRiferCant] [nvarchar](20) NULL,
 CONSTRAINT [PK_TblCantieri] PRIMARY KEY CLUSTERED 
(
	[IdCantieri] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblClienti]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblClienti](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[RagSocCli] [nvarchar](255) NULL,
	[Indirizzo] [nvarchar](50) NULL,
	[cap] [nvarchar](5) NULL,
	[Città] [nvarchar](50) NULL,
	[Tel1] [int] NULL,
	[Cell1] [int] NULL,
	[PartitaIva] [nvarchar](11) NULL,
	[CodFiscale] [nvarchar](16) NULL,
	[Data] [datetime] NULL,
	[Provincia] [nvarchar](4) NULL,
	[Note] [ntext] NULL,
 CONSTRAINT [PK_TblClienti] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblCompGruppoFrut]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCompGruppoFrut](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdTblGruppo] [int] NOT NULL,
	[IdTblFrutto] [int] NOT NULL,
	[Qta] [int] NOT NULL,
 CONSTRAINT [PK_TblCompGruppoFrut] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblDDTMef]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblDDTMef](
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
 CONSTRAINT [PK_TblDDTMef] PRIMARY KEY CLUSTERED 
(
	[IdDDTMef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblForitori]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblForitori](
	[IdFornitori] [int] IDENTITY(1,1) NOT NULL,
	[RagSocForni] [nvarchar](50) NULL,
	[Indirizzo] [nvarchar](50) NULL,
	[cap] [nvarchar](5) NULL,
	[Città] [nvarchar](50) NULL,
	[Tel1] [int] NULL,
	[Cell1] [int] NULL,
	[PartitaIva] [float] NULL,
	[CodFiscale] [nvarchar](16) NULL,
	[Abbreviato] [nvarchar](3) NULL,
 CONSTRAINT [PK_TblForitori] PRIMARY KEY CLUSTERED 
(
	[IdFornitori] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblFrutti]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFrutti](
	[ID1] [int] IDENTITY(1,1) NOT NULL,
	[descr001] [nvarchar](150) NULL,
 CONSTRAINT [PK_TblFrutti] PRIMARY KEY CLUSTERED 
(
	[ID1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblGruppiFrutti]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblGruppiFrutti](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomeGruppo] [varchar](100) NOT NULL,
	[Descrizione] [varchar](200) NOT NULL,
	[Completato] [bit] NULL,
 CONSTRAINT [PK_TblGruppiFrutti] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TblLocali]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblLocali](
	[IdLocali] [int] IDENTITY(1,1) NOT NULL,
	[NomeLocale] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblLocali] PRIMARY KEY CLUSTERED 
(
	[IdLocali] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblManodoperaCantieri]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblManodoperaCantieri](
	[IdManodopera] [int] IDENTITY(1,1) NOT NULL,
	[IdTbCantieri] [int] NULL,
	[Qta] [float] NULL,
	[DescriManodopera] [nvarchar](50) NULL,
	[Note] [ntext] NULL,
	[Data] [datetime] NULL,
	[IdTblOperaio] [int] NULL,
	[Pagato] [bit] NULL,
	[Con] [nvarchar](20) NULL,
	[Visibile] [bit] NULL,
	[Mano] [nvarchar](4) NULL,
	[Note2] [ntext] NULL,
	[Acquirente] [nvarchar](50) NULL,
	[ProtocolloInterno] [int] NULL,
 CONSTRAINT [PK_TblManodoperaCantieri] PRIMARY KEY CLUSTERED 
(
	[IdManodopera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblMaterialiCantieri]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMaterialiCantieri](
	[IdMaterialiCantiere] [int] IDENTITY(1,1) NOT NULL,
	[IdTblCantieri] [int] NULL,
	[IdTblOperaio] [int] NULL,
	[DescriMateriali] [nvarchar](255) NULL,
	[Qta] [float] NULL,
	[Visibile] [bit] NULL,
	[Ricalcolo] [bit] NULL,
	[ricaricoSiNo] [bit] NULL,
	[Data] [datetime] NULL,
	[PzzoUniCantiere] [money] NULL,
	[Rientro] [bit] NULL,
	[CodArt] [nvarchar](30) NULL,
	[DescriCodArt] [nvarchar](255) NULL,
	[Tipologia] [nvarchar](20) NULL,
	[UnitàDiMisura] [nvarchar](3) NULL,
	[ZOldNumeroBolla] [nvarchar](15) NULL,
	[Mate] [nvarchar](4) NULL,
	[Fascia] [int] NULL,
	[pzzoTemp] [money] NULL,
	[Acquirente] [nvarchar](50) NULL,
	[Fornitore] [nvarchar](3) NULL,
	[NumeroBolla] [int] NULL,
	[ProtocolloInterno] [int] NULL,
	[Note] [ntext] NULL,
	[Note2] [ntext] NULL,
	[PzzoFinCli] [money] NULL,
	[OperaioPagato] [bit] NULL,
 CONSTRAINT [PK_TblMaterialiCantieri] PRIMARY KEY CLUSTERED 
(
	[IdMaterialiCantiere] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblMatOrdFrut]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblMatOrdFrut](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCantiere] [int] NOT NULL,
	[IdGruppiFrutti] [int] NULL,
	[IdLocale] [int] NOT NULL,
	[DataOrdine] [date] NULL,
	[Appartamento] [varchar](50) NULL,
	[IdFrutto] [int] NULL,
	[QtaFrutti] [int] NULL,
 CONSTRAINT [PK_TblMatOrdFrut] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TblOperaio]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblOperaio](
	[IdOperaio] [int] IDENTITY(1,1) NOT NULL,
	[NomeOp] [nvarchar](50) NULL,
	[DescrOP] [nvarchar](50) NULL,
	[Suffisso] [nvarchar](4) NULL,
	[Operaio] [nvarchar](4) NULL,
	[CostoOperaio] [money] NULL,
 CONSTRAINT [PK_TblOperaio] PRIMARY KEY CLUSTERED 
(
	[IdOperaio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblPagamenti]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblPagamenti](
	[IdPagamenti] [int] IDENTITY(1,1) NOT NULL,
	[IdTblCantieri] [int] NULL,
	[data] [datetime] NULL,
	[Imporo] [money] NULL,
	[DescriPagamenti] [nvarchar](50) NULL,
	[Acconto] [bit] NULL,
	[Saldo] [bit] NULL,
 CONSTRAINT [PK_TblPagamenti] PRIMARY KEY CLUSTERED 
(
	[IdPagamenti] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblSpese]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSpese](
	[IdSpesa] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](50) NULL,
	[Prezzo] [money] NULL,
 CONSTRAINT [PK_TblSpese] PRIMARY KEY CLUSTERED 
(
	[IdSpesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblStampe]    Script Date: 05/11/2017 11:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblStampe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nomeStampa] [varchar](50) NULL,
 CONSTRAINT [PK_TblStampe] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TblCantieri]  WITH CHECK ADD  CONSTRAINT [FK_TblCantieri_TblClienti] FOREIGN KEY([IdTblClienti])
REFERENCES [dbo].[TblClienti] ([IdCliente])
GO
ALTER TABLE [dbo].[TblCantieri] CHECK CONSTRAINT [FK_TblCantieri_TblClienti]
GO
ALTER TABLE [dbo].[TblCompGruppoFrut]  WITH CHECK ADD  CONSTRAINT [FK_TblCompGruppoFrut_TblFrutti] FOREIGN KEY([IdTblFrutto])
REFERENCES [dbo].[TblFrutti] ([ID1])
GO
ALTER TABLE [dbo].[TblCompGruppoFrut] CHECK CONSTRAINT [FK_TblCompGruppoFrut_TblFrutti]
GO
ALTER TABLE [dbo].[TblCompGruppoFrut]  WITH CHECK ADD  CONSTRAINT [FK_TblCompGruppoFrut_TblGruppiFrutti] FOREIGN KEY([IdTblGruppo])
REFERENCES [dbo].[TblGruppiFrutti] ([Id])
GO
ALTER TABLE [dbo].[TblCompGruppoFrut] CHECK CONSTRAINT [FK_TblCompGruppoFrut_TblGruppiFrutti]
GO
ALTER TABLE [dbo].[TblManodoperaCantieri]  WITH CHECK ADD  CONSTRAINT [FK_TblManodoperaCantieri_TblCantieri] FOREIGN KEY([IdTbCantieri])
REFERENCES [dbo].[TblCantieri] ([IdCantieri])
GO
ALTER TABLE [dbo].[TblManodoperaCantieri] CHECK CONSTRAINT [FK_TblManodoperaCantieri_TblCantieri]
GO
ALTER TABLE [dbo].[TblMaterialiCantieri]  WITH CHECK ADD  CONSTRAINT [FK_TblMaterialiCantieri_TblCantieri] FOREIGN KEY([IdTblCantieri])
REFERENCES [dbo].[TblCantieri] ([IdCantieri])
GO
ALTER TABLE [dbo].[TblMaterialiCantieri] CHECK CONSTRAINT [FK_TblMaterialiCantieri_TblCantieri]
GO
ALTER TABLE [dbo].[TblMaterialiCantieri]  WITH CHECK ADD  CONSTRAINT [FK_TblMaterialiCantieri_TblOperaio] FOREIGN KEY([IdTblOperaio])
REFERENCES [dbo].[TblOperaio] ([IdOperaio])
GO
ALTER TABLE [dbo].[TblMaterialiCantieri] CHECK CONSTRAINT [FK_TblMaterialiCantieri_TblOperaio]
GO
ALTER TABLE [dbo].[TblMatOrdFrut]  WITH CHECK ADD  CONSTRAINT [FK_TblMatOrdFrut_TblGruppiFrutti] FOREIGN KEY([IdGruppiFrutti])
REFERENCES [dbo].[TblGruppiFrutti] ([Id])
GO
ALTER TABLE [dbo].[TblMatOrdFrut] CHECK CONSTRAINT [FK_TblMatOrdFrut_TblGruppiFrutti]
GO
ALTER TABLE [dbo].[TblMatOrdFrut]  WITH CHECK ADD  CONSTRAINT [FK_TblMatOrdFrut_TblLocali] FOREIGN KEY([IdLocale])
REFERENCES [dbo].[TblLocali] ([IdLocali])
GO
ALTER TABLE [dbo].[TblMatOrdFrut] CHECK CONSTRAINT [FK_TblMatOrdFrut_TblLocali]
GO
ALTER TABLE [dbo].[TblPagamenti]  WITH CHECK ADD  CONSTRAINT [FK_TblPagamenti_TblCantieri] FOREIGN KEY([IdTblCantieri])
REFERENCES [dbo].[TblCantieri] ([IdCantieri])
GO
ALTER TABLE [dbo].[TblPagamenti] CHECK CONSTRAINT [FK_TblPagamenti_TblCantieri]
GO
