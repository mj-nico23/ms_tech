CREATE TABLE [dbo].[Estados]
(
[IdEstado] [int] NOT NULL IDENTITY(1, 1),
[Nombre] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL,
[Finalizado] [bit] NOT NULL CONSTRAINT [DF_Estados_Finalizado] DEFAULT ((0))
) ON [PRIMARY]
ALTER TABLE [dbo].[Estados] ADD 
CONSTRAINT [PK_Estados] PRIMARY KEY CLUSTERED  ([IdEstado]) ON [PRIMARY]
GO
