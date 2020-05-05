using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ecommerceED1_2.Models;
using ecommerceED1_2.Utils;


namespace ecommerceED1_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            
            var fileName = string.Empty;
            var path = string.Empty;
            if (file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }

            // Leer el archivo subido
            reader myReader = new reader(Server.MapPath("~/App_Data/uploads/" + fileName));


            // Recorre la lista de farmacos y agrega los nodos con nombre y linea al arbolAVL.
            foreach(var Item in Storage.Instance.listaFarmacos)
            {
                Nodo<string, int> nodo = new Nodo<string, int>(Item.nombreFarmaco, (Item.id), null);

                Storage.Instance.arbolAVL.Add(nodo);
            }
           
            
            return RedirectToAction("Farmacos");
        }



        public ActionResult Farmacos(int page = 1)
        {
            // Paginacion, 35 elementos por pagina
            var paginacionFarmacos = new paginacionFarmacos
            {
                BlogPerPage = 35,
                farmacos = Storage.Instance.listaFarmacos,
                CurrentPage = page
            };
            return View(paginacionFarmacos);
        }

        [HttpPost]
        public ActionResult Farmacos(string id, FormCollection collection, int page)
        {

            int idFarmaco = Convert.ToInt32(id);
            int cantidad = int.Parse(collection[("CantidadFarmaco_" + id)]);

            // Obteniene la informacion del farmaco agregado
            Farmacos farmacoAgregado = new Farmacos
            {
                id = Storage.Instance.listaFarmacos[(idFarmaco - 1)].id,
                nombreFarmaco = Storage.Instance.listaFarmacos[(idFarmaco - 1)].nombreFarmaco,
                descripcionFarmaco = Storage.Instance.listaFarmacos[(idFarmaco - 1)].descripcionFarmaco,
                casaProductora = Storage.Instance.listaFarmacos[(idFarmaco - 1)].casaProductora,
                precio = Storage.Instance.listaFarmacos[(idFarmaco - 1)].precio,
                existencia = Storage.Instance.listaFarmacos[(idFarmaco - 1)].existencia,
            };

            FarmacosPedidos farmacoPedido = new FarmacosPedidos
            {
                FarmacoPedido = farmacoAgregado,
                cantidadSolicitada = cantidad
            };

            // Y lo adjunta al carrito 
            Storage.Instance.totalACancelar += farmacoPedido.calcularTotal();
            Storage.Instance.pedidosFarmacos.Add(farmacoPedido);

            var paginacionFarmacos = new paginacionFarmacos
            {
                BlogPerPage = 35,
                farmacos = Storage.Instance.listaFarmacos,
                CurrentPage = page
            };
            return View(paginacionFarmacos);
        }

        [HttpGet]
        public ActionResult EditFarmacos(string id)
        {
            // Obtiene el id del farmaco por la url y manda el farmaco como modelo
            int _id = int.Parse(id);
            return View(Storage.Instance.listaFarmacos[(_id - 1)]);
        }

        [HttpPost]
        public ActionResult EditFarmacos(string id, FormCollection collection)
        {
            //Edita el farmaco y redirecciona a la vista de Farmacos
            int _id = int.Parse(id);
            int nuevaExistencia = int.Parse(collection["CantidadFarmaco"]);
            Storage.Instance.listaFarmacos[(_id - 1)].existencia = nuevaExistencia;
            return RedirectToAction("Farmacos");
        }

        [HttpGet]
        public ActionResult busquedaFarmacos(int id)
        {
            // Lee el id por la url y lo manda el farmaco como modelo 
            return View(Storage.Instance.listaFarmacos[(id - 1)]);
        }

        [HttpPost]
        public ActionResult busquedaFarmacos(FormCollection collection)
        {
            // Lee la informacion del FormCollection y redirecciona a la vista de busquedaFarmac
            string _nombreFarmaco = collection["farmacoBuscado"];

            // Realiza la busqueda en el arbolAVL y devuelve el id que es la linea donde esta ubicado en el archivo
            int _id = Storage.Instance.arbolAVL.Search(_nombreFarmaco);
            return Redirect("/Home/busquedaFarmacos/" + _id);
        }

        public ActionResult Pedidos()
        {
            // Manda como modelo la lista de farmacos agregados al carrito
            return View(Storage.Instance.pedidosFarmacos);
        }

        [HttpPost]
        public ActionResult Pedidos(FormCollection collection)
        {

            // Obtiene la informacion en el "CheckOut"
            var pedido = new Pedidos
            {
                nombreCliente = collection["Nombre"],
                direccion = collection["Direccion"],
                nit = collection["Nit"],
            };

            //Descuenta del stock los farmacos "Comprados"
            pedido.descontarStock(Storage.Instance.listaFarmacos, Storage.Instance.pedidosFarmacos);
            pedido.vaciarPedidos(Storage.Instance.pedidosFarmacos);
            Storage.Instance.totalACancelar = 0;
            return View(Storage.Instance.pedidosFarmacos);
        }
    }
}