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
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegistrarCliente() {
            return View();
        }
        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
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

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Clientes/Edit/5
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

        // GET: Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Clientes/Delete/5
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
        [HttpPost]
        public JsonResult EsCorreoValido(string correo)
        {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@correo","1",correo);
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["verificarCorreo"].ToString();
            obj__BD_Controller.Excute_Scalar(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "" && obj_BD_Model.sValorScalar=="")
            {
                return Json("V");
            }
            else {
                return Json("F");
            }
        }
        [HttpPost]
        public JsonResult Guardar_Info(string nombre,string apellidos,string cedula,string correo,string contra,string celular,string telCasa) {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@nombre", "1", nombre);
            obj_BD_Model.Dt_Parametros.Rows.Add("@apellidos", "1", apellidos);
            obj_BD_Model.Dt_Parametros.Rows.Add("@celular", "1", celular);
            obj_BD_Model.Dt_Parametros.Rows.Add("@telefCasa", "1", telCasa);
            obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaCliente", "1", cedula);
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["insertarCliente"].ToString();
            obj__BD_Controller.Excute_NonQuery(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "")
            {

                obj_BD_Model.Dt_Parametros.Rows.Clear();
                obj_BD_Model.Dt_Parametros.Rows.Add("@correo", "1", correo);
                obj_BD_Model.Dt_Parametros.Rows.Add("@contra", "1", contra);
                obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaCliente", "3", cedula);
                obj_BD_Model.sParametro = ConfigurationManager.AppSettings["insertarUsu"].ToString();
                obj__BD_Controller.Excute_NonQuery(ref obj_BD_Model);
                if (obj_BD_Model.sMsError == "")
                {
                    string[] respuesta = new string[2];
                    respuesta[0] = "E";
                    respuesta[1] = cedula;
                    return Json(respuesta);
                }
                else
                {
                    return Json("F");
                }
            }
            else
            {
                return Json("F");
            }
            }
            
        }
    }
    

