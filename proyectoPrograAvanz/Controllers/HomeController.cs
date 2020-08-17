using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proyectoPrograAvanz.Controllers.DB;
using proyectoPrograAvanz.Models.BD;
using System.Data;
using System.Configuration;
using System.Web.Mvc;

namespace proyectoPrograAvanz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Login() {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult HomePage() {
            return View();
        }
        [HttpPost]
        public JsonResult iniciarSesion(string[] datos)
        {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@correo", "1", datos[0]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@contra", "1", datos[1]);
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["login"].ToString();
            obj__BD_Controller.Excute_Scalar(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == ""&& obj_BD_Model.sValorScalar!="")
            {
                obj_BD_Model.Dt_Parametros.Rows.Clear();
                obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaCliente", "1", obj_BD_Model.sValorScalar);
                Session["IdCl"] = obj_BD_Model.sValorScalar;
                obj_BD_Model.sParametro= ConfigurationManager.AppSettings["ObtenerInfoCl"].ToString();
                obj__BD_Controller.Excute_Scalar(ref obj_BD_Model);
                Session["NombCl"] = obj_BD_Model.sValorScalar;
                Session["CorrCl"] = datos[0];
                return Json("E");
            }
            else
            {
                return Json("F");
            }
        }
        [HttpPost]
        public void CerrarSesion() {
            Session["IdCl"] = null;
            Session["NombCl"] = null;
            Session["CorrCl"] = null;
        }
    }
}