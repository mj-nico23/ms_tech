CREATE TABLE [dbo].[Problemas]
(
[IdProblema] [int] NOT NULL IDENTITY(1, 1),
[IdProducto] [int] NOT NULL,
[Nombre] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL,
[FechaCreacion] [datetime] NOT NULL,
[FechaModificacion] [datetime] NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[Problemas] WITH NOCHECK ADD
CONSTRAINT [FK_Problemas_Productos] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Productos] ([IdProducto])
GO
ALTER TABLE [dbo].[Problemas] ADD CONSTRAINT [PK_Problemas] PRIMARY KEY CLUSTERED  ([IdProblema]) ON [PRIMARY]
GO
