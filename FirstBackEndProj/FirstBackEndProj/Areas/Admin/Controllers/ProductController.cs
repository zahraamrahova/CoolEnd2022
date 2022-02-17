using FirstBackEndProj.DAL;
using FirstBackEndProj.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstBackEndProj.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly CoolContext _db = new CoolContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(_db.Products.Include("Category").Include("Brand").Include("ProductPhotos").ToList());
        }


        public ActionResult Create()
        {
            ViewBag.Categories = _db.Categories.Where(c => c.ParentId == null).ToList();

            ViewBag.Brands = _db.Brands.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ProductPhotos")] Product product, HttpPostedFileBase[] ProductPhotos)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                foreach (var photo in ProductPhotos)
                {
                    string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + photo.FileName;
                    string path = Path.Combine(Server.MapPath("~/Uploads/Products"), photoName);
                    photo.SaveAs(path);
                    ProductPhoto p = new ProductPhoto
                    {
                        Name = photoName,
                        ProductId = product.Id
                    };
                    _db.ProductPhotoes.Add(p);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _db.Categories.Where(c => c.ParentId == null).ToList();
            ViewBag.Brands = _db.Brands.ToList();
            return View(product);

        }

        public ActionResult Delete(int Id)
        {
            var prdct = _db.Products.FirstOrDefault(p => p.Id == Id);
            if (prdct == null)
            {
                return HttpNotFound();
            }

            _db.Products.Remove(prdct);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {

            var model = _db.Products.FirstOrDefault(p => p.Id == Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = _db.Categories.Where(c => c.ParentId == null).ToList();
            ViewBag.Brands = _db.Brands.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "ProductPhotos")] Product product, HttpPostedFileBase[] ProductPhotos)
        {
            if (ProductPhotos != null)
            {
                
                foreach (var photo in ProductPhotos)
                {
                    string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + photo.FileName;
                    string path = Path.Combine(Server.MapPath("~/Uploads/Products"), photoName);
                    photo.SaveAs(path);
                    ProductPhoto p = new ProductPhoto
                    {
                        Name = photoName,
                        ProductId = product.Id
                    };
                    _db.ProductPhotoes.Add(p);
                    _db.SaveChanges();
                }
                var prdct = _db.Products.FirstOrDefault(p => p.Id == product.Id);
                prdct.Name = product.Name;
                prdct.Description = product.Description;
                prdct.Price = product.Price;
                prdct.Discount = product.Discount;
                prdct.BrandId = product.BrandId;
                prdct.CategoryId = product.CategoryId;
                prdct.ProductPhotos = product.ProductPhotos;
                _db.SaveChanges();
            }

            return RedirectToAction("Index");


        }
    }
}
