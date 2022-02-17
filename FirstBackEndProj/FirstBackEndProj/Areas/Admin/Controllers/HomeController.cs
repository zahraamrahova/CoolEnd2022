using FirstBackEndProj.DAL;
using FirstBackEndProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FirstBackEndProj.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {

        private readonly CoolContext _db = new CoolContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateAdmin()
        {

            Models.User admin = new User
            {
                FirstName = "Inji",
                LastName = "Amrah",
                Email = "inji@gmail.com",
                AuthGroupId = 2,
                Status = true,
                PasswordHash = Crypto.HashPassword("123123")

            };

            _db.Users.Add(admin);
            _db.SaveChanges();
            return Content(admin.Id.ToString());
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login (User user)
        {
            if (string.IsNullOrEmpty(user.Email) ||string.IsNullOrEmpty(user.PasswordHash))
            {
                Session["LoginError"] = "Email and Password must be entered!";
                return View();
            }
                var usr=_db.Users.FirstOrDefault(u => u.Email == user.Email);

            if (usr==null)
            {
                if (Crypto.VerifyHashedPassword(usr.PasswordHash, user.PasswordHash))
                {
                    Session["IsLogin"] = true;
                    Session["User"] = usr;
                    return RedirectToAction("Index");
                }
              
            }
            Session["LoginError"] = "Email and password are wrong!";
            return View();


        }

        public ActionResult Logout (int Id)
        {
            Session["IsLogin"] = null;
            Session["User"] = null;

            return RedirectToAction("Index");
        }
    }
}