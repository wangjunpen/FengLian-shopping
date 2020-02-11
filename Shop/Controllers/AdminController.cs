using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Controllers
{
    public class AdminController : Controller
    {
        private readonly SHOPEntities _context = new SHOPEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Brand()
        {
            return View();
        }

        public ActionResult GetBrandList()
        {
            var pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            var pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            var pageSort = Request["sort"] ?? string.Empty;
            var pageOrder = Request["order"] ?? string.Empty;

            var list = from r in _context.Brand
                       select new
                       {
                           r.BrandId,
                           r.BrandName,
                           r.Promoted
                       };

            //记录数
            var total = list.Count();

            //排序
            if (!String.IsNullOrEmpty(pageSort))
            {
                if (String.IsNullOrEmpty(pageOrder) || pageOrder == "asc")
                {
                    switch (pageSort)
                    {
                        case "BrandName":
                            list = list.OrderBy(r => r.BrandName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                            break;
                    }
                }
                else
                {
                    switch (pageSort)
                    {
                        case "BrandName":
                            list = list.OrderByDescending(r => r.BrandName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                            break;
                    }
                }
            }
            else
            {
                list = list.OrderByDescending(r => r.BrandName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            var page = new PageViewModel { rows = list, total = total };

            return Json(page, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBrandList1(int page, int rows, string sort, string order)
        {
            using (SHOPEntities db = new SHOPEntities())
            {
                //查询集合的所有数据            
                IEnumerable<Brand> list2 = db.Brand.AsEnumerable();//缓存，延迟查询                       
                int c = list2.Count();//数据的长度
                //skip跳过某几行   take选择当前集合的前几行
                list2 = list2.Skip((page - 1) * rows).Take(rows);
                //排序
                switch (sort)
                {
                    case "BrandName": list2 = order == "asc" ? list2.OrderBy(x => x.BrandName) : list2.OrderByDescending(x => x.BrandName);
                        break;
                    
                }
                var obj = new
                {
                    total = c,
                    rows = list2.ToArray()
                };
                return Json(obj);
            }

        }

        /// <summary>
        /// 添加品牌 判断是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult AddBrand(Brand info)
        {
            var exists = _context.Brand.Any(r => r.BrandName == info.BrandName);
            if (exists)
                return Content(info.BrandName + "已存在");

            _context.Brand.Add(info);
            _context.SaveChanges();
            return Content("OK");
        }

        /// <summary>
        /// 绑定用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetInfo(int id)
        {
            var info = _context.Brand.FirstOrDefault(r => r.BrandId == id);
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改品牌信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateBrand(Brand info)
        {
            var find = _context.Brand.FirstOrDefault(r => r.BrandId == info.BrandId);

            if (find == null)
                return Content("信息不存在。");

            var exists = _context.Brand.Where(r => r.BrandId != find.BrandId).Any(r => r.BrandName == info.BrandName);
            if (exists)
                return Content(info.BrandName + "已存在");

            find.BrandName = info.BrandName;
            find.Promoted = info.Promoted;
            _context.SaveChanges();

            return Content("OK");
        }

        /// <summary>
        /// 删除品牌信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteBrand(int id)
        {
            var brand = _context.Brand.FirstOrDefault(r => r.BrandId == id);

            if (brand == null)
                return Content("信息不存在。");

            _context.Brand.Remove(brand);
            _context.SaveChanges();

            return Content("OK");
        }

        public ActionResult ProductTypeF()
        {
            return View();
        }

        public ActionResult GetTypeFList()
        {
            var pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            var pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            var pageSort = Request["sort"] ?? string.Empty;
            var pageOrder = Request["order"] ?? string.Empty;

            var list = from r in _context.ProductTypeF
                       select new
                           {
                               r.TypeId,
                               r.TypeName,
                               r.Note
                           };

            //记录数
            var total = list.Count();

            //排序
            if (!String.IsNullOrEmpty(pageSort))
            {
                if (String.IsNullOrEmpty(pageOrder) || pageOrder == "asc")
                {
                    switch (pageSort)
                    {
                        case "TypeName":
                            list = list.OrderBy(r => r.TypeName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                            break;
                    }
                }
                else
                {
                    switch (pageSort)
                    {
                        case "TypeName":
                            list = list.OrderByDescending(r => r.TypeName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                            break;
                    }
                }
            }
            else
            {
                list = list.OrderByDescending(r => r.TypeName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            var page = new PageViewModel { rows = list, total = total };

            return Json(page, JsonRequestBehavior.AllowGet);
        }  

        public ActionResult GetBrandComboboxList()
        {
            var list = from f in _context.Brand
                       orderby f.BrandName
                       select new
                       {
                           f.BrandId,
                           f.BrandName
                       };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTypeComboboxList()
        {
            var list = from f in _context.ProductTypeF
                       from s in _context.ProductTypeS
                       where f.TypeId == s.SupID
                       orderby f.TypeName, s.TypeName
                       select new
                       {
                           SupName = f.TypeName,
                           s.TypeId,
                           s.TypeName
                       };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTypeFComboboxList()
        {
            var list = from r in _context.ProductTypeF
                       orderby r.TypeName
                       select new
                       {
                           r.TypeId,
                           r.TypeName
                       };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTypeSComboboxList()
        {
            var list = from r in _context.ProductTypeS
                       orderby r.TypeName
                       select new
                       {
                           r.TypeId,
                           r.TypeName
                       };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否存在该大类
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult AddTypeF(ProductTypeF info)
        {
            var exists = _context.ProductTypeF.Any(r => r.TypeName == info.TypeName);
            if (exists)
                return Content(info.TypeName + "已存在");

            _context.ProductTypeF.Add(info);
            _context.SaveChanges();
            return Content("OK");
        }

        /// <summary>
        /// 绑定大类数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetTypeFInfo(int id)
        {
            var info = _context.ProductTypeF.FirstOrDefault(r => r.TypeId == id);
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改大类信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateTypeF(ProductTypeF info)
        {
            var find = _context.ProductTypeF.FirstOrDefault(r => r.TypeId == info.TypeId);

            if (find == null)
                return Content("信息不存在。");

            var exists = _context.ProductTypeF.Where(r => r.TypeId != find.TypeId).Any(r => r.TypeName == info.TypeName);
            if (exists)
                return Content(info.TypeName + "已存在");

            find.TypeName = info.TypeName;
            find.Note = info.Note;
            _context.SaveChanges();

            return Content("OK");
        }

        /// <summary>
        /// 删除大类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteTypeF(int id)
        {
            var info = _context.ProductTypeF.FirstOrDefault(r => r.TypeId == id);

            if (info == null)
                return Content("信息不存在。");

            _context.ProductTypeF.Remove(info);
            _context.SaveChanges();

            return Content("OK");
        }

        public ActionResult ProductTypeS()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult GetTypeSList()
        {
            var pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            var pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);

            var list = from s in _context.ProductTypeS
                       from f in _context.ProductTypeF
                       where s.SupID == f.TypeId
                       orderby f.TypeName, s.TypeName
                       select new
                       {
                           s.TypeId,
                           s.TypeName,
                           s.Promoted,
                           s.Note,
                           s.SupID,
                           SupName = f.TypeName
                       };

            //记录数
            var total = list.Count();
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var page = new PageViewModel { rows = list, total = total };

            return Json(page, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductList()
        {
            var pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            var pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);

            var list = from p in _context.Products
                       from t in _context.ProductTypeS
                       from b in _context.Brand
                       where p.TypeID == t.TypeId && p.BrandID == b.BrandId
                       orderby t.TypeName, p.ProName
                       select new
                       {
                           p.Id,
                           p.TypeID,
                           p.BrandID,
                           p.ProName,
                           p.Attribute,
                           p.Description,
                           p.Overview,
                           p.Price,
                           p.Promotion,
                           t.TypeName,
                           b.BrandName
                       };

            //记录数
            var total = list.Count();
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var page = new PageViewModel { rows = list, total = total };

            return Json(page, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加商品小类
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult AddTypeS(ProductTypeS info)
        {
            _context.ProductTypeS.Add(info);
            _context.SaveChanges();
            return Content("OK");
        }

        /// <summary>
        /// 商品
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult AddProduct(Products info)
        {
            _context.Products.Add(info);
            _context.SaveChanges();
            return Content("OK");
        }

        /// <summary>
        /// 绑定小类数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetTypeSInfo(int id)
        {
            var info = _context.ProductTypeS.FirstOrDefault(r => r.TypeId == id);
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 绑定商品数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetProductInfo(int id)
        {
            var info = _context.Products.FirstOrDefault(r => r.Id == id);
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改小类信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateTypeS(ProductTypeS info)
        {
            var find = _context.ProductTypeS.FirstOrDefault(r => r.TypeId == info.TypeId);

            if (find == null)
                return Content("信息不存在。");

            find.TypeName = info.TypeName;
            find.Promoted = info.Promoted;
            _context.SaveChanges();

            return Content("OK");
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateProduct(Products info)
        {
            var find = _context.Products.FirstOrDefault(r => r.Id == info.Id);

            if (find == null)
                return Content("信息不存在。");

            find.ProName = info.ProName;
            find.TypeID = info.TypeID;
            find.BrandID = info.BrandID;
            find.Attribute = info.Attribute;
            find.Description = info.Description;
            find.Overview = info.Overview;
            find.Price = info.Price;
            find.Promotion = info.Promotion;
            _context.SaveChanges();

            return Content("OK");
        }

        /// <summary>
        /// 删除小类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteTypeS(int id)
        {
            var info = _context.ProductTypeS.FirstOrDefault(r => r.TypeId == id);

            if (info == null)
                return Content("信息不存在。");

            _context.ProductTypeS.Remove(info);
            _context.SaveChanges();

            return Content("OK");
        }

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteProduct(int id)
        {
            var info = _context.Products.FirstOrDefault(r => r.Id == id);

            if (info == null)
                return Content("信息不存在。");

            _context.Products.Remove(info);
            _context.SaveChanges();

            return Content("OK");
        }

        public ActionResult UploadImg(int id)
        {
            return View();
        }

        public ActionResult GetImgs(int pid)
        {
            return View();
        }

        public ActionResult GetImgList(int pid)
        {
            var list = from p in _context.ProductImg
                       where p.ProductID == pid
                       select new
                       {
                           p.Id,
                           p.ProductID
                       };

            var total = list.Count();
            var page = new PageViewModel { rows = list, total = total };

            return Json(page, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult GetImg(int id)
        {
            var img = _context.ProductImg
                .FirstOrDefault(r => r.Id == id);
            return img == null ? null : File(img.ImgData, img.ImgMimeType);
        }

        public FileContentResult GetProductImg(int id)
        {
            var img = _context.Products
                .FirstOrDefault(r => r.Id == id);
            return img == null ? null : File(img.ImgData, img.ImgMimeType);
        }

        public ActionResult DeleteImg(int id)
        {
            var img = _context.ProductImg.FirstOrDefault(r => r.Id == id);

            _context.ProductImg.Remove(img);
            _context.SaveChanges();

            return Content("OK");
        }

        public ActionResult Upload(int id, HttpPostedFileBase img)
        {
            if (img == null) return Content("Error");
            var pimg = new ProductImg
            {
                ProductID = id,
                ImgMimeType = img.ContentType,
                ImgData = new byte[img.ContentLength]
            };
            img.InputStream.Read(pimg.ImgData, 0, img.ContentLength);
            _context.ProductImg.Add(pimg);
            _context.SaveChanges();

            return Content("OK");
        }
        public ActionResult UploadProductImg(int id, HttpPostedFileBase img)
        {
            if (img == null) return Content("Error");
            var pro = _context.Products.FirstOrDefault(r => r.Id == id);
            if (pro == null) return Content("Error");

            pro.ImgData = new byte[img.ContentLength];
            img.InputStream.Read(pro.ImgData, 0, img.ContentLength);
            pro.ImgMimeType = img.ContentType;
            _context.SaveChanges();

            return Content("OK");
        }
    }
}
