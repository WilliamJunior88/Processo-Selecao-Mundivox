USE [CopaDoMundo]
GO

/****** Object:  Table [dbo].[Jogos]    Script Date: 07/10/2018 20:30:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Jogos](
	[JogoId] [int] IDENTITY(1,1) NOT NULL,
	[SelecaoId1] [int] NOT NULL,
	[SelecaoId2] [int] NOT NULL,
	[Gol1] [int] NOT NULL,
	[Gol2] [int] NOT NULL,
	[FaseId] [int] NOT NULL,
 CONSTRAINT [PK_Jogos] PRIMARY KEY CLUSTERED 
(
	[JogoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Jogos]  WITH CHECK ADD  CONSTRAINT [FK_Jogos_Fases] FOREIGN KEY([FaseId])
REFERENCES [dbo].[Fases] ([FaseId])
GO

ALTER TABLE [dbo].[Jogos] CHECK CONSTRAINT [FK_Jogos_Fases]
GO

ALTER TABLE [dbo].[Jogos]  WITH CHECK ADD  CONSTRAINT [FK_Jogos_Selecoes1] FOREIGN KEY([SelecaoId1])
REFERENCES [dbo].[Selecoes] ([IdSelecao])
GO

ALTER TABLE [dbo].[Jogos] CHECK CONSTRAINT [FK_Jogos_Selecoes1]
GO

ALTER TABLE [dbo].[Jogos]  WITH CHECK ADD  CONSTRAINT [FK_Jogos_Selecoes2] FOREIGN KEY([SelecaoId2])
REFERENCES [dbo].[Selecoes] ([IdSelecao])
GO

ALTER TABLE [dbo].[Jogos] CHECK CONSTRAINT [FK_Jogos_Selecoes2]
GO


