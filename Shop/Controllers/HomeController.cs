using System.Linq;
using System.Web.Mvc;
using Shop.Dal;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly SHOPEntities _context = new SHOPEntities();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 商品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult Catalog()
        {
            //商品分类视图
            var model = new CatalogViewModel();
            //获取上级商品与品牌的关联
            var dal = new BrandDal();
            //通过GetList查询所有品牌 绑定到BrandList获取
            model.BrandList = dal.GetList();
            //获取到商品大类
            var pdal = new ProductTypeDal();
            model.ProductTypeList = pdal.GetTypeFList();
            return PartialView(model);
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <returns></returns>
        public ActionResult Navigation()
        {
            //
            var model = new NavViewModel();
            var dal = new BrandDal();
            model.BrandTypeList = dal.GetListByProduct();
            model.BrandList = dal.GetList();
            //男上
            var pdal = new ProductTypeDal();
            model.ProductTypeList = pdal.GetTypeFList();
            return PartialView(model);
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            ViewBag.id = id;
            var pro = _context.Products.FirstOrDefault(r => r.Id == id);
            return View(pro);
        }

        /// <summary>
        /// 品牌活动
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBrandList(int id)
        {
            return View();
        }

        public ActionResult GetProduct(int bid, int tid)
        {
            var list = _context.Products.Where(r => (r.BrandID == bid && bid > 0) || (r.TypeID == tid && tid > 0));
            return View(list);
        }
    }
}
