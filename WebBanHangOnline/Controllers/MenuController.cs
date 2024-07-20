using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        public ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var item = _dbContext.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", item);
        }
        public ActionResult MenuProductCategory()
        {
            var item = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", item);
        }
        public ActionResult MenuArrivals()
        {
            var item = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuArrivals", item);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateID = id;
            }
            var item = _dbContext.ProductCategories.ToList();
            return PartialView("_MenLeft", item);
        }
    }
}