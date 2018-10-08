USE [CopaDoMundo]
GO

/****** Object:  Table [dbo].[Fases]    Script Date: 07/10/2018 20:29:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fases](
	[FaseId] [int] NOT NULL,
	[Descricao] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Fase] PRIMARY KEY CLUSTERED 
(
	[FaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


