using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SysWebNHME.Clases;
using SysWebNHME.Models;

namespace SysWebNHME.Controllers
{
    [SessionFilter]
    public class AreasController : Controller
    {
        private DBWNHMEEntities db = new DBWNHMEEntities();
        // GET: Areas             
        public ActionResult Index(String mensaje)
        {
            ViewBag.mensaje = mensaje;
            ViewBag.Direcciones = new SelectList(db.Direcciones, "IDDireccion", "Descripcion");
            ViewBag.EstadoInventario = new SelectList(db.EstadoInventario, "IDEstadoInventario", "Descripcion");
            return View();

        }
        [ChildActionOnly]
        public ActionResult ListaAreas()
        {
            var Select = from a in db.Areas
                                 where a.EsBodega == true 
                                 select a;
            return PartialView(Select.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDArea,IDDireccion,Descripcion,DescCorta,FechaCrea,FechaModifica,EsBodega,Estado,IDUsuarioCrea,HostCrea,IDUsuarioEdita,HostEdita")] Areas areas)
        {
            if (ModelState.IsValid)
            {
                int IDDireccion = int.Parse(Request.Form["Direcciones"]);
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                var obj = db.SP_AdminAreas(0, IDDireccion, areas.Descripcion, areas.DescCorta, areas.EsBodega, Convert.ToInt32(Session["IDUsuario"]), Session["Host"].ToString(), 0 ,"",1,1, Mensaje);
                return RedirectToAction("Index", new { mensaje = Mensaje.Value.ToString() });
                
            }
            return RedirectToAction("Index", new { mensaje = "" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IDArea,IDDireccion,Descripcion,DescCorta,FechaCrea,FechaModifica,EsBodega,Estado,IDUsuarioCrea,HostCrea,IDUsuarioEdita,HostEdita")] Areas areas)
        {
            if (ModelState.IsValid)
            {
                int IDDireccion = int.Parse(Request.Form["Direcciones"]);
                int IDArea = int.Parse(Request.Form["IDArea"]);
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                var obj = db.SP_AdminAreas(IDArea, IDDireccion, areas.Descripcion, areas.DescCorta, areas.EsBodega, 0, "", Convert.ToInt32(Session["IDUsuario"]), Session["Host"].ToString(),1,2, Mensaje);
                return RedirectToAction("Index", new { mensaje = Mensaje.Value.ToString() });
            }
            return RedirectToAction("Index", new { mensaje = "" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BajaAlta([Bind(Include = "IDArea,IDDireccion,Descripcion,DescCorta,FechaCrea,FechaModifica,EsBodega,Estado,IDUsuarioCrea,HostCrea,IDUsuarioEdita,HostEdita")] Areas areas)
        {
            if (ModelState.IsValid)
            {
                int IDArea = int.Parse(Request.Form["IDArea"]);
                int IDEstadoInventario = int.Parse(Request.Form["EstadoInventario"]);
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                var obj = db.SP_AdminAreas(IDArea, 0, "", "", areas.EsBodega,0 , "", Convert.ToInt32(Session["IDUsuario"]), Session["Host"].ToString(), IDEstadoInventario, 3, Mensaje);
                return RedirectToAction("Index", new { mensaje = Mensaje.Value.ToString() });
            }
            return RedirectToAction("Index", new { mensaje = "" });
        }
    }
}