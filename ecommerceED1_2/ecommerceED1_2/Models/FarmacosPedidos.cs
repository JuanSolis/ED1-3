using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ecommerceED1_2.Models;

namespace ecommerceED1_2.Models
{
    public class FarmacosPedidos:IPedidos
    {
        public Farmacos FarmacoPedido { get; set; }
        public int cantidadSolicitada { get; set; }

        public double calcularTotal()
        {
            return this.FarmacoPedido.precio * this.cantidadSolicitada;
        }
    }
}