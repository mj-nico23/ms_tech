using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ms_tech.Clases;
using ms_tech.Models;
using ms_tech.ViewModels;

namespace ms_tech.Controllers
{
    public class EstadisticasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Estadisticas
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Estadisticas/Index/" });
            }
            Usuarios usuario = new Usuarios();
            usuario = usuario.Obtener(User.Identity.Name);
            if (usuario == null || usuario.IdUsuarioTipo != 1)
            {
                ViewBag.Error = "No tiene permisos para acceder a esta página.";
                return View();
            }

            ViewBag.Error = "";
            EstadisticasViewModel estadisticas = new EstadisticasViewModel();
            ViewBag.IdClienteTipo = new SelectList(db.ClientesTipos, "IdClienteTipo", "Nombre");
            ViewBag.IdUsuarioTipo = new SelectList(db.UsuariosTipos, "IdUsuarioTipo", "Nombre");
            return View(estadisticas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AbrirReporte([Bind(Include = "TipoReporte,FechaDesde,FechaHasta,Orden,TipoOrden,Nombre,Apellido,IdClienteTipo,Mail,IdUsuarioTipo")] EstadisticasViewModel estadisticas)
        {
            if (!Request.IsAuthenticated)
            {
                RedirectToAction("Login", "Usuarios", new { r = "/Estadisticas/Index/" });
            }

            if (estadisticas == null)
            {
                return HttpNotFound();
            }

            #region Reporte

            DateTime fechaDesde = DateTime.MinValue;
            DateTime fechaHasta = DateTime.MinValue;

            if (estadisticas.TipoReporte < 5)
            {
                DateTime.TryParse(estadisticas.FechaDesde.ToString(), out fechaDesde);
                DateTime.TryParse(estadisticas.FechaHasta.ToString(), out fechaHasta);

                if (fechaDesde == DateTime.MinValue)
                    fechaDesde = new DateTime(1900, 1, 1);

                if (fechaHasta == DateTime.MinValue)
                    fechaHasta = new DateTime(2100, 1, 1);

                estadisticas.FechaDesde = fechaDesde;
                estadisticas.FechaHasta = fechaHasta;
            }

            estadisticas.Orden = estadisticas.Orden + " " + estadisticas.TipoOrden;

            Document pdfDoc;
            switch (estadisticas.TipoReporte)
            {
                case 1:
                    pdfDoc = ReporteIncidentes(estadisticas.FechaDesde, estadisticas.FechaHasta, estadisticas.Orden);
                    break;
                case 2:
                    pdfDoc = ReporteIncidentesNuevos(estadisticas.FechaDesde, estadisticas.FechaHasta, estadisticas.Orden);
                    break;
                case 3:
                    pdfDoc = ReporteIncidentesEnProceso(estadisticas.FechaDesde, estadisticas.FechaHasta, estadisticas.Orden);
                    break;
                case 4:
                    pdfDoc = ReporteIncidentesTerminados(estadisticas.FechaDesde, estadisticas.FechaHasta, estadisticas.Orden);
                    break;
                case 5:
                    pdfDoc = ReporteClientes(estadisticas.Nombre, estadisticas.Apellido, estadisticas.IdClienteTipo, estadisticas.Mail, estadisticas.Orden);
                    break;
                case 6:
                    pdfDoc = ReporteUsuarios(estadisticas.Nombre, estadisticas.Apellido, estadisticas.IdUsuarioTipo, estadisticas.Mail, estadisticas.Orden);
                    break;
                default:
                    pdfDoc = new Document();
                    break;
            }

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" + "filename=Reporte_" + DateTime.Now.Ticks.ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
            #endregion

            return View(estadisticas);
        }

        private Document ReporteIncidentes(DateTime fechaDesde, DateTime fechaHasta, string orden)
        {
            string sql = string.Format("SELECT * FROM vIncidentes where fecha BETWEEN '{0}' AND '{1}' order by {2}", fechaDesde.ToString("yyyy-MM-dd"), fechaHasta.ToString("yyyy-MM-dd 23:59"), orden);

            DataTable dt = SQL.Execute(sql);

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 30f, 10f, 70f, 0f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            PdfEventsGenerico events = new PdfEventsGenerico();
            events.Logo = Server.MapPath("/Images") + "/logo.jpg";
            events.Titulo = "Reporte de Incidentes";

            pdfWriter.PageEvent = events;

            pdfDoc.Open();
            PdfPTable tabla1 = new PdfPTable(9);
            tabla1.WidthPercentage = 100f;
            tabla1.SetWidths(new float[] { 5, 8, 7, 7, 14, 14, 10, 10, 25 });

            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 8));
            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 8));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Nro", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Fecha", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Estado", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Prioridad", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Usuario", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Cliente", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Producto", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Problema", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Descripción", FNC_iTextSharp.Fuente.fArial10b, 0));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["IdIncidente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["FechaMostrar"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Estado"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Prioridad"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Usuario"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Cliente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Producto"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Problema"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Descripcion"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
            }

            pdfDoc.Add(tabla1);

            pdfDoc.Close();

            return pdfDoc;

        }

        private Document ReporteIncidentesNuevos(DateTime fechaDesde, DateTime fechaHasta, string orden)
        {
            string sql = string.Format("SELECT * FROM vIncidentes where IdEstado=1 AND fecha BETWEEN '{0}' AND '{1}' order by {2}", fechaDesde.ToString("yyyy-MM-dd"), fechaHasta.ToString("yyyy-MM-dd 23:59"), orden);

            DataTable dt = SQL.Execute(sql);

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 30f, 10f, 70f, 0f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            PdfEventsGenerico events = new PdfEventsGenerico();
            events.Logo = Server.MapPath("/Images") + "/logo.jpg";
            events.Titulo = "Reporte de Incidentes Nuevos";

            pdfWriter.PageEvent = events;

            pdfDoc.Open();
            PdfPTable tabla1 = new PdfPTable(8);
            tabla1.WidthPercentage = 100f;
            tabla1.SetWidths(new float[] { 5, 10, 10, 15, 15, 10, 10, 25 });

            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 8));
            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 8));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Nro", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Fecha", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Prioridad", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Usuario", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Cliente", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Producto", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Problema", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Descripción", FNC_iTextSharp.Fuente.fArial10b, 0));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["IdIncidente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["FechaMostrar"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Prioridad"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Usuario"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Cliente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Producto"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Problema"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Descripcion"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
            }

            pdfDoc.Add(tabla1);

            pdfDoc.Close();

            return pdfDoc;

        }

        private Document ReporteIncidentesEnProceso(DateTime fechaDesde, DateTime fechaHasta, string orden)
        {
            string sql = string.Format("SELECT * FROM vIncidentes where IdEstado <> 1 AND IdEstado <> 4 AND fecha BETWEEN '{0}' AND '{1}' order by {2}", fechaDesde.ToString("yyyy-MM-dd"), fechaHasta.ToString("yyyy-MM-dd 23:59"), orden);

            DataTable dt = SQL.Execute(sql);

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 30f, 10f, 70f, 0f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            PdfEventsGenerico events = new PdfEventsGenerico();
            events.Logo = Server.MapPath("/Images") + "/logo.jpg";
            events.Titulo = "Reporte de Incidentes en Proceso";

            pdfWriter.PageEvent = events;

            pdfDoc.Open();
            PdfPTable tabla1 = new PdfPTable(9);
            tabla1.WidthPercentage = 100f;
            tabla1.SetWidths(new float[] { 5, 8, 7, 7, 14, 14, 10, 10, 25 });

            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 8));
            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 8));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Nro", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Fecha", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Estado", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Prioridad", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Usuario", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Cliente", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Producto", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Problema", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Descripción", FNC_iTextSharp.Fuente.fArial10b, 0));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["IdIncidente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["FechaMostrar"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Estado"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Prioridad"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Usuario"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Cliente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Producto"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Problema"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Descripcion"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
            }

            pdfDoc.Add(tabla1);

            pdfDoc.Close();

            return pdfDoc;

        }

        private Document ReporteIncidentesTerminados(DateTime fechaDesde, DateTime fechaHasta, string orden)
        {
            string sql = string.Format("SELECT * FROM vIncidentes where IdEstado = 4 AND fecha BETWEEN '{0}' AND '{1}' order by {2}", fechaDesde.ToString("yyyy-MM-dd"), fechaHasta.ToString("yyyy-MM-dd 23:59"), orden);

            DataTable dt = SQL.Execute(sql);

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 30f, 10f, 70f, 0f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            PdfEventsGenerico events = new PdfEventsGenerico();
            events.Logo = Server.MapPath("/Images") + "/logo.jpg";
            events.Titulo = "Reporte de Incidentes Terminados";

            pdfWriter.PageEvent = events;

            pdfDoc.Open();
            PdfPTable tabla1 = new PdfPTable(8);
            tabla1.WidthPercentage = 100f;
            tabla1.SetWidths(new float[] { 5, 10, 10, 15, 15, 10, 10, 25 });

            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 8));
            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 8));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Nro", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Fecha", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Prioridad", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Usuario", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Cliente", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Producto", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Problema", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Descripción", FNC_iTextSharp.Fuente.fArial10b, 0));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["IdIncidente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["FechaMostrar"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Prioridad"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Usuario"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Cliente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Producto"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Problema"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Descripcion"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
            }

            pdfDoc.Add(tabla1);

            pdfDoc.Close();

            return pdfDoc;

        }

        private Document ReporteClientes(string nombre, string apellido, int tipoCliente, string mail, string orden)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT c.Nombre, c.Apellido, c.Mail, ct.Nombre AS TipoCliente, c.Direccion, c.Telefono, c.Activo");
            sql.AppendLine("FROM dbo.Clientes c");
            sql.AppendLine("INNER JOIN dbo.ClientesTipos ct ON ct.IdClienteTipo = c.IdClienteTipo");
            sql.AppendLine("WHERE 1=1");

            if (nombre != "")
                sql.AppendFormat(" AND c.Nombre LIKE '{0}%'", nombre);
            if (apellido != "")
                sql.AppendFormat(" AND c.Apellido LIKE '{0}%'", apellido);
            if (tipoCliente != 0)
                sql.AppendFormat(" AND c.IdClienteTipo = {0}", tipoCliente);
            if (mail != "")
                sql.AppendFormat(" AND c.Mail LIKE '{0}%'", mail);

            sql.AppendFormat(" Order by {0}", orden);

            DataTable dt = SQL.Execute(sql.ToString());

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 30f, 10f, 70f, 0f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            PdfEventsGenerico events = new PdfEventsGenerico();
            events.Logo = Server.MapPath("/Images") + "/logo.jpg";
            events.Titulo = "Reporte de Clientes";

            pdfWriter.PageEvent = events;

            pdfDoc.Open();
            PdfPTable tabla1 = new PdfPTable(7);
            tabla1.WidthPercentage = 100f;
            tabla1.SetWidths(new float[] { 20, 20, 19, 10, 15, 10, 6 });

            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 7));
            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 7));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Nombre", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Apellido", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Mail", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Tipo Cliente", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Dirección", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Teléfono", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Activo", FNC_iTextSharp.Fuente.fArial10b, 0));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Nombre"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Apellido"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Mail"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["TipoCliente"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Direccion"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Telefono"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                if (dt.Rows[i]["Activo"].ToString() == "True")
                    tabla1.AddCell(FNC_iTextSharp.GetCell("Si", FNC_iTextSharp.Fuente.fArial10, 0));
                else
                    tabla1.AddCell(FNC_iTextSharp.GetCell("No", FNC_iTextSharp.Fuente.fArial10, 0));
            }

            pdfDoc.Add(tabla1);

            pdfDoc.Close();

            return pdfDoc;

        }

        private Document ReporteUsuarios(string nombre, string apellido, int tipoUsuario, string mail, string orden)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT u.Nombre, u.Apellido, u.Email, u.Activo, ut.Nombre AS TipoUsuario ");
            sql.AppendLine("FROM dbo.Usuarios u");
            sql.AppendLine("INNER JOIN dbo.UsuariosTipos ut ON ut.IdUsuarioTipo = u.IdUsuarioTipo");
            sql.AppendLine("WHERE 1=1");

            if (nombre != "")
                sql.AppendFormat(" AND u.Nombre LIKE '{0}%'", nombre);
            if (apellido != "")
                sql.AppendFormat(" AND u.Apellido LIKE '{0}%'", apellido);
            if (tipoUsuario != 0)
                sql.AppendFormat(" AND u.IdUsuarioTipo = {0}", tipoUsuario);
            if (mail != "")
                sql.AppendFormat(" AND u.Email LIKE '{0}%'", mail);

            sql.AppendFormat(" Order by {0}", orden);

            DataTable dt = SQL.Execute(sql.ToString());

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 30f, 10f, 70f, 0f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            PdfEventsGenerico events = new PdfEventsGenerico();
            events.Logo = Server.MapPath("/Images") + "/logo.jpg";
            events.Titulo = "Reporte de Usuarios";

            pdfWriter.PageEvent = events;

            pdfDoc.Open();
            PdfPTable tabla1 = new PdfPTable(5);
            tabla1.WidthPercentage = 100f;
            tabla1.SetWidths(new float[] { 20, 20, 20, 20, 20 });

            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 5));
            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 5));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Nombre", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Apellido", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Mail", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Tipo Usuario", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Activo", FNC_iTextSharp.Fuente.fArial10b, 0));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Nombre"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["Apellido"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["email"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                tabla1.AddCell(FNC_iTextSharp.GetCell(dt.Rows[i]["TipoUsuario"].ToString(), FNC_iTextSharp.Fuente.fArial10, 0));
                if (dt.Rows[i]["Activo"].ToString() == "True")
                    tabla1.AddCell(FNC_iTextSharp.GetCell("Si", FNC_iTextSharp.Fuente.fArial10, 0));
                else
                    tabla1.AddCell(FNC_iTextSharp.GetCell("No", FNC_iTextSharp.Fuente.fArial10, 0));
            }

            pdfDoc.Add(tabla1);

            pdfDoc.Close();

            return pdfDoc;

        }

    }
}