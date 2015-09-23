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
    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Productos
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Productos/Index" });
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = sortOrder == "nombre" ? "nombre_desc" : "nombre";
            ViewBag.ActivoSortParm = sortOrder == "activo" ? "activo_desc" : "activo";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var productos = db.Productos.Where(s => 1 == 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(s => s.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nombre":
                    productos = productos.OrderBy(c => c.Nombre);
                    break;
                case "nombre_desc":
                    productos = productos.OrderByDescending(c => c.Nombre);
                    break;
                case "activo":
                    productos = productos.OrderByDescending(c => c.Activo);
                    break;
                case "activo_desc":
                    productos = productos.OrderBy(c => c.Activo);
                    break;
                default:
                    productos = productos.OrderBy(c => c.Nombre);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(productos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Productos/Details/" + id });
            }

            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Productos/Create/" });
            }

            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProducto,Nombre,Activo,FechaCreacion,FechaModificacion")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                productos.FechaCreacion = DateTime.Now;
                productos.FechaModificacion = DateTime.Now;
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Productos/Edit/" + id });
            }

            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProducto,Nombre,Activo,FechaCreacion,FechaModificacion")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.Entry(productos).Property("FechaCreacion").IsModified = false;
                db.Entry(productos).Property("FechaModificacion").CurrentValue = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Productos/Edit/" + id });
            }

            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
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
