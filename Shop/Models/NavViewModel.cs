using System.Collections.Generic;

namespace Shop.Models
{
    /// <summary>
    /// 菜单视图
    /// </summary>
    /// <returns></returns>
    public class NavViewModel
    {
        //获取所有品牌
        public List<Brand> BrandList;
        //获取商品男上女上
        public List<ProductTypeF> ProductTypeList;
        //获取商品男上女上中所有的品牌
        public List<BrandTypeModel> BrandTypeList;
    }
}