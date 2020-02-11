using System.Collections.Generic;
using System.Linq;
using Shop.Models;

namespace Shop.Dal
{
    public class BrandDal
    {
        private readonly SHOPEntities _context = new SHOPEntities();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<Brand> GetList()
        {
            //查询所有品牌
            return _context.Brand.ToList();
        }

        /// <summary>
        /// 获取上级商品与品牌的关联
        /// </summary>
        /// <returns></returns>
        public List<BrandTypeModel> GetListByProduct()
        {
            //linq查询
            var list = from p in _context.Products
                       from ps in _context.ProductTypeS
                       from pf in _context.ProductTypeF
                       from b in _context.Brand
                       where p.TypeID == ps.TypeId && ps.SupID == pf.TypeId && p.BrandID == b.BrandId
                       select new
                       {
                           b.BrandId,
                           b.BrandName,
                           pf.TypeId,
                           pf.TypeName
                       };
            return list.Select(r => new BrandTypeModel
            {
                BrandID = r.BrandId,
                BrandName = r.BrandName,
                TypeID = r.TypeId,
                TypeName = r.TypeName
            }).ToList();
        }
    }
}