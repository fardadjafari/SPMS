using CaptchaMvc.HtmlHelpers;
using Project_Recipient_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_Recipient_Application.Controllers
{
    public class AcountController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Acount
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                if (this.IsCaptchaValid(""))
                {
                    string Hash = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");

                    var q = db.Useres.FirstOrDefault(u => u.Username == user.Username && u.Password == Hash);

                    if (q != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, true);

                        if (q.Activeate)
                        {
                            if (q.Role.RoleName == "admin"||q.Role.RoleName=="user")
                            {
                                return Redirect("/Admin/Dashboard/Index");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Errorpass = "شماره تلفن همراه یا کلمه عبور شما اشتباه است";
                    }

                }
                else
                {
                    ViewBag.ErrMessage = "کد امنیتی اشتباه است";
                }
            }
            else
            {
                ModelState.AddModelError("UserPassword", "شماره تلفن همراه یا کلمه عبور شما اشتباه است");
            }

            return View();
        }
    }
}