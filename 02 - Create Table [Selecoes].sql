USE [CopaDoMundo]
GO

/****** Object:  Table [dbo].[Selecoes]    Script Date: 07/10/2018 20:30:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Selecoes](
	[IdSelecao] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[QtdTitulos] [int] NULL,
	[Bandeira] [varbinary](max) NULL,
	[Descricao] [nvarchar](100) NULL,
 CONSTRAINT [PK_Selecoes] PRIMARY KEY CLUSTERED 
(
	[IdSelecao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


