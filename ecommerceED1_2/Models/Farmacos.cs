using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerceED1_2.Models
{
    public class Farmacos
    {
        public int id { get; set; }
        public string nombreFarmaco { get; set; }
        public string descripcionFarmaco { get; set; }
        public string casaProductora { get; set; }
        public double precio { get; set; }
        public int existencia { get; set; }
    }
}