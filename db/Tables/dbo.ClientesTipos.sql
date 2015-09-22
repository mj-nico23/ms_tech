CREATE TABLE [dbo].[ClientesTipos]
(
[IdClienteTipo] [int] NOT NULL IDENTITY(1, 1),
[Nombre] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientesTipos] ADD CONSTRAINT [PK_ClientesTipos] PRIMARY KEY CLUSTERED  ([IdClienteTipo]) ON [PRIMARY]
GO
