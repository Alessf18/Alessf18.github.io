using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectoPrograAvanz.Controllers.DB;
using proyectoPrograAvanz.Models.BD;
using System.Data;
using System.Configuration;
namespace proyectoPrograAvanz.Controllers
{
    public class RepartidorController : Controller
    {
        // GET: Repartidor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Repartidor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Repartidor/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult IngresarRepartidor() {
            return View();
        }
        [HttpPost]
        public JsonResult Insertar_Repartidor(string []datos) {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaRepartidor","3",datos[0]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@nombre", "1", datos[1]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@apellidos", "1", datos[2]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@celular", "3", datos[3]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@correo", "1", datos[4]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@estado", "3","A");

            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["insertarRepartidor"].ToString();

            obj__BD_Controller.Excute_NonQuery(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "")
            {
                return Json("E");
            }
            else {
                return Json("F");
            }
        }
        [HttpPost]
        public JsonResult Eliminar_Repartidor(string []datos) {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaRepartidor", "1", datos[0]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@estado", "3", 'I');
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["eliminarRepartidor"].ToString();
            obj__BD_Controller.Excute_NonQuery(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "")
            {
                return Json("V");
            }
            else
            {
                return Json("F");
            }
        }
        [HttpPost]
        public JsonResult RecuperarInfo(string[] datos) {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaRepartidor", "3", datos[0]);
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["recuperarInfo"].ToString();
            obj_BD_Model.sNombreTabla = ConfigurationManager.AppSettings["tablaRepartidores"].ToString();
            obj__BD_Controller.Excute_DataAdapter(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == ""&& obj_BD_Model.Ds.Tables[0].Rows.Count==1)
            {
                string[] respuesta = new string[4];
                respuesta[0] = obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[0].ToString();
                respuesta[1] = obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[1].ToString();
                respuesta[2] = obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[2].ToString();
                respuesta[3] = obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[3].ToString();
                return Json(respuesta);
            }
            else {
                return Json("F");
            }

        }

        [HttpPost]
        public JsonResult EsCorreoValido(string[] datos) {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@correo", "1", datos[0]);
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["verificarCorreoRepart"].ToString();
            obj__BD_Controller.Excute_Scalar(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "" && obj_BD_Model.sValorScalar=="")
            {
                return Json("V");
            }
            else
            {
                return Json("F");
            }
          
        }

        public ActionResult EliminarRepartidor() {
            return View();
        }
        // POST: Repartidor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Repartidor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Repartidor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Repartidor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Repartidor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
