using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ecommerceED1_2.Models;
using ecommerceED1_2.Utils;

namespace ecommerceED1_2.Models
{
        public class Pedidos
        {
            public string nombreCliente { get; set; }
            public string direccion { get; set; }
            public string nit { get; set; }
            public double totalCancelar { get; set; }

            public void descontarStock(List<Farmacos> listaFarmacos, List<FarmacosPedidos> pedidosFarmacos)
            {
                var stockAntiguo = 0;
                var farmacoACambiar = new Farmacos();
                foreach (var item in pedidosFarmacos)
                {
                    farmacoACambiar = listaFarmacos[item.FarmacoPedido.id - 1];
                    stockAntiguo = farmacoACambiar.existencia;

                    listaFarmacos[item.FarmacoPedido.id - 1].existencia = stockAntiguo - item.cantidadSolicitada;

                }
            }

            public void vaciarPedidos(List<FarmacosPedidos> pedidosFarmacos)
            {
                pedidosFarmacos.Clear();
            }


    }
}