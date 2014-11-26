using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ms_tech.Models;
using ms_tech.ViewModels;

namespace ms_tech.Controllers
{
    public class SolucionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Soluciones
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Soluciones/Index" });
            }

           

            IEnumerable<SolucionesViewModel> model = (from s in db.Soluciones
                                                      join p in db.Problemas on s.IdProblema equals p.IdProblema
                                                      join p1 in db.Productos on p.IdProducto equals p1.IdProducto
                                                      select new SolucionesViewModel
                                                      {
                                                          IdSolucion = s.IdSolucion,
                                                          IdProblema = s.IdProblema,
                                                          IdProducto = p.IdProducto,
                                                          Descripcion = s.Descripcion,
                                                          Activo = s.Activo,
                                                          FechaCreacion = s.FechaCreacion,
                                                          FechaModificacion = s.FechaModificacion,
                                                          Problemas = p,
                                                          Productos = p1
                                                      });

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model.ToList());
        }

        // GET: Soluciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Soluciones/Details/" + id });
            }

            SolucionesViewModel solucionesVM = (from s in db.Soluciones
                                                join p in db.Problemas on s.IdProblema equals p.IdProblema
                                                join p1 in db.Productos on p.IdProducto equals p1.IdProducto
                                                where s.IdSolucion == id
                                                select new SolucionesViewModel
                                                {
                                                    IdSolucion = s.IdSolucion,
                                                    IdProblema = s.IdProblema,
                                                    IdProducto = p.IdProducto,
                                                    Descripcion = s.Descripcion,
                                                    Activo = s.Activo,
                                                    FechaCreacion = s.FechaCreacion,
                                                    FechaModificacion = s.FechaModificacion,
                                                    Problemas = p,
                                                    Productos = p1
                                                }).FirstOrDefault();

            if (solucionesVM == null)
            {
                return HttpNotFound();
            }

            return View(solucionesVM);
        }

        // GET: Soluciones/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Soluciones/Create/" });
            }

            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre");
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre");

            SolucionesViewModel solucionesVM = new SolucionesViewModel();
            solucionesVM.Activo = true;

            return View("Create", solucionesVM);
        }

        // POST: Soluciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSolucion,IdProblema,Descripcion,Activo,FechaCreacion,FechaModificacion")] SolucionesViewModel soluciones)
        {
            if (ModelState.IsValid)
            {
                Soluciones s = new Soluciones();
                s.IdProblema = soluciones.IdProblema;
                s.Descripcion = soluciones.Descripcion;
                s.Activo = soluciones.Activo;
                s.FechaCreacion = DateTime.Now;
                s.FechaModificacion = DateTime.Now;
                db.Soluciones.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre", soluciones.IdProblema);
            return View(soluciones);
        }

        // GET: Soluciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Soluciones/Edit/" + id });
            }

            Soluciones soluciones = db.Soluciones.Find(id);
            if (soluciones == null)
            {
                return HttpNotFound();
            }

            SolucionesViewModel solucionesVM = new SolucionesViewModel();
            solucionesVM.IdSolucion = soluciones.IdSolucion;
            solucionesVM.IdProblema = soluciones.IdProblema;
            solucionesVM.Activo = soluciones.Activo;
            solucionesVM.Descripcion = soluciones.Descripcion;

            var problemas = db.Problemas.Where(a => a.IdProblema.Equals(soluciones.IdProblema)).FirstOrDefault();
            if (problemas != null)
            {
                solucionesVM.IdProducto = problemas.IdProducto;
            }

            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", solucionesVM.IdProducto);

            var problemasList = db.Problemas.Where(a => a.IdProducto.Equals(solucionesVM.IdProducto));

            ViewBag.IdProblema = new SelectList(problemasList, "IdProblema", "Nombre", soluciones.IdProblema);

            return View("Edit", solucionesVM);
        }

        // POST: Soluciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSolucion,IdProblema,Descripcion,Activo,FechaCreacion,FechaModificacion")] Soluciones soluciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soluciones).State = EntityState.Modified;
                db.Entry(soluciones).Property("FechaCreacion").IsModified = false;
                db.Entry(soluciones).Property("FechaModificacion").CurrentValue = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre", soluciones.IdProblema);
            return View(soluciones);
        }

        // GET: Soluciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuarios", new { r = "/Soluciones/Edit/" + id });
            }

            //Soluciones soluciones = db.Soluciones.Find(id);


            SolucionesViewModel soluciones = (from s in db.Soluciones
                                                join p in db.Problemas on s.IdProblema equals p.IdProblema
                                                join p1 in db.Productos on p.IdProducto equals p1.IdProducto
                                                where s.IdSolucion == id
                                                select new SolucionesViewModel
                                                {
                                                    IdSolucion = s.IdSolucion,
                                                    IdProblema = s.IdProblema,
                                                    IdProducto = p.IdProducto,
                                                    Descripcion = s.Descripcion,
                                                    Activo = s.Activo,
                                                    FechaCreacion = s.FechaCreacion,
                                                    FechaModificacion = s.FechaModificacion,
                                                    Problemas = p,
                                                    Productos = p1
                                                }).FirstOrDefault();

            if (soluciones == null)
            {
                return HttpNotFound();
            }

            return View(soluciones);

        }

        // POST: Soluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Soluciones soluciones = db.Soluciones.Find(id);
            db.Soluciones.Remove(soluciones);
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

        [HttpPost]
        public JsonResult GetProblemas(string id)
        {
            var problemas = db.Problemas.Where(a => a.IdProducto.ToString() == id);
            return Json(new SelectList(problemas, "IdProblema", "Nombre"));
        }
    }
}
