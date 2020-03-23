using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ecommerceED1_2.Utils;
using ecommerceED1_2.Models;
namespace ecommerceED1_2.Models
{
    public class paginacionFarmacos
    {
        public IEnumerable<Farmacos> farmacos { get; set; }
        public int BlogPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(farmacos.Count() / (double)BlogPerPage));
        }
        public IEnumerable<Farmacos> PaginatedBlogs()
        {
            int start = (CurrentPage - 1) * BlogPerPage;
            return farmacos.Skip(start).Take(BlogPerPage);
        }
    }
}