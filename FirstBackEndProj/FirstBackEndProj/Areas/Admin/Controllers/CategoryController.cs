using FirstBackEndProj.DAL;
using FirstBackEndProj.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstBackEndProj.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CoolContext _db = new CoolContext();
        // GET: Admin/Category
        public ActionResult Index()
        {

            return View(_db.Categories.ToList());
        }
        [HttpGet]
        public ActionResult Create ()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();

        }

        [HttpPost]
        public ActionResult Create (Category category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories.ToList();
                return View(category);
            }

            if(category.ParentId==0)
            {
                category.ParentId = null;
            }
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete (int Id)
        {
            var ctgry = _db.Categories.FirstOrDefault(c => c.Id == Id);


            if (ctgry==null)
            {
                return HttpNotFound();
            }

            if (_db.Categories.FirstOrDefault(c=>c.ParentId==Id)!=null)
            {
                return HttpNotFound();
            }

            _db.Categories.Remove(ctgry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit (int Id)
        {

            var model = _db.Categories.FirstOrDefault(c => c.Id == Id);
            if (model==null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = _db.Categories.Where(c=>c.Id!=Id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit (Category category)
        {
            var cat = _db.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            if (_db.Categories.FirstOrDefault(c => c.Name == category.Name && c.Id!=category.Id)!=null)
            {
                ModelState.AddModelError("Name", "There is category with this name");
            }
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            cat.Name = category.Name;
            cat.ParentId = category.ParentId;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public JsonResult GetChildren(int Id)
        {
            JsonResponse response = new JsonResponse {

            Success = false
                };
            var model = _db.Categories.Where(c => c.ParentId == Id);


                if (model!=null) {
                     response.Data=JsonConvert.SerializeObject(model);
                        response.Success=true;

            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}