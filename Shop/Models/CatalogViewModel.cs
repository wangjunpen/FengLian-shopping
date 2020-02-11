using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    /// <summary>
    /// 商品分类视图
    /// </summary>
    /// <returns></returns>
    public class CatalogViewModel
    {
        //品牌表
        public List<Brand> BrandList;
        //类型表
        public List<ProductTypeF> ProductTypeList;
    }
}