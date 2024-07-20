using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(string Searchtext, int? page)
        {
            var pagesize = 10;
            if (page == null) page = 1;

            IEnumerable<Product> item = _dbContext.Products.OrderByDescending(x => x.Id);
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
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if(Images != null && Images.Count > 0)
                {
                    for(int i=0; i<Images.Count; i++)
                    {
                        if(i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true,
                                CreatedDate = DateTime.Now,
                                ModifyedDate = DateTime.Now,
                        });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false,
                                CreatedDate = DateTime.Now,
                                ModifyedDate = DateTime.Now,
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifyedDate = DateTime.Now;
                if(string.IsNullOrEmpty(model.SeoTitle)) { model.SeoTitle = model.Title; }
                if(string.IsNullOrEmpty(model.Alias)) { model.Alias = Models.Common.Filter.FilterChar(model.Title); }

                _dbContext.Products.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            var item = _dbContext.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            var productImage = _dbContext.ProductImage.FirstOrDefault(x => x.ProductId == model.Id);
                            if (productImage != null)
                            {
                                // Cập nhật thông tin của hình ảnh tương ứng với id
                                productImage.Image = Images[i];
                                productImage.IsDefault = true;
                                productImage.ModifyedDate = DateTime.Now;
                            }

                        }
                    }
                }
                model.ModifyedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.Alias)) { model.Alias = Models.Common.Filter.FilterChar(model.Title); }
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                _dbContext.Products.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult IsActive(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                item.isActive = !item.isActive;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, IsActive = item.isActive });
            }
            return Json(new { success = false });
        }
        public ActionResult IsNew(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                item.IsFeature = !item.IsFeature;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, IsNew = item.IsFeature });
            }
            return Json(new { success = false });
        }
    }
}