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
    public class IncidentesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Incidentes
        public ActionResult Index()
        {
            var incidentes = db.Incidentes.Include(i => i.Clientes).Include(i => i.Problemas).Include(i => i.Usuarios);
            return View(incidentes.ToList());
        }

        // GET: Incidentes/Details/5
        public ActionResult Details(int? id)
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

        // GET: Incidentes/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre");
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre");
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre");

            IncidentesViewModel incidentesVM = new IncidentesViewModel();
            incidentesVM.Fecha = DateTime.Today;
            
            return View(incidentesVM);
        }

        // POST: Incidentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIncidente,IdUsuario,IdCliente,IdProblema,Fecha,Descripcion,Prioridad,Calificacion,Comentario")] Incidentes incidentes)
        {
            if (ModelState.IsValid)
            {
                db.Incidentes.Add(incidentes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre", incidentes.IdCliente);
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre", incidentes.IdProblema);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Email", incidentes.IdUsuario);
            return View(incidentes);
        }

        // GET: Incidentes/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre", incidentes.IdCliente);
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre", incidentes.IdProblema);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Email", incidentes.IdUsuario);
            return View(incidentes);
        }

        // POST: Incidentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdIncidente,IdUsuario,IdCliente,IdProblema,Fecha,Descripcion,Prioridad,Calificacion,Comentario")] Incidentes incidentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre", incidentes.IdCliente);
            ViewBag.IdProblema = new SelectList(db.Problemas, "IdProblema", "Nombre", incidentes.IdProblema);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Email", incidentes.IdUsuario);
            return View(incidentes);
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
