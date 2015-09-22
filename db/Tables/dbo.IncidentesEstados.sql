CREATE TABLE [dbo].[IncidentesEstados]
(
[IdEstado] [int] NOT NULL,
[IdIncidente] [int] NOT NULL,
[IdUsuario] [int] NOT NULL,
[FechaActualizacion] [datetime] NOT NULL,
[Observacion] [varchar] (500) COLLATE Modern_Spanish_CI_AS NOT NULL,
[IdIncidenteEstado] [int] NOT NULL IDENTITY(1, 1)
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IncidentesEstados] ADD CONSTRAINT [PK_IncidentesEstados] PRIMARY KEY CLUSTERED  ([IdEstado], [IdIncidente]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IncidentesEstados] ADD CONSTRAINT [FK_IncidentesEstados_Estados] FOREIGN KEY ([IdEstado]) REFERENCES [dbo].[Estados] ([IdEstado])
GO
ALTER TABLE [dbo].[IncidentesEstados] ADD CONSTRAINT [FK_IncidentesEstados_Incidentes] FOREIGN KEY ([IdIncidente]) REFERENCES [dbo].[Incidentes] ([IdIncidente])
GO
