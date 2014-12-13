using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using ms_tech.Models;
using ms_tech.ViewModels;

namespace ms_tech.Controllers
{
    public class UsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Start()
        {
            
            return View();
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", new { r = "/Usuarios/Index" });
            }
            var usuarios = db.Usuarios.Include(u => u.UsuariosTipos);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", new { r = "/Usuarios/Details/" + id });
            }

            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", new { r = "/Usuarios/Create/" });
            }

            ViewBag.IdUsuarioTipo = new SelectList(db.UsuariosTipos, "IdUsuarioTipo", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,IdUsuarioTipo,Email,Nombre,Apellido,Activo,Password,FechaCreacion,FechaModificacion")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3";
                usuarios.FechaCreacion = DateTime.Now;
                usuarios.FechaModificacion = DateTime.Now;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuarioTipo = new SelectList(db.UsuariosTipos, "IdUsuarioTipo", "Nombre", usuarios.IdUsuarioTipo);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", new { r = "/Usuarios/Edit/" + id });
            }

            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuarioTipo = new SelectList(db.UsuariosTipos, "IdUsuarioTipo", "Nombre", usuarios.IdUsuarioTipo);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,IdUsuarioTipo,Email,Nombre,Apellido,Activo,Password,FechaCreacion,FechaModificacion")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.Entry(usuarios).Property("Password").IsModified = false;
                db.Entry(usuarios).Property("FechaCreacion").IsModified = false;
                db.Entry(usuarios).Property("FechaModificacion").CurrentValue = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuarioTipo = new SelectList(db.UsuariosTipos, "IdUsuarioTipo", "Nombre", usuarios.IdUsuarioTipo);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", new { r = "/Usuarios/Edit/" + id });
            }

            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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

        #region Login

        public ActionResult Login(string r)
        {
            ViewBag.ReturnUrl = r;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios u, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string sha = getHash(u.Password); //Encripto password
                var v = db.Usuarios.Where(a => a.Email.Equals(u.Email) && a.Password.Equals(sha)).FirstOrDefault();
                if (v != null)
                {
                    FormsAuthentication.SetAuthCookie(u.Email, false);

                    return RedirectToLocal(returnUrl);
                }
                ViewBag.Msg = "Error de Login";
            }
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salir()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Start", "Usuarios");
        }

        public ActionResult ChangePassword()
        {
            //for initialize viewmodel
            var cambiarPasswordVM = new CambiarPasswordViewModel();
            Usuarios usuario = new Usuarios();
            //assign values for viewmodel
            cambiarPasswordVM.IdUsuario = usuario.ObtenerId(User.Identity.Name);

            if (cambiarPasswordVM.IdUsuario == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View("ChangePassword", cambiarPasswordVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(CambiarPasswordViewModel c)
        {
            if (ModelState.IsValid)
            {
                string old = getHash(c.OldPassword); //Encripto password

                var v = db.Usuarios.Where(a => a.IdUsuario.Equals(c.IdUsuario) && a.Password.Equals(old)).FirstOrDefault();
                if (v != null)
                {
                    if (c.NewPassword == c.ConfirmPassword)
                    {
                        string nuevo = getHash(c.NewPassword); //Encripto password
                        v.Password = nuevo;
                        db.SaveChanges();

                        ViewBag.Msg = "La contraseña se cambio correctamente";
                    }
                    else
                        ViewBag.Msg = "Error: La contraseña nueva y la confirmacíon no coinciden";

                }
                else
                    ViewBag.Msg = "Error: La contraseña anterior no es correcta";

            }
            return View();
        }

        private string getHash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }


        #endregion

        #region Metodos Particulares
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Start", "Usuarios");
        }

       
        #endregion
    }
}
