using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class SizesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Color
        public ActionResult Index(string Searchtext, int? page)
        {
            var pagesize = 10;
            if (page == null) page = 1;

            IEnumerable<Sizes> item = _dbContext.Sizes.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {

                item = item.Where(x => x.Size != null && x.Size != null && (x.Size.Contains(Searchtext) || x.Size.Contains(Searchtext)));
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
        public ActionResult Add(Sizes model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Sizes.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var item = _dbContext.Sizes.Find(id);
            ViewBag.Product = new SelectList(_dbContext.Products.ToList(), "Id", "Title");
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sizes model)
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
            var item = _dbContext.Sizes.Find(id);
            if (item != null)
            {
                _dbContext.Sizes.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}