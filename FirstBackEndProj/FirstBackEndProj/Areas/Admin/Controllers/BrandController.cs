using FirstBackEndProj.DAL;
using FirstBackEndProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstBackEndProj.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        private readonly CoolContext _db = new CoolContext();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            return View(_db.Brands.ToList());
        }
        [HttpGet]
        public ActionResult Create ()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _db.Brands.Add(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete (int Id)
        {
            var brand = _db.Brands.FirstOrDefault(b => b.Id == Id);

            if (brand ==null)
            {
                return HttpNotFound();
            }

            if (brand.Products != null && brand.Products.Count>0)
            {
                return HttpNotFound();
            }

            _db.Brands.Remove(brand);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit (int Id)
        {
            var brand = _db.Brands.FirstOrDefault(b => b.Id == Id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (Brand brand)
        {
            var brnd = _db.Brands.FirstOrDefault(b => b.Id == brand.Id);

            if (brnd==null)
            {
                return HttpNotFound();
            }
            if ((_db.Brands.FirstOrDefault(b => b.Name == brand.Name && b.Id != brand.Id)) != null) {
                ModelState.AddModelError("Name", "There is brand with this name");
                return View(brand);
            }
            if (!ModelState.IsValid)
            {
                return View(brand);
            }
            brnd.Name = brand.Name;
            _db.SaveChanges();
           
            return RedirectToAction("Index");
        }
    }
}