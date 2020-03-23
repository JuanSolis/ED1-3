using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerceED1_2.Models
{
    // Nodo con Tipos T y U que en un futuro seran String e Int
    public class Nodo<T,U>
    {
        public T nomFarmaco { get; set; }
        public U lineaArchivo { get; set; }
        public Nodo<T,U> nodoIzq { get; set; }
        public Nodo<T, U> nodoDer { get; set; }

        public Nodo()
        {

        }

    }
}