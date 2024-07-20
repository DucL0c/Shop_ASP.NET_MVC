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
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index(string Searchtext, int? page)
        {
            var pagesize = 10;
            if (page == null) page = 1;

            IEnumerable<ProductCategory> item = _dbContext.ProductCategories.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {

                item = item.Where(x => x.Alias != null && x.Title != null && (x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext)));
            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pagesize);
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
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifyedDate = DateTime.Now;
                model.Alias = Models.Common.Filter.FilterChar(model.Title);
                _dbContext.ProductCategories.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbContext.ProductCategories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var existingPost = _dbContext.ProductCategories.Find(model.Id);
                if (existingPost != null)
                {
                    existingPost.Title = model.Title;
                    existingPost.Description = model.Description;
                    existingPost.SeoDescription = model.SeoDescription;
                    existingPost.SeoKeywords = model.SeoKeywords;
                    existingPost.SeoTitle = model.SeoTitle;
                    existingPost.ModifierBy = null;
                    existingPost.ModifyedDate = DateTime.Now;
                    existingPost.Alias = Models.Common.Filter.FilterChar(model.Title);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the post with the given ID is not found
                    // You might want to return a specific view or handle it accordingly
                    return HttpNotFound();
                }
            }
            // If ModelState is not valid, return the view with the model
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.ProductCategories.Find(id);
            if (item != null)
            {
                _dbContext.ProductCategories.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}