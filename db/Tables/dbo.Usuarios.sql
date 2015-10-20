CREATE TABLE [dbo].[Usuarios]
(
[IdUsuario] [int] NOT NULL IDENTITY(1, 1),
[IdUsuarioTipo] [int] NOT NULL,
[Email] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Nombre] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Apellido] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL,
[Password] [varchar] (256) COLLATE Modern_Spanish_CI_AS NOT NULL,
[FechaCreacion] [datetime] NOT NULL,
[FechaModificacion] [datetime] NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[Usuarios] WITH NOCHECK ADD
CONSTRAINT [FK_Usuarios_UsuariosTipos] FOREIGN KEY ([IdUsuarioTipo]) REFERENCES [dbo].[UsuariosTipos] ([IdUsuarioTipo])
GO
ALTER TABLE [dbo].[Usuarios] ADD CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED  ([IdUsuario]) ON [PRIMARY]
GO
