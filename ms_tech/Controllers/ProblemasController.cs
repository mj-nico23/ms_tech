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
    public class ProblemasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Problemas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Problemas/Index" });
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.ProductoSortParm = sortOrder == "producto" ? "producto_desc" : "producto";
            ViewBag.NombreSortParm = sortOrder == "nombre" ? "nombre_desc" : "nombre";
            ViewBag.ActivoSortParm = sortOrder == "activo" ? "activo_desc" : "activo";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var problemas = db.Problemas.Include(p => p.Productos);

            if (!String.IsNullOrEmpty(searchString))
            {
                problemas = problemas.Where(s => s.Nombre.Contains(searchString) || s.Productos.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "producto":
                    problemas = problemas.OrderBy(c => c.Productos.Nombre);
                    break;
                case "producto_desc":
                    problemas = problemas.OrderByDescending(c => c.Productos.Nombre);
                    break;
                case "nombre":
                    problemas = problemas.OrderBy(c => c.Nombre);
                    break;
                case "nombre_desc":
                    problemas = problemas.OrderByDescending(c => c.Nombre);
                    break;
                case "activo":
                    problemas = problemas.OrderByDescending(c => c.Activo);
                    break;
                case "activo_desc":
                    problemas = problemas.OrderBy(c => c.Activo);
                    break;
                default:
                    problemas = problemas.OrderBy(c => c.Nombre);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(problemas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Problemas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Problemas/Details/" + id });
            }

            Problemas problemas = db.Problemas.Find(id);
            if (problemas == null)
            {
                return HttpNotFound();
            }
            return View(problemas);
        }

        // GET: Problemas/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Problemas/Create/" });
            }

            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: Problemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProblema,IdProducto,Nombre,Activo,FechaCreacion,FechaModificacion")] Problemas problemas)
        {
            if (ModelState.IsValid)
            {
                problemas.FechaCreacion = DateTime.Now;
                problemas.FechaModificacion = DateTime.Now;
                db.Problemas.Add(problemas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", problemas.IdProducto);
            return View(problemas);
        }

        // GET: Problemas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Problemas/Edit/" + id });
            }

            Problemas problemas = db.Problemas.Find(id);
            if (problemas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", problemas.IdProducto);
            return View(problemas);
        }

        // POST: Problemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProblema,IdProducto,Nombre,Activo,FechaCreacion,FechaModificacion")] Problemas problemas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(problemas).State = EntityState.Modified;
                db.Entry(problemas).Property("FechaCreacion").IsModified = false;
                db.Entry(problemas).Property("FechaModificacion").CurrentValue = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", problemas.IdProducto);
            return View(problemas);
        }

        // GET: Problemas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Problemas/Edit/" + id });
            }

            Problemas problemas = db.Problemas.Find(id);
            if (problemas == null)
            {
                return HttpNotFound();
            }
            return View(problemas);
        }

        // POST: Problemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Problemas problemas = db.Problemas.Find(id);
            db.Problemas.Remove(problemas);
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
