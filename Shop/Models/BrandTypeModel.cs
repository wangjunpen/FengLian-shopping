using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class BrandTypeModel
    {
        public int BrandID { get; set; }
        public int TypeID { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
    }
}