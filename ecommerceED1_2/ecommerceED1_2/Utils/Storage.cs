using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ecommerceED1_2.Utils;
using ecommerceED1_2.Models;

namespace ecommerceED1_2.Utils
{
    public class Storage
    {
        private static Storage _instance = null;

        public static Storage Instance
        {
            get
            {
                if (_instance == null) _instance = new Storage();
                return _instance;
            }
        }

        public List<Farmacos> listaFarmacos = new List<Farmacos>();
        public List<FarmacosPedidos> pedidosFarmacos = new List<FarmacosPedidos>();
        public double totalACancelar = 0;

    }
}