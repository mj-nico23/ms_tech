CREATE TABLE [dbo].[Estados]
(
[IdEstado] [int] NOT NULL,
[Nombre] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Estados] ADD CONSTRAINT [PK_Estados] PRIMARY KEY CLUSTERED  ([IdEstado]) ON [PRIMARY]
GO
