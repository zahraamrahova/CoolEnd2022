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
                FirstName = "Zahra",
                LastName = "Amrahova",
                Email = "zehra.khudaverdiyeva@gmail.com",
                AuthGroupId = 45,
                Status = true,
                PasswordHash = Crypto.HashPassword("123123")

            };

            _db.Users.Add(admin);
            _db.SaveChanges();
            return Content(admin.Id.ToString());
        }
    }
}