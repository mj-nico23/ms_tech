using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ms_tech.Models;
using ms_tech.ViewModels;
using ms_tech.Clases;

namespace ms_tech.Controllers
{
    public class IncidentesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Incidentes
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Incidentes/Index/" });
            }

            //var incidentes = db.Incidentes.Include(i => i.Clientes).Include(i => i.Problemas).Include(i => i.Usuarios);

            var incidentesVM = (from s in db.Incidentes
                                join p in db.Problemas on s.IdProblema equals p.IdProblema
                                join p1 in db.Productos on p.IdProducto equals p1.IdProducto
                                join p2 in db.Prioridades on s.IdPrioridad equals p2.IdPrioridad
                                join c in db.Clientes on s.IdCliente equals c.IdCliente
                                join u in db.Usuarios on s.IdUsuario equals u.IdUsuario
                                //where s.IdIncidente == id
                                select new IncidentesViewModel
                                {
                                    IdIncidente = s.IdIncidente,
                                    IdProblema = s.IdProblema,
                                    IdProducto = p.IdProducto,
                                    IdCliente = s.IdCliente,
                                    Descripcion = s.Descripcion,
                                    IdUsuario = s.IdUsuario,
                                    Fecha = s.Fecha,
                                    IdPrioridad = s.IdPrioridad,
                                    Prioridades = p2,
                                    Problemas = p,
                                    Productos = p1,
                                    Clientes = c,
                                    Usuarios = u,
                                    NombreUsuario = u.Email,
                                    Calificacion = s.Calificacion,
                                    Comentario = s.Comentario
                                });


            return View(incidentesVM.ToList());
        }

        // GET: Incidentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Incidentes/Details/" + id });
            }

            IncidentesViewModel incidentesVM = (from s in db.Incidentes
                                                join p in db.Problemas on s.IdProblema equals p.IdProblema
                                                join p1 in db.Productos on p.IdProducto equals p1.IdProducto
                                                join p2 in db.Prioridades on s.IdPrioridad equals p2.IdPrioridad
                                                join c in db.Clientes on s.IdCliente equals c.IdCliente
                                                join u in db.Usuarios on s.IdUsuario equals u.IdUsuario
                                                where s.IdIncidente == id
                                                select new IncidentesViewModel
                                                {
                                                    IdIncidente = s.IdIncidente,
                                                    IdProblema = s.IdProblema,
                                                    IdProducto = p.IdProducto,
                                                    IdCliente = s.IdCliente,
                                                    Descripcion = s.Descripcion,
                                                    IdPrioridad = s.IdPrioridad,
                                                    Usuarios = u,
                                                    IdUsuario = s.IdUsuario,
                                                    Fecha = s.Fecha,
                                                    Prioridades = p2,
                                                    Problemas = p,
                                                    Productos = p1,
                                                    Clientes = c,
                                                    NombreUsuario = u.Email,
                                                    Calificacion = s.Calificacion,
                                                    Comentario = s.Comentario
                                                }).FirstOrDefault();
            if (incidentesVM == null)
            {
                return HttpNotFound();
            }

            return View(incidentesVM);
        }

        // GET: Incidentes/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Incidentes/Create/" });
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre");
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre");
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre");
            ViewBag.IdPrioridad = new SelectList(db.Prioridades.OrderByDescending(x => x.IdPrioridad), "IdPrioridad", "Nombre");


            IncidentesViewModel incidentesVM = new IncidentesViewModel();
            incidentesVM.Fecha = DateTime.Today;


            return View(incidentesVM);
        }

        // POST: Incidentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIncidente,IdUsuario,IdCliente,IdProblema,Fecha,Descripcion,IdPrioridad,Calificacion,Comentario")] IncidentesViewModel incidentesVM)
        {
            if (ModelState.IsValid)
            {
                Incidentes incidentes = new Incidentes();
                Usuarios usuario = new Usuarios();
                incidentes.IdUsuario = usuario.ObtenerId(User.Identity.Name);
                incidentes.IdCliente = incidentesVM.IdCliente;
                incidentes.IdProblema = incidentesVM.IdProblema;
                incidentes.Fecha = incidentesVM.Fecha;
                incidentes.Descripcion = incidentesVM.Descripcion;
                incidentes.IdPrioridad = incidentesVM.IdPrioridad;



                db.Incidentes.Add(incidentes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre", incidentesVM.IdCliente);
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre", incidentesVM.IdProblema);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Email", incidentesVM.IdUsuario);
            ViewBag.IdPrioridad = new SelectList(db.Prioridades.OrderByDescending(x => x.IdPrioridad), "IdPrioridad", "Nombre", incidentesVM.IdPrioridad);

            return View(incidentesVM);
        }

        // GET: Incidentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Incidentes/Edit/" + id });
            }





            //IncidentesViewModel incidentesVM = new IncidentesViewModel();
            //incidentesVM.Fecha = DateTime.Today;

            IncidentesViewModel incidentesVM = (from s in db.Incidentes
                                                join p in db.Problemas on s.IdProblema equals p.IdProblema
                                                join p1 in db.Productos on p.IdProducto equals p1.IdProducto
                                                join p2 in db.Prioridades on s.IdPrioridad equals p2.IdPrioridad
                                                join c in db.Clientes on s.IdCliente equals c.IdCliente
                                                join u in db.Usuarios on s.IdUsuario equals u.IdUsuario
                                                where s.IdIncidente == id
                                                select new IncidentesViewModel
                                                {
                                                    IdIncidente = s.IdIncidente,
                                                    IdProblema = s.IdProblema,
                                                    IdProducto = p.IdProducto,
                                                    IdCliente = s.IdCliente,
                                                    Descripcion = s.Descripcion,
                                                    IdUsuario = s.IdUsuario,
                                                    Fecha = s.Fecha,
                                                    Prioridades = p2,
                                                    Problemas = p,
                                                    Productos = p1,
                                                    Clientes = c,
                                                    NombreUsuario = u.Email
                                                }).FirstOrDefault();

            if (incidentesVM == null)
            {
                return HttpNotFound();
            }

            var usuarios = db.Usuarios.Select(u => new
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre + " " + u.Apellido
            });

            ViewBag.IdUsuario = new SelectList(usuarios, "IdUsuario", "Nombre", incidentesVM.IdUsuario);

            var clientes = db.Clientes.Select(u => new
            {
                IdCliente = u.IdCliente,
                Nombre = u.Nombre + " " + u.Apellido
            });
            ViewBag.IdCliente = new SelectList(clientes, "IdCliente", "Nombre", incidentesVM.IdCliente);

            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre", incidentesVM.IdProblema);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", incidentesVM.IdProducto);
            ViewBag.IdPrioridad = new SelectList(db.Prioridades.OrderByDescending(x => x.IdPrioridad), "IdPrioridad", "Nombre", incidentesVM.IdPrioridad);

            return View(incidentesVM);
        }

        // POST: Incidentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdIncidente,IdUsuario,IdCliente,IdProblema,Fecha,Descripcion,IdPrioridad,Calificacion,Comentario")] IncidentesViewModel incidentesVM)
        {

            if (ModelState.IsValid)
            {
                Incidentes incidentes = db.Incidentes.Where(a => a.IdIncidente.Equals(incidentesVM.IdIncidente)).FirstOrDefault();
                db.Entry(incidentes).State = EntityState.Modified;
                Usuarios usuario = new Usuarios();
                incidentes.IdUsuario = incidentesVM.IdUsuario;
                incidentes.IdCliente = incidentesVM.IdCliente;
                incidentes.IdProblema = incidentesVM.IdProblema;
                incidentes.Descripcion = incidentesVM.Descripcion;
                incidentes.IdPrioridad = incidentesVM.IdPrioridad;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre");
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre");
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre");
            ViewBag.IdPrioridad = new SelectList(db.Prioridades.OrderByDescending(x => x.IdPrioridad), "IdPrioridad", "Nombre");

            return View(incidentesVM);
        }

        // GET: Incidentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidentes incidentes = db.Incidentes.Find(id);
            if (incidentes == null)
            {
                return HttpNotFound();
            }
            return View(incidentes);
        }

        // POST: Incidentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incidentes incidentes = db.Incidentes.Find(id);
            db.Incidentes.Remove(incidentes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void Imprimir(int id)
        {
            Document pdfDoc = new Document(PageSize.A4, 30f, 10f, 70f, 0f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            PdfEventsGenerico events = new PdfEventsGenerico();
            events.Logo = Server.MapPath("/Images") + "/logo.jpg";
            events.Titulo = "Comprobante de Incidente";

            pdfWriter.PageEvent = events;

            pdfDoc.Open();
            //PdfPTable tab_titulo = new PdfPTable(3);
            //tab_titulo.SetWidths(new float[] { 25, 50, 25 });

            //string logoUrl = Server.MapPath("/Images") + "/logo.jpg";

            //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(logoUrl);
            
            //PdfPCell cell1= new PdfPCell(jpg, true);
            //cell1.Border = 0;
            //tab_titulo.AddCell(cell1);

            //cell1 =FNC_iTextSharp.GetCell("Comprobante de Incidente", FNC_iTextSharp.Fuente.fArial16b, 1, null, 2);
            //cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            //tab_titulo.AddCell(cell1);

            //pdfDoc.Add(tab_titulo);
            pdfDoc.Add(new Paragraph("prueba"));
            pdfDoc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" + "filename=" + DateTime.Now.Ticks.ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();

            //return "Se imprime el " + id.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
