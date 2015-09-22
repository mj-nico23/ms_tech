CREATE TABLE [dbo].[Incidentes]
(
[IdIncidente] [int] NOT NULL IDENTITY(1, 1),
[IdUsuario] [int] NOT NULL,
[IdCliente] [int] NOT NULL,
[IdProblema] [int] NOT NULL,
[Fecha] [datetime] NOT NULL,
[Descripcion] [varchar] (500) COLLATE Modern_Spanish_CI_AS NOT NULL,
[IdPrioridad] [int] NOT NULL CONSTRAINT [DF_Incidentes_Prioridad] DEFAULT ((1)),
[Calificacion] [tinyint] NULL,
[Comentario] [varchar] (500) COLLATE Modern_Spanish_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Incidentes] ADD CONSTRAINT [PK_Incidentes] PRIMARY KEY CLUSTERED  ([IdIncidente]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Incidentes] ADD CONSTRAINT [FK_Incidentes_Clientes] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Clientes] ([IdCliente])
GO
ALTER TABLE [dbo].[Incidentes] ADD CONSTRAINT [FK_Incidentes_Prioridades] FOREIGN KEY ([IdPrioridad]) REFERENCES [dbo].[Prioridades] ([IdPrioridad])
GO
ALTER TABLE [dbo].[Incidentes] ADD CONSTRAINT [FK_Incidentes_Problemas] FOREIGN KEY ([IdProblema]) REFERENCES [dbo].[Problemas] ([IdProblema])
GO
ALTER TABLE [dbo].[Incidentes] ADD CONSTRAINT [FK_Incidentes_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
