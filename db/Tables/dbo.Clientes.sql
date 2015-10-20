CREATE TABLE [dbo].[Clientes]
(
[IdCliente] [int] NOT NULL IDENTITY(1, 1),
[Nombre] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Apellido] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Mail] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[IdClienteTipo] [int] NOT NULL,
[Password] [varchar] (256) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL,
[Direccion] [varchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Telefono] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[FechaCreacion] [datetime] NOT NULL,
[FechaModificacion] [datetime] NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[Clientes] WITH NOCHECK ADD
CONSTRAINT [FK_Clientes_ClientesTipos] FOREIGN KEY ([IdClienteTipo]) REFERENCES [dbo].[ClientesTipos] ([IdClienteTipo])
GO
ALTER TABLE [dbo].[Clientes] ADD CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED  ([IdCliente]) ON [PRIMARY]
GO
