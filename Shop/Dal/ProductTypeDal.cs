using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models;

namespace Shop.Dal
{
    public class ProductTypeDal
    {
        private readonly Models.SHOPEntities _context = new SHOPEntities();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<ProductTypeF> GetTypeFList()
        {
            //获取所有男上女上
            return _context.ProductTypeF.ToList();
        }
    }
}