using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_Recipient_Application.Models;

namespace Project_Recipient_Application.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Admin/User
        public ActionResult Index()
        {
            try
            {
                var q = db.Useres.FirstOrDefault(z => z.Username == User.Identity.Name);
                if (q.fullpermision == true)
                {
                    var q1 = db.Useres.ToList();
                    ViewBag.permis = true;
                    return View(q1);
                }
                else
                {
                    var q1 = db.Useres.Where(z => z.fullpermision == false && z.Role.RoleName == q.Role.RoleName).ToList();
                    return View(q1);
                }
            }
            catch (Exception)
            {
                return View();
            }


        }


        // GET: Admin/User/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleTitle");
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(User usr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.Useres.Where(z => z.Username == usr.Username).Count() == 0)
                    {
                        db.Useres.Add(usr);
                         usr.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(usr.Password,"MD5");
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("<script>alert('کاربر با موفقیت ثبت شد'); window.location.href='/Admin/User/Create';</script>"); ;
                        }
                        else
                        {
                            return Content("<script>alert('عملیات ثبت شکست خورد'); window.location.href='/Admin/User/Create';</script>"); ;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "این نام کاربری قبلا استفاده شده است ");
                    }

                }
                ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleTitle", usr.RoleId);
                return View(usr);
            }
            catch
            {
                return Content("<script>alert('عملیات ثبت شکست خورد');  window.location.href='/Admin/User/Create';</script>"); ;
            }
        }


        // GET: Admin/User/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var s = db.Useres.Find(id);
                db.Useres.Remove(s);
                db.SaveChanges();
                return Content("<script>alert('کاربر با موفقیت حذف شد'); window.location.href='/Admin/User/Index';</script>");
            }
            catch (Exception ex)
            {

                return Content("<script>alert('" + ex.Message + "'); window.location.href='/Admin/User/Index';</script>");
            }

        }


    }
}
