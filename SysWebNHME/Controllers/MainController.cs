using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SysWebNHME.Clases;
namespace SysWebNHME.Controllers
{
    [SessionFilter]
    public class MainController : Controller
    {
        // GET: Main
        [SessionFilter]
        public ActionResult Index()
        {

            List<String> ids = Session["Permisos"] != null ? (List<String>)Session["Permisos"] : null;

            if (ids != null)
            {
                foreach (var item in ids)
                {
                    //Response.Write(item+"<br/>");
                }
            }
            
            return View();

        }
        public ActionResult Inicio()
        {

            return View();
        }
    }
}