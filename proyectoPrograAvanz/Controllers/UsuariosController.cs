using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proyectoPrograAvanz.Controllers.DB;
using proyectoPrograAvanz.Models.BD;
using System.Data;
using System.Configuration;
using proyectoPrograAvanz.Models;
using System.Web.Mvc;

namespace proyectoPrograAvanz.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
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

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuarios/Edit/5
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

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
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
        
        public ActionResult RegistrarUsuario(string dato) {
           
           
            ViewBag.clCd = dato;

            return View("RegistrarUsuario");
        }
        [HttpPost]
        public JsonResult guardarInfo( string []datos) {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Clear();
            obj_BD_Model.Dt_Parametros.Rows.Add("@correo", "1", datos[0]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@contra", "1", datos[1]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaCliente", "3", datos[2]);
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["insertarUsu"].ToString();
            obj__BD_Controller.Excute_NonQuery(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "")
            {
                return Json("V");
            }
            else {
                return Json("E");
            }
        }
    }
}
