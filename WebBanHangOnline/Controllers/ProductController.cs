using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext dbContext =  new ApplicationDbContext();
        // GET: Product
        public ActionResult Index(int? id)
        {
            var items = dbContext.Products.ToList();
            if (id != null)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();   
            }
            return View(items);
        }
        public ActionResult CategoryProduct(int? id)
        {
            var items = dbContext.Products.ToList();
            if (id != null)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            ViewBag.CateID = id;
            return View(items);
        }
        public ActionResult Detail(string alias, int id)
        {
            var items = dbContext.Products.Find(id);
            var sizes = dbContext.Sizes.Where(ps => ps.ProductId == id).Select(ps => ps.Size).ToList();
            var colors = dbContext.Colors.Where(pc => pc.ProductId == id).Select(pc => pc.Color).ToList();

            ViewBag.Sizes = sizes;
            ViewBag.Colors = colors;
            return View(items);
        }
        public ActionResult Partial_ItemsByCateID()
        {
            var items = dbContext.Products.Where(x => x.isActive).Take(11).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSale()
        {
            var items = dbContext.Products.Where(x => x.isActive).Take(11).ToList();
            return PartialView(items);
        }
    }
}