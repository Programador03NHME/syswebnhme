using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SysWebNHME.Clases;
using SysWebNHME.Models;

namespace SysWebNHME.Controllers
{
    [SessionFilter]
    public class ArticulosController : Controller
    {

        private DBWNHMEEntities db = new DBWNHMEEntities();
        // GET: Articulos
        public ActionResult Index()
        {
            ViewBag.Familia = new SelectList(db.Familias, "IDFamilia", "Descripcion");
            ViewBag.Laboratorio = new SelectList(db.Laboratorios, "IDLaboratorio", "Nombre");
            ViewBag.UnidadDeMedida = new SelectList(db.UnidadDeMedidas, "IDUnidadDeMedida", "Descripcion");
            ViewBag.Accion = new SelectList(db.AccionArticulos, "IDAccion", "Descripcion");
            ViewBag.Generico = new SelectList(db.GenericoArticulos, "IDGenerico", "Descripcion");
            
            return View();
        }

        public ActionResult ListaArticulos()
        {
            var SelectArt = from a in db.Articulos
                         //where a.EsBodega == true
                         select a;
            return PartialView(SelectArt.ToList());
        }
    }
}