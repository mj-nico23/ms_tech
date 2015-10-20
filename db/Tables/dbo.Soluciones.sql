CREATE TABLE [dbo].[Soluciones]
(
[IdSolucion] [int] NOT NULL IDENTITY(1, 1),
[IdProblema] [int] NOT NULL,
[Descripcion] [varchar] (500) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Activo] [bit] NOT NULL,
[FechaCreacion] [datetime] NOT NULL,
[FechaModificacion] [datetime] NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[Soluciones] WITH NOCHECK ADD
CONSTRAINT [FK_Soluciones_Problemas] FOREIGN KEY ([IdProblema]) REFERENCES [dbo].[Problemas] ([IdProblema])
GO
ALTER TABLE [dbo].[Soluciones] ADD CONSTRAINT [PK_Soluciones] PRIMARY KEY CLUSTERED  ([IdSolucion]) ON [PRIMARY]
GO
