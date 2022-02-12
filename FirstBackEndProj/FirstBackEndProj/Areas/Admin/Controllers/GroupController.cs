using FirstBackEndProj.DAL;
using FirstBackEndProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstBackEndProj.Areas.Admin.Controllers
{
    public class GroupController : Controller
    {

        private readonly CoolContext _db = new CoolContext();
        // GET: Admin/Group
        public ActionResult Index()
        {

            var model = _db.AuthGroups.Include("Users").ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {

            var model = _db.AuthActions.ToList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Create(AuthGroup group)
        {
            JsonResponse response = new JsonResponse
            {
                Success = false
            };

            if (group == null || string.IsNullOrEmpty(group.Name))
            {
                response.Message = "Group name cannot be empty";
                return Json(response);
            }
            if (group.AuthPermissions == null || group.AuthPermissions.Count == 0)
            {
                response.Message = "At least 1 permission have to choose";
                return Json(response);
            }

            _db.AuthGroups.Add(group);
            _db.SaveChanges();

            response.Success = true;


            return Json(response);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var model = _db.AuthGroups.Include("AuthPermissions").FirstOrDefault(g => g.Id == Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Actions = _db.AuthActions.ToList();
            return View(model);
        }



        [HttpPost]
        public JsonResult Edit(AuthGroup group, AuthPermission[] permissions)
        {

            JsonResponse response = new JsonResponse
            {
                Success = false
            };

            if (_db.AuthGroups.FirstOrDefault(g => g.Name == group.Name && g.Id != group.Id) != null)
            {

                response.Message = "This group is already available";
                return Json(response);
            }

            if (string.IsNullOrEmpty(group.Name))
            {
                response.Message = "Name cannot be empty";
                return Json(response);
            }

            if (permissions == null || permissions.Length == 0)
            {
                response.Message = "Permissions cannot be empty";
                return Json(response);
            }

            var perms = _db.AuthPermissions.Where(p => p.AuthGroupId == group.Id);

            _db.AuthPermissions.RemoveRange(perms);
            _db.AuthPermissions.AddRange(permissions);
            _db.Entry(group).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();


            response.Success = true;
            return Json(response);

        }





        [HttpGet]
        public ActionResult Delete(int Id)
        {

            var grp = _db.AuthGroups.FirstOrDefault(g => g.Id == Id);
            if (grp == null)
            {
                return HttpNotFound();
            }

            if (_db.Users.FirstOrDefault(u => u.AuthGroupId == Id) != null)
            {
                return HttpNotFound();
            }

            _db.AuthGroups.Remove(grp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}