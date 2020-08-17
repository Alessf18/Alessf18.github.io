using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proyectoPrograAvanz.Controllers.DB;
using proyectoPrograAvanz.Models.BD;
using proyectoPrograAvanz.Helpers;
using System.Data;
using System.Configuration;
using System.Globalization;

using System.Web.Mvc;

namespace proyectoPrograAvanz.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RealizarPedido()
        {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["ListarProductos"].ToString();
            obj_BD_Model.sNombreTabla = ConfigurationManager.AppSettings["tablaProductos"].ToString();
            obj__BD_Controller.Excute_DataAdapter(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "")
            {
                ViewBag.listProductos = obj_BD_Model.Ds.Tables[0].Rows;
            }
            return View();
        }
        public ActionResult CancelarPedido()
        {
            return View();
        }
        // GET: Pedidos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pedidos/Create
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

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedidos/Edit/5
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

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedidos/Delete/5
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
        public JsonResult Guardar_Info(string[] datos, int[] productos)
        {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["ListarRepart"].ToString();
            obj_BD_Model.sNombreTabla = ConfigurationManager.AppSettings["tablaRepartidores"].ToString();
            obj__BD_Controller.Excute_DataAdapter(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "" && obj_BD_Model.Ds.Tables[0].Rows.Count>=1)
            {
                Random numRnd = new Random();
                int numeroRepart = numRnd.Next(0, obj_BD_Model.Ds.Tables[0].Rows.Count-1);
                string cedRepart = obj_BD_Model.Ds.Tables[0].Rows[numeroRepart].ItemArray[0].ToString();
                string nombreRepart = obj_BD_Model.Ds.Tables[0].Rows[numeroRepart].ItemArray[1].ToString();
                obj_BD_Model.sParametro = string.Empty;
                obj_BD_Model.Dt_Parametros = new DataTable();
                obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
                obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
                obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
                obj_BD_Model.Dt_Parametros.Rows.Add("@ubicacion", "1", datos[0]);
                obj_BD_Model.Dt_Parametros.Rows.Add("@fechaEntr", "6", datos[1]);
                obj_BD_Model.Dt_Parametros.Rows.Add("@horaEntr", "5", datos[2]);
                obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaCliente", "1", datos[3]);
                obj_BD_Model.Dt_Parametros.Rows.Add("@estado", "3", "A");
                obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaRepartidor", "1", cedRepart);
                obj_BD_Model.Dt_Parametros.Rows.Add("@precio", "8", datos[4]);
               
                obj_BD_Model.sParametro = ConfigurationManager.AppSettings["InsertarPedido"].ToString();
                obj__BD_Controller.Excute_Scalar(ref obj_BD_Model);
                if (obj_BD_Model.sMsError == "")
                {
                    if (agregarProductos(obj_BD_Model.sValorScalar, productos))
                    {

                        obj_BD_Model.Dt_Parametros.Rows.Clear();
                        obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaRepartidor", "1", cedRepart);
                        obj_BD_Model.sParametro = obj_BD_Model.sParametro = ConfigurationManager.AppSettings["traerInfoRepart"].ToString();
                        obj_BD_Model.sNombreTabla = ConfigurationManager.AppSettings["tablaRepartidores"].ToString();
                        obj__BD_Controller.Excute_DataAdapter(ref obj_BD_Model);
                        if (obj_BD_Model.sMsError == "")
                        {
                            /*Una vez que todo el proceso del pedido se realiza de manera correcta, se envia un correo al repartidor y al cliente*/
                            EmailNotification email = new EmailNotification();
                            string correoRepart = obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[3].ToString();
                            email.notificarAUsuario("Hola " + nombreRepart + " este es un correo  generado  automaticamente, informandole que si le ha asignado un nuevo pedido, dirección de la divienda del cliente: "+datos[0].ToString()+"nombre del cliente:"+Session["NombCl"], correoRepart, "", "Asignacion de pedido # " + obj_BD_Model.sValorScalar);/*envio de correo al repartidor*/
                            string correoCliente = Session["CorrCl"].ToString();
                            email.notificarAUsuario("Hola " + Session["NombCl"] + " este es un correo generado automaticamente , informandole que su pedido se ha realizado de manera correcta, el repartidor: " + nombreRepart + " realizara la entrega, gracias por su compra", Session["CorrCl"].ToString(), "", "Registro de pedido # " + obj_BD_Model.sValorScalar);/*envio de correo al cliente*/

                        }
                        return Json("E");
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
            else
            {
                return Json("FR");
            }
        }

        private bool agregarProductos(string idPedido, int[] idProductos)
        {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            for (int indice = 0; indice < idProductos.Length; indice++)
            {
                obj_BD_Model.Dt_Parametros.Rows.Add("@IdPedido", "2", idPedido);
                obj_BD_Model.Dt_Parametros.Rows.Add("@IdProducto", "2", idProductos[indice]);
                obj_BD_Model.sParametro = ConfigurationManager.AppSettings["insertarProdPedi"].ToString();
                obj__BD_Controller.Excute_NonQuery(ref obj_BD_Model);
                obj_BD_Model.Dt_Parametros.Rows.Clear();
            }
            return true;
        }
        [HttpPost]
        public JsonResult Eliminar_Pedido(string[] datos)
        {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@IdPedido", "2", datos[0]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@estado", "3", 'C');
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["eliminarPedido"].ToString();
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
        public JsonResult RecuperarInfo(string[] datos)
        {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@idPedido", "2", datos[0]);
            obj_BD_Model.Dt_Parametros.Rows.Add("@cedula", "3", datos[1]);
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["recuperarInfoPedido"].ToString();
            obj_BD_Model.sNombreTabla = ConfigurationManager.AppSettings["tablaPedidos"].ToString();
            obj__BD_Controller.Excute_DataAdapter(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "" && obj_BD_Model.Ds.Tables[0].Rows.Count == 1)
            {
                string[] respuesta = new string[5];
                respuesta[0] = obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[0].ToString();
                respuesta[1] = obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[1].ToString();
                respuesta[2] = Convert.ToDateTime(obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[2]).ToString("dd/MM/yyyy");
                respuesta[3] = Convert.ToDateTime(obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[3].ToString(), CultureInfo.CurrentCulture.DateTimeFormat).ToShortTimeString();
                obj_BD_Model.Dt_Parametros.Rows.Clear();
                obj_BD_Model.Dt_Parametros.Rows.Add("@cedulaRepartidor", "3", respuesta[0].ToString());
                obj_BD_Model.sParametro = ConfigurationManager.AppSettings["traerInfoRepart"].ToString();
                obj_BD_Model.sNombreTabla = ConfigurationManager.AppSettings["tablaRepartidores"].ToString();
                obj__BD_Controller.Excute_DataAdapter(ref obj_BD_Model);
                if (obj_BD_Model.sMsError == "" && obj_BD_Model.Ds.Tables[0].Rows.Count == 1)
                {
                    respuesta[4] = obj_BD_Model.Ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    obj_BD_Model.Dt_Parametros.Rows.Clear();

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
        [HttpPost]
        public JsonResult TraerProductosPorPedido(string[] datos)
        {
            BD obj__BD_Controller = new BD();
            Cls_BD obj_BD_Model = new Cls_BD();
            obj_BD_Model.Dt_Parametros = new DataTable();
            obj_BD_Model.Dt_Parametros.Columns.Add("Nombre_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Tipo Dato_Par");
            obj_BD_Model.Dt_Parametros.Columns.Add("Valor_Par");
            obj_BD_Model.Dt_Parametros.Rows.Add("@idPedido", "2", datos[0]);
            obj_BD_Model.sParametro = ConfigurationManager.AppSettings["infoProductoPedido"].ToString();
            obj_BD_Model.sNombreTabla = ConfigurationManager.AppSettings["tablaPedidos"].ToString();
            obj__BD_Controller.Excute_DataAdapter(ref obj_BD_Model);
            if (obj_BD_Model.sMsError == "" && obj_BD_Model.Ds.Tables[0].Rows.Count >= 1)
            {
                obj_BD_Model.Dt_Parametros.Rows.Clear();
                string nombresProductos = "";
                byte numeroProdPorPedido = Convert.ToByte(obj_BD_Model.Ds.Tables[0].Rows.Count);
                for (int indice = 0; indice < numeroProdPorPedido; indice++)
                {
                    obj_BD_Model.Dt_Parametros.Rows.Add("@IdProd", "2", obj_BD_Model.Ds.Tables[0].Rows[indice].ItemArray[0].ToString());
                    obj_BD_Model.sParametro = ConfigurationManager.AppSettings["infoProd"].ToString();
                    obj__BD_Controller.Excute_Scalar(ref obj_BD_Model);
                    if (nombresProductos == "")
                    {
                        nombresProductos = obj_BD_Model.sValorScalar;
                    }
                    else
                    {
                        nombresProductos = nombresProductos + " , " + obj_BD_Model.sValorScalar;
                    }
                    obj_BD_Model.Dt_Parametros.Rows.Clear();
                }
                return Json(nombresProductos);
            }
            else
            {
                return Json("F");
            }

        }

    }
}


