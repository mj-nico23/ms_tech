CREATE TABLE [dbo].[UsuariosTipos]
(
[IdUsuarioTipo] [int] NOT NULL IDENTITY(1, 1),
[Nombre] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UsuariosTipos] ADD CONSTRAINT [PK_UsuariosTipos] PRIMARY KEY CLUSTERED  ([IdUsuarioTipo]) ON [PRIMARY]
GO
