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
using PagedList;
using System.Text;

namespace ms_tech.Controllers
{
    public class IncidentesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Incidentes
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Incidentes/Index/" });
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.Cliente = sortOrder == "cliente" ? "cliente_desc" : "cliente";
            ViewBag.Usuario = sortOrder == "usuario" ? "usuario_desc" : "usuario";
            ViewBag.Producto = sortOrder == "producto" ? "producto_desc" : "producto";
            ViewBag.Problema = sortOrder == "problema" ? "problema_desc" : "problema";
            ViewBag.Fecha = sortOrder == "fecha" ? "fecha_desc" : "fecha";
            ViewBag.Prioridad = sortOrder == "prioriodad" ? "prioriodad_desc" : "prioriodad";
            ViewBag.Estado = sortOrder == "estado" ? "estado_desc" : "estado";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var incidentesVM = (from s in db.Incidentes
                                join p in db.Problemas on s.IdProblema equals p.IdProblema
                                join p1 in db.Productos on p.IdProducto equals p1.IdProducto
                                join p2 in db.Prioridades on s.IdPrioridad equals p2.IdPrioridad
                                join c in db.Clientes on s.IdCliente equals c.IdCliente
                                join u in db.Usuarios on s.IdUsuario equals u.IdUsuario
                                //join ie in db.IncidentesEstados on s.IdIncidente equals ie.IdIncidente
                                //join e in db.Estados on ie.IdEstado equals e.IdEstado
                                let ie = db.IncidentesEstados.Where(iex => iex.IdIncidente == s.IdIncidente).OrderByDescending(iex => iex.FechaActualizacion).Take(1)
                                let e = db.Estados.Where(ex => ie.FirstOrDefault().IdEstado == ex.IdEstado).Take(1)
                                //orderby ie.FirstOrDefault().FechaActualizacion descending
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
                                    IdEstado = ie.FirstOrDefault() == null ? 0 : ie.FirstOrDefault().IdEstado,
                                    IncidentesEstados = ie.FirstOrDefault(),
                                    Comentario = s.Comentario,
                                    Estados = e.FirstOrDefault()
                                });

            if (!String.IsNullOrEmpty(searchString))
            {
                incidentesVM = incidentesVM.Where(s => s.NombreUsuario.Contains(searchString) || s.Clientes.Apellido.Contains(searchString) || s.Clientes.Nombre.Contains(searchString)
                || s.Usuarios.Apellido.Contains(searchString) || s.Usuarios.Nombre.Contains(searchString) || s.Productos.Nombre.Contains(searchString) || s.Problemas.Nombre.Contains(searchString)
                || s.Estados.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "cliente":
                    incidentesVM = incidentesVM.OrderBy(c => c.Clientes.Nombre);
                    break;
                case "cliente_desc":
                    incidentesVM = incidentesVM.OrderByDescending(c => c.Clientes.Nombre);
                    break;
                case "usuario":
                    incidentesVM = incidentesVM.OrderBy(c => c.Usuarios.Nombre);
                    break;
                case "usuario_desc":
                    incidentesVM = incidentesVM.OrderByDescending(c => c.Usuarios.Nombre);
                    break;
                case "producto":
                    incidentesVM = incidentesVM.OrderBy(c => c.Productos.Nombre);
                    break;
                case "producto_desc":
                    incidentesVM = incidentesVM.OrderByDescending(c => c.Productos.Nombre);
                    break;
                case "problema":
                    incidentesVM = incidentesVM.OrderBy(c => c.Productos.Nombre);
                    break;
                case "problema_desc":
                    incidentesVM = incidentesVM.OrderByDescending(c => c.Problemas.Nombre);
                    break;
                case "fecha":
                    incidentesVM = incidentesVM.OrderBy(c => c.Productos.Nombre);
                    break;
                case "fecha_desc":
                    incidentesVM = incidentesVM.OrderByDescending(c => c.Fecha);
                    break;
                case "prioriodad":
                    incidentesVM = incidentesVM.OrderBy(c => c.Prioridades.Nombre);
                    break;
                case "prioriodad_desc":
                    incidentesVM = incidentesVM.OrderByDescending(c => c.Prioridades.Nombre);
                    break;
                case "estado":
                    incidentesVM = incidentesVM.OrderBy(c => c.Estados.Nombre);
                    break;
                case "estado_desc":
                    incidentesVM = incidentesVM.OrderByDescending(c => c.Estados.Nombre);
                    break;
                default:
                    incidentesVM = incidentesVM.OrderByDescending(c => c.Fecha);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize));

            return View(incidentesVM.ToPagedList(pageNumber, pageSize));
            //return View(incidentesVM.ToList());
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


            IncidentesViewModel incidentesVM = new IncidentesViewModel();
            incidentesVM.Fecha = DateTime.Today;

            var clientes = db.Clientes.Select(u => new
            {
                IdCliente = u.IdCliente,
                Nombre = u.Nombre + " " + u.Apellido
            }).OrderBy(x => x.Nombre);
            ViewBag.IdCliente = new SelectList(clientes, "IdCliente", "Nombre", incidentesVM.IdCliente);
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre");
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre");
            ViewBag.IdPrioridad = new SelectList(db.Prioridades.OrderByDescending(x => x.IdPrioridad), "IdPrioridad", "Nombre");



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

                IncidentesEstados incidentesEstados = new IncidentesEstados();
                incidentesEstados.IdIncidente = incidentes.IdIncidente;
                incidentesEstados.IdEstado = 1;
                incidentesEstados.Finalizado = false;
                incidentesEstados.FechaActualizacion = DateTime.Now;
                incidentesEstados.IdUsuario = incidentes.IdUsuario;
                incidentesEstados.Observacion = "Alta de incidente.";
                db.IncidentesEstados.Add(incidentesEstados);
                db.SaveChanges();


                return RedirectToAction("Index");
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

        public ActionResult EditarEstado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Incidentes/Edit/" + id });
            }

            IncidentesViewModel incidentesVM = (from s in db.Incidentes
                                                join p in db.Problemas on s.IdProblema equals p.IdProblema
                                                join p1 in db.Productos on p.IdProducto equals p1.IdProducto
                                                join p2 in db.Prioridades on s.IdPrioridad equals p2.IdPrioridad
                                                join c in db.Clientes on s.IdCliente equals c.IdCliente
                                                join u in db.Usuarios on s.IdUsuario equals u.IdUsuario
                                                let ie = db.IncidentesEstados.Where(iex => iex.IdIncidente == s.IdIncidente).OrderByDescending(iex => iex.FechaActualizacion).Take(1)
                                                //let e = db.Estados.Where(ex => ie.FirstOrDefault().IdEstado == ex.IdEstado).Take(1)
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
                                                    IncidentesEstados = ie.FirstOrDefault(),
                                                    NombreUsuario = u.Email,
                                                    IdPrioridad = p2.IdPrioridad,
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
            ViewBag.IdEstado = new SelectList(db.Estados, "IdEstado", "Nombre", incidentesVM.IncidentesEstados == null ? 1 : incidentesVM.IncidentesEstados.IdEstado);

            return View(incidentesVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEstado([Bind(Include = "IdIncidente,IdUsuario,IdEstado,Observaciones")] IncidentesViewModel incidentesVM)
        {

            if (ModelState.IsValid)
            {
                IncidentesEstados incidentesEstados = new IncidentesEstados();
                Usuarios usuario = new Usuarios();
                incidentesEstados.IdUsuario = usuario.ObtenerId(User.Identity.Name);
                incidentesEstados.IdIncidente = incidentesVM.IdIncidente;
                incidentesEstados.IdEstado = incidentesVM.IdEstado;
                incidentesEstados.FechaActualizacion = DateTime.Now;
                incidentesEstados.Observacion = incidentesVM.Observaciones;
                db.IncidentesEstados.Add(incidentesEstados);
                db.SaveChanges();

                ActualizarMarcaFinalizado(incidentesEstados.IdEstado, incidentesEstados.IdIncidente);

                return RedirectToAction("Index");
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
            ViewBag.IdEstado = new SelectList(db.Estados, "IdEstado", "Nombre", incidentesVM.IdEstado);

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

        public ActionResult Imprimir(int id)
        {
            if (!Request.IsAuthenticated)
            {
                RedirectToAction("Login", "Usuarios", new { r = "/Incidentes/Imprimir/" + id });
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


            Document pdfDoc = new Document(PageSize.A4, 30f, 10f, 70f, 0f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            PdfEventsGenerico events = new PdfEventsGenerico();
            events.Logo = Server.MapPath("/Images") + "/logo.jpg";
            events.Titulo = "Comprobante de Incidente";

            var url_string = Url.Action("Details", "Incidentes", new { id = incidentesVM.IdIncidente }, protocol: Request.Url.Scheme);

            BarcodeQRCode qrcode = new BarcodeQRCode(url_string, 1, 1, null);
            Image img = qrcode.GetImage();
            img.ScaleAbsoluteWidth(80);

            pdfWriter.PageEvent = events;

            pdfDoc.Open();
            PdfPTable tabla1 = new PdfPTable(4);
            tabla1.SetWidths(new float[] { 18, 35, 25, 25 });

            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 4));
            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 4));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Incidente Nro:", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell(incidentesVM.IdIncidente.ToString(), FNC_iTextSharp.Fuente.fArial10, 0, null, 2));

            PdfPCell cell1 = FNC_iTextSharp.GetCell("", FNC_iTextSharp.Fuente.fArial10, 0, null);
            cell1.AddElement(img);
            cell1.Rowspan = 6;
            tabla1.AddCell(cell1);

            tabla1.AddCell(FNC_iTextSharp.GetCell("Cliente:", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell(incidentesVM.Clientes.Nombre + " " + incidentesVM.Clientes.Apellido, FNC_iTextSharp.Fuente.fArial10, 0, null, 2));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Producto:", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell(incidentesVM.Productos.Nombre, FNC_iTextSharp.Fuente.fArial10, 0, null, 2));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Problema:", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell(incidentesVM.Problemas.Nombre, FNC_iTextSharp.Fuente.fArial10, 0, null, 2));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Fecha:", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell(incidentesVM.Fecha.ToString("dd/MM/yyyy"), FNC_iTextSharp.Fuente.fArial10, 0, null, 2));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Prioridad:", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell(incidentesVM.Prioridades.Nombre, FNC_iTextSharp.Fuente.fArial10, 0, null, 2));

            tabla1.AddCell(FNC_iTextSharp.GetCell("Descripción:", FNC_iTextSharp.Fuente.fArial10b, 0));
            tabla1.AddCell(FNC_iTextSharp.GetCell(incidentesVM.Descripcion, FNC_iTextSharp.Fuente.fArial10, 0, null, 3));

            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 4));
            tabla1.AddCell(FNC_iTextSharp.GetCell(" ", FNC_iTextSharp.Fuente.fArial10, 0, null, 4));
            tabla1.AddCell(FNC_iTextSharp.GetCell("Para comprobar el estado de su Incidente escanee el codigo con su celular o ingrese a la siguiente dirección: " + url_string, FNC_iTextSharp.Fuente.fArial10, 0, null, 4));

            pdfDoc.Add(tabla1);

            pdfDoc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" + "filename=" + DateTime.Now.Ticks.ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();

            return View(incidentesVM);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ActualizarMarcaFinalizado(int idEstado, int idIncidentes)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendLine("IF (SELECT Finalizado FROM dbo.Estados WHERE IdEstado=" + idEstado + ") = 1");
            sbQuery.AppendLine("BEGIN");
            sbQuery.AppendLine("	UPDATE dbo.IncidentesEstados SET Finalizado=1");
            sbQuery.AppendLine("	WHERE IdIncidenteEstado = (");
            sbQuery.AppendLine("								SELECT MAX(IdIncidenteEstado) ");
            sbQuery.AppendLine("								FROM dbo.IncidentesEstados");
            sbQuery.AppendLine("								WHERE IdIncidente=" + idIncidentes + "");
            sbQuery.AppendLine("								)");
            sbQuery.AppendLine("END	");

            db.Database.ExecuteSqlCommand(sbQuery.ToString());
        }
    }
}
