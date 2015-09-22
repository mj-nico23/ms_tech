CREATE TABLE [dbo].[Productos]
(
[IdProducto] [int] NOT NULL IDENTITY(1, 1),
[Nombre] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL,
[FechaCreacion] [datetime] NOT NULL,
[FechaModificacion] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Productos] ADD CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED  ([IdProducto]) ON [PRIMARY]
GO
