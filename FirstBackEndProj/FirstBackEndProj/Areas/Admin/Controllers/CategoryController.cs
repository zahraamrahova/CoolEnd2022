using FirstBackEndProj.DAL;
using FirstBackEndProj.Models;
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
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}