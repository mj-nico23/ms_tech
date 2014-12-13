using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ms_tech.Models;
using PagedList;

namespace ms_tech.Controllers
{
    public class ClientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clientes
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Clientes/Index" });
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = sortOrder == "nombre" ? "nombre_desc" : "nombre";
            ViewBag.ApellidoSortParm = sortOrder == "apellido" ? "apellido_desc" : "apellido";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.TipoSortParm = sortOrder == "tipo" ? "tipo_desc" : "tipo";
            ViewBag.ActivoSortParm = sortOrder == "activo" ? "activo_desc" : "activo";
            ViewBag.DireccionSortParm = sortOrder == "direccion" ? "direccion_desc" : "direccion";
            ViewBag.TelefonoSortParm = sortOrder == "telefono" ? "telefono_desc" : "telefono";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var clientes = db.Clientes.Include(c => c.ClientesTipos);

            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.Nombre.Contains(searchString)
                                       || s.Apellido.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "tipo":
                    clientes = clientes.OrderBy(c => c.ClientesTipos.Nombre);
                    break;
                case "tipo_desc":
                    clientes = clientes.OrderByDescending(c => c.ClientesTipos.Nombre);
                    break;
                case "nombre":
                    clientes = clientes.OrderBy(c => c.Nombre);
                    break;
                case "nombre_desc":
                    clientes = clientes.OrderByDescending(c => c.Nombre);
                    break;
                case "apellido":
                    clientes = clientes.OrderBy(c => c.Apellido);
                    break;
                case "apellido_desc":
                    clientes = clientes.OrderByDescending(c => c.Apellido);
                    break;
                case "email":
                    clientes = clientes.OrderBy(c => c.Mail);
                    break;
                case "email_desc":
                    clientes = clientes.OrderByDescending(c => c.Mail);
                    break;
                case "activo":
                    clientes = clientes.OrderByDescending(c => c.Activo);
                    break;
                case "activo_desc":
                    clientes = clientes.OrderBy(c => c.Activo);
                    break;
                case "direccion":
                    clientes = clientes.OrderBy(c => c.Direccion);
                    break;
                case "direccion_desc":
                    clientes = clientes.OrderByDescending(c => c.Direccion);
                    break;
                case "telefono":
                    clientes = clientes.OrderBy(c => c.Telefono);
                    break;
                case "telefono_desc":
                    clientes = clientes.OrderByDescending(c => c.Telefono);
                    break;
                default:
                    clientes = clientes.OrderBy(c => c.IdCliente);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize));

            return View(clientes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Clientes/Details" + id });
            }

            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Clientes/Create" });
            }

            ViewBag.IdClienteTipo = new SelectList(db.ClientesTipos, "IdClienteTipo", "Nombre");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCliente,Nombre,Apellido,Mail,IdClienteTipo,Password,Activo,Direccion,Telefono,FechaCreacion,FechaModificacion")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                clientes.Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3";
                clientes.FechaCreacion = DateTime.Now;
                clientes.FechaModificacion = DateTime.Now;
                db.Clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdClienteTipo = new SelectList(db.ClientesTipos, "IdClienteTipo", "Nombre", clientes.IdClienteTipo);
            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Clientes/Edit" + id });
            }

            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdClienteTipo = new SelectList(db.ClientesTipos, "IdClienteTipo", "Nombre", clientes.IdClienteTipo);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,Nombre,Apellido,Mail,IdClienteTipo,Password,Activo,Direccion,Telefono,FechaCreacion,FechaModificacion")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientes).State = EntityState.Modified;
                db.Entry(clientes).Property("Password").IsModified = false;
                db.Entry(clientes).Property("FechaCreacion").IsModified = false;
                db.Entry(clientes).Property("FechaModificacion").CurrentValue = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdClienteTipo = new SelectList(db.ClientesTipos, "IdClienteTipo", "Nombre", clientes.IdClienteTipo);
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Clientes/Delete" + id });
            }

            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
            db.SaveChanges();
            return RedirectToAction("Index");
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
