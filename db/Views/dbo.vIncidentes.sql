SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vIncidentes]
as
SELECT i.IdIncidente, ie.IdEstado, e.Nombre AS Estado, ie.FechaActualizacion, CONVERT(varchar(10), i.Fecha, 103) as FechaMostrar, p3.Nombre AS Prioridad, 
u.Nombre + ' ' + u.Apellido AS Usuario,  c.Nombre + ' ' + c.Apellido AS Cliente, p2.Nombre AS Producto, p.Nombre AS Problema, i.Descripcion,
i.Fecha, i.IdPrioridad
FROM dbo.Incidentes i
INNER JOIN dbo.Usuarios u ON u.IdUsuario = i.IdUsuario
INNER JOIN dbo.Clientes c ON c.IdCliente = i.IdCliente
INNER JOIN dbo.Problemas p ON p.IdProblema = i.IdProblema
INNER JOIN dbo.Productos p2 ON p2.IdProducto = p.IdProducto
INNER JOIN dbo.Prioridades p3 ON p3.IdPrioridad = i.IdPrioridad
JOIN dbo.IncidentesEstados ie ON ie.IdIncidenteEstado  = (
    SELECT TOP 1 IdIncidenteEstado FROM IncidentesEstados ie1 WHERE ie1.IdIncidente = i.IdIncidente ORDER BY ie1.FechaActualizacion DESC
)
INNER JOIN dbo.Estados e ON e.IdEstado = ie.IdEstado
GO
