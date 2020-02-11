using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class BrandViewModel
    {
        public List<Brand> BrandList;
        public Brand CurentBrand { get; set; }
    }
}