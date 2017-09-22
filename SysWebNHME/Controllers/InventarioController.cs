using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SysWebNHME.Clases;
using SysWebNHME.Models;

namespace SysWebNHME.Controllers
{
    [SessionFilter]
    public class InventarioController : Controller
    {
        private DBWNHMEEntities db = new DBWNHMEEntities();
        // GET: Inventario
        public ActionResult Index(string mensaje)
        {
            ViewBag.IDUsuarioCrea = new SelectList(db.Usuarios, "IDUsuario", "Usuario");
            ViewBag.mensaje = mensaje;
            return View();
        }

        public ActionResult ListadoMovimientos()
        {
            return PartialView(db.TiposDeMov.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTipoMov,Descripcion,DescCorta,TipoMov,FechaCrea,FechaModifica,Estado,IDUsuarioCrea,HostCrea,IDUsuarioEdita,HostEdita")] TiposDeMov  Mov)
        {
            if (ModelState.IsValid)
            {
                
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                var obj = db.SP_AdminMovimientos(0, Mov.Descripcion, Mov.DescCorta, Mov.TipoMov,true, Convert.ToInt32(Session["IDUsuario"]), Session["Host"].ToString(), 0, "", 1, Mensaje);
                return RedirectToAction("Index", new { mensaje = Mensaje.Value.ToString() });
                //return RedirectToAction("Index", new { mensaje = "<div class='alert alert-success' role='alert'>" + Mensaje.Value.ToString() + "</ div >" });

            }
            return RedirectToAction("Index", new { mensaje = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IDTipoMov,Descripcion,DescCorta,TipoMov,FechaCrea,FechaModifica,Estado,IDUsuarioCrea,HostCrea,IDUsuarioEdita,HostEdita")] TiposDeMov Mov)
        {
            if (ModelState.IsValid)
            {

                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                var obj = db.SP_AdminMovimientos(Mov.IDTipoMov, Mov.Descripcion, Mov.DescCorta, Mov.TipoMov, true, 0, "", Convert.ToInt32(Session["IDUsuario"]), Session["Host"].ToString(), 2, Mensaje);
                return RedirectToAction("Index", new { mensaje = Mensaje.Value.ToString() });

            }
            return RedirectToAction("Index", new { mensaje = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BajaAlta([Bind(Include = "IDTipoMov,Descripcion,DescCorta,TipoMov,FechaCrea,FechaModifica,Estado,IDUsuarioCrea,HostCrea,IDUsuarioEdita,HostEdita")] TiposDeMov Mov)
        {
            if (ModelState.IsValid)
            {
                
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                var obj = db.SP_AdminMovimientos(Mov.IDTipoMov, Mov.Descripcion, Mov.DescCorta, Mov.TipoMov, true, 0, "", Convert.ToInt32(Session["IDUsuario"]), Session["Host"].ToString(), 3, Mensaje);
                return RedirectToAction("Index", new { mensaje = Mensaje.Value.ToString() });
            }
            return RedirectToAction("Index", new { mensaje = "" });
        }

        public ActionResult Ajax()
        {
            var lista = db.TiposDeMov.ToList();
            //return Json(lista, JsonRequestBehavior.AllowGet);

            return Json(
                        db.TiposDeMov.Select(x => new {
                            id = x.IDTipoMov,
                            name = x.Descripcion
                        }), JsonRequestBehavior.AllowGet);

            //harley gay
        }
    }
}