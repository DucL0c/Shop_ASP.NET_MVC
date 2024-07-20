using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class PostsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Posts
        public ActionResult Index(string Searchtext, int? page)
        {
            var pagesize = 10;
            if (page == null) page = 1;

            IEnumerable<Posts> item = _dbContext.Posts.OrderByDescending(x => x.Id);
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
        public ActionResult Add(Posts model)
        {
            if (ModelState.IsValid)
            {
                model.CategoryId = _dbContext.Categories
                                            .Where(x => x.Description == "Posts")
                                            .Select(x => x.Id)
                                            .FirstOrDefault();
                model.CreatedDate = DateTime.Now;
                model.ModifyedDate = DateTime.Now;
                model.Alias = Models.Common.Filter.FilterChar(model.Title);
                _dbContext.Posts.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbContext.Posts.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Posts model)
        {
            if (ModelState.IsValid)
            {
                var existingPost = _dbContext.Posts.Find(model.Id);
                if (existingPost != null)
                {
                    model.Alias = Models.Common.Filter.FilterChar(model.Title);
                    existingPost.Title = model.Title;
                    existingPost.Description = model.Description;
                    existingPost.SeoDescription = model.SeoDescription;
                    existingPost.Detail = model.Detail;
                    existingPost.isActive = model.isActive;
                    existingPost.Image = model.Image;
                    existingPost.SeoKeywords = model.SeoKeywords;
                    existingPost.SeoTitle = model.SeoTitle;
                    existingPost.ModifierBy = null;
                    existingPost.ModifyedDate = DateTime.Now;

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
            var item = _dbContext.Posts.Find(id);
            if (item != null)
            {
                _dbContext.Posts.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.Posts.Find(id);
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
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _dbContext.Posts.Find(Convert.ToInt32(item));
                        _dbContext.Posts.Remove(obj);
                        _dbContext.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}