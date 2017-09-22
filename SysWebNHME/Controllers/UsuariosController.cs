using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Mvc;
using SysWebNHME.Clases;
using SysWebNHME.Models;

namespace SysWebNHME.Controllers
{
    public class UsuariosController : Controller
    {
        private DBWNHMEEntities db = new DBWNHMEEntities();
        /**/
        //IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        public ActionResult Login(string Mensaje)
        {
            //Response.Write("carajo "+Mensaje);
            Session.Clear();
            SessionFilter MAC = new SessionFilter();
            if (MAC.MacValida())
            {
                ViewBag.Message = Mensaje;                
                return View();
            }
            return View("Error");            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios objUser)
        {
            if (ModelState.IsValid)
            {
                using (DBWNHMEEntities db = new DBWNHMEEntities())
                {
                    SessionFilter PC = new SessionFilter();                    
                    var obj2 = from t1 in db.Usuarios
                               join t2 in db.Roles on t1.IdRol equals t2.IdRol
                               where t2.Estado == true && t1.Usuario == objUser.Usuario && t1.Clave == objUser.Clave && t1.Estado == true
                               select new { t1.IDUsuario, t1.Usuario, t1.Nombre, t1.IdRol };
                    if (obj2.Any())
                    {
                        foreach (var item in obj2)
                        {
                            Session.Timeout = 1500;
                            Session["IDUsuario"] = item.IDUsuario;
                            Session["Usuario"] = item.Usuario;
                            Session["NombreUsuario"] = item.Nombre;
                            Session["IdRol"] = item.IdRol;                            
                            Session["Host"] = PC.GetPCName();
                        }
                        /*cargamos los permisos del usuario en session*/
                        var obj = db.SP_CargarPermisos(Convert.ToInt32(Session["IdRol"])).ToList();
                        
                        if (obj.Count>0)
                        {
                            List<String> permisos = new List<String>(150);

                            foreach (SP_CargarPermisos_Result datos in obj)
                            {
                                permisos.Add(datos.IdPermiso.ToString());
                            }
                            Session["Permisos"] = permisos;
                            return Redirect("/Main/Index");
                        }
                    }
                    
                    return RedirectToAction("Login", new { mensaje = "Usuario o Clave incorrecta Consulte con el Administrador" });
                }
            }
            return RedirectToAction("Login", new { mensaje = "Usuario o Clave incorrecta Consulte con el Administrador" });
        }

        public ActionResult Error()
        {
            return View();
        }
        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Roles);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "Descripcion");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDUsuario,Usuario,Nombre,Clave,IdRol,Estado,FechaCrea,FechaBaja,HostCrea")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "Descripcion", usuarios.IdRol);
            return View(usuarios);
        }
        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "Descripcion", usuarios.IdRol);
            return View(usuarios);
        }
        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDUsuario,Usuario,Nombre,Clave,IdRol,Estado,FechaCrea,FechaBaja,HostCrea")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "Descripcion", usuarios.IdRol);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
    }
}
