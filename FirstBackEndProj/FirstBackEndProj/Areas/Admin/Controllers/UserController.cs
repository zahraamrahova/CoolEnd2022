using FirstBackEndProj.DAL;
using FirstBackEndProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FirstBackEndProj.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        private readonly CoolContext _db = new CoolContext();
        // GET: Admin/User
        public ActionResult Index()
        {

            var model = _db.Users.Include("AuthGroup").ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = _db.AuthGroups.ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(User user)
        {

            try
            {
                if (user == null || string.IsNullOrEmpty(user.Email) || user.AuthGroupId == 0)
                {
                    HttpNotFound();
                }

                if (_db.Users.FirstOrDefault(u => u.Email == user.Email) != null)
                {
                    HttpNotFound();
                }
                string token = Crypto.Hash(DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                user.Token = token;
                _db.Users.Add(user);
                _db.SaveChanges();
                SendEmail(user);
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


        }

        public ActionResult Confirm(string token)
        {

            var user = _db.Users.FirstOrDefault(u => u.Token == token);

            if (user == null)
            {

                return HttpNotFound();
            }
            return View(user);

        }
        [HttpPost]
        public ActionResult Confirm(User user)
        {
            var usr = _db.Users.FirstOrDefault(u => u.Id == user.Id);
            usr.FirstName = user.FirstName;
            usr.LastName = user.LastName;
            usr.Status = true;
            usr.PasswordHash = Crypto.HashPassword(user.PasswordHash);
            usr.Token = null;



            try
            {
                _db.SaveChanges();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Index", "Home");
        }
        private void SendEmail(User user)
        {
            string message = "Please click below link for entering CoolEndProject <br> <a href='https://localhost:44315/Admin/User/confirm?token=" + user.Token + "' > Activate Profile! </a>";
            var senderEmail = new MailAddress("myprojectinfo2022@gmail.com", "CoolEnd");
            var receiverEmail = new MailAddress(user.Email, "Receiver");
            var password = "pNzt4W39cgHQwC";
            var sub = "CoolEnd Invitation";
            var body = message;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("myprojectinfo2022@gmail.com", password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body,
                IsBodyHtml = true
            })

            {
                smtp.Send(mess);
            }


        }

        public ActionResult Delete(int Id)
        {
            var usr = _db.Users.FirstOrDefault(u => u.Id == Id);

            if (usr == null)
            {
                return HttpNotFound();
            }

            _db.Users.Remove(usr);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var model = _db.Users.FirstOrDefault(u => u.Id == Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            if (model.Token != null)
            {
                return HttpNotFound();
            }
            ViewBag.Groups = _db.AuthGroups.ToList();
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(User user)
        {
            var usr = _db.Users.FirstOrDefault(u => u.Id == user.Id);
            usr.Status = user.Status;
            usr.AuthGroupId = user.AuthGroupId;

            return RedirectToAction("Index");
        }
    }
}