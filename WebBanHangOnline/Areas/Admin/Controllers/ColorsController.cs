using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ColorsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Color
        public ActionResult Index(string Searchtext, int? page)
        {
            var pagesize = 10;
            if (page == null) page = 1;

            IEnumerable<Colors> item = _dbContext.Colors.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {

                item = item.Where(x => x.Color != null && x.Color != null && (x.Color.Contains(Searchtext) || x.Color.Contains(Searchtext)));
            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pagesize);
            ViewBag.page = page;
            ViewBag.pageSize = pagesize;
            ViewBag.Product = new SelectList(_dbContext.Products.OrderBy(p => p.Title).ToList(), "Id", "Title");
            return View(item);
        }
        public ActionResult Add()
        {
            ViewBag.Product = new SelectList(_dbContext.Products.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Colors model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Colors.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var item = _dbContext.Colors.Find(id);
            ViewBag.Product = new SelectList(_dbContext.Products.ToList(), "Id", "Title");
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Colors model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Colors.Find(id);
            if (item != null)
            {
                _dbContext.Colors.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}