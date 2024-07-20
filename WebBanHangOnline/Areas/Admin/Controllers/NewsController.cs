using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class NewsController : Controller
    {
        private ApplicationDbContext  _dbContext = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index(string Searchtext,int? page)
        {
            var pagesize = 10;
            if (page == null) page = 1;
            
            IEnumerable<News> item = _dbContext.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {

                item = item.Where(x => x.Alias != null && x.Title != null && (x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext)));
            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex,pagesize);
            ViewBag.page = page;
            ViewBag.pageSize = pagesize;
            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            if(ModelState.IsValid) {
                model.CategoryId = _dbContext.Categories
                                            .Where(x => x.Description == "News")
                                            .Select(x => x.Id)
                                            .FirstOrDefault();
                model.CreatedDate = DateTime.Now;
                model.ModifyedDate = DateTime.Now;
                model.Alias = Models.Common.Filter.FilterChar(model.Title);
                _dbContext.News.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbContext.News.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                model.Alias = Models.Common.Filter.FilterChar(model.Title);
                model.ModifyedDate = DateTime.Now;
                /*_dbContext.News.Attach(model);
                _dbContext.Entry(model).Property(x => x.Title).IsModified = true;
                _dbContext.Entry(model).Property(x => x.Description).IsModified = true;
                _dbContext.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                _dbContext.Entry(model).Property(x => x.Detail).IsModified = true;
                _dbContext.Entry(model).Property(x => x.isActive).IsModified = true;
                _dbContext.Entry(model).Property(x => x.Image).IsModified = true;
                _dbContext.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                _dbContext.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                _dbContext.Entry(model).Property(x => x.ModifierBy).IsModified = true;*/
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.News.Find(id);
            if (item != null)
            {
                _dbContext.News.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.News.Find(id);
            if (item != null)
            {
                item.isActive = !item.isActive;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, IsActive = item.isActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var items = str.Split(',');
                if(items!=null && items.Any())
                {
                    foreach(var item in items)
                    {
                        var obj = _dbContext.News.Find(Convert.ToInt32(item));
                        _dbContext.News.Remove(obj);
                        _dbContext.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}