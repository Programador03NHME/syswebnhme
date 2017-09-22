using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SysWebNHME.Models;

namespace SysWebNHME.Clases
{
    public class SessionFilter: ActionFilterAttribute
    {

        private DBWNHMEEntities db = new DBWNHMEEntities();
        /*propiedad para accesar a la mac del equipo*/
        NetworkInterface[] Cliente = NetworkInterface.GetAllNetworkInterfaces();    

        public static object Request { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Validar la información que se encuentra en la sesión.
            if (HttpContext.Current.Session["IDUsuario"] == null)
            {
                // Si la información es nula, redireccionar a 
                // página de error u otra página deseada.
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                    { "Controller", "Usuarios" },
                    { "Action", "Login" }
                });
            }

            base.OnActionExecuting(filterContext);
        }

        public bool MacValida()
        {
            string MAC = Cliente[0].GetPhysicalAddress().ToString();
            var query = from tbl in db.EquiposMac
                   where tbl.Mac== MAC && tbl.Estado == true
                   select tbl;
            if (query.Count()>0)
            {
                return true;
            }
            return false;
        }
        public string GetPCName()
        {            
            return Dns.GetHostName();
        }
        public string GetMD5(string str)
        {
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(str);
            byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedString;
        }

        public static bool ValidarPermiso(int IDPermiso)
        {
            List<String> ids = HttpContext.Current.Session["Permisos"] != null ? (List<String>)HttpContext.Current.Session["Permisos"] : null;

            if (ids != null)
            {
                foreach (var item in ids)
                {
                    if(IDPermiso.ToString() == item)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}