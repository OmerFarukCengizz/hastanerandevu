using hastanerandevu.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace hastanerandevu.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        hastaneEntities db = new hastaneEntities();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user U)
        {
            if (ModelState.IsValid)
            {
                using (hastaneEntities dc = new hastaneEntities())
                {
                    if (dc.user.Any(x => x.USERTC == U.USERTC))
                    {
                        ViewBag.DuplicateMessage = "Kullanıcı zaten mevcut";
                        return View(U);
                    }
                    U.AILEHID = 2074;
                    dc.user.Add(U);
                    dc.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.Message = "Kaydınız başarıyla tamamlanmıştır";
                }
            }
            return View(U);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user US)
        {
            var checkLogin = db.user.Where(x => x.USERTC.Equals(US.USERTC) && x.USERSİFRE.Equals(US.USERSİFRE)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["USERTCSS"] = US.USERTC.ToString();
                TempData["UserName"] = checkLogin.USERAD;
                FormsAuthentication.SetAuthCookie(US.USERTC, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Yanlış TC veya şifre";
            }
            return View();
        }
        [HttpGet]
        public ActionResult PersonelG()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonelG(personel PS)
        {
            var checkLogin = db.personel.Where(x => x.PUsername.Equals(PS.PUsername) && x.PPasword.Equals(PS.PPasword)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["PUsernameSS"] = PS.PUsername.ToString();
                
                return RedirectToAction("Index", "AdminP");
            }
            else
            {
                ViewBag.Notification = "Yanlış kullanıcı adı veya şifre";
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}
