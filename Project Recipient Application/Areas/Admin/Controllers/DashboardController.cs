using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_Recipient_Application.Models;


namespace Project_Recipient_Application.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            int sum = db.projects.Count();
            var q = db.projects.Where(s => s.Studentses.Curse == "نرم افزار").Count();
            var q2 = db.projects.Where(s => s.Studentses.Curse == "IT").Count();
            if (q > 0 && q2 > 0)
            {
                var qdar = q * 100 / sum;
                var q2dar = q2 * 100 / sum;
                string[] Cu = { q.ToString(), qdar.ToString(), q2.ToString(), q2dar.ToString() };
                ViewData["Cu"] = Cu;
            }
            else
            {
                string[] Cu = { "0", "0","0","0" };
                ViewData["Cu"] = Cu;
            }

            return View();
        }

        public ActionResult Singout()
        {
            FormsAuthentication.SignOut();

            return Redirect("/Acount/Index");
        }
    }
}