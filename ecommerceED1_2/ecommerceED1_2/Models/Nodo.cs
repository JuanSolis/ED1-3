using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ecommerceED1_2.Models;
using ecommerceED1_2.Interfaz;

namespace ecommerceED1_2
{
    // Nodo con Tipos T y U que en un futuro seran String e Int
    public class Nodo<T, U> : IComparable<T>
    {
        public T indice { get; set; }
        public U valor { get; set; }
        public Nodo<T, U> izquierdo { get; set; }
        public Nodo<T, U> derecho { get; set; }

        private ComparadorNodosDelegate<T> comparar;
        public Nodo(T _indice, U _value, ComparadorNodosDelegate<T> _comparador)
        {
            this.indice = _indice;
            this.valor = _value;
            this.izquierdo = null;
            this.derecho = null;
            comparar = _comparador;
        }
        public int CompareTo(T other)
        {
            return comparar(indice, other);
        }
    }
}