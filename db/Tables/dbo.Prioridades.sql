CREATE TABLE [dbo].[Prioridades]
(
[IdPrioridad] [int] NOT NULL,
[Nombre] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Prioridades] ADD CONSTRAINT [PK_Prioridades] PRIMARY KEY CLUSTERED  ([IdPrioridad]) ON [PRIMARY]
GO
