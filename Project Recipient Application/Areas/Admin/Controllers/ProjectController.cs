using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Recipient_Application.Models;

namespace Project_Recipient_Application.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Admin/Project
        public ActionResult search(string strsearch)
        {

            var query = db.projects.OrderByDescending(i=>i.DateSend).Take(5);
            if (!String.IsNullOrEmpty(strsearch))
            {
                query = db.projects.Where(z => z.Studentses.stuCode == strsearch);
            }

            return View(query.ToList());
        }

       
        // GET: Admin/Project/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var q = db.projects.SingleOrDefault(z => z.ID == id);
                db.projects.Remove(q);
                db.SaveChanges();
                return Content("<script>alert('پروژه با موفقیت حذف شد'); window.location.href='/Admin/Project/search';</script>");
            }
            catch 
            {
                return Content("<script>alert('حذف پروژه با شکست روبرو شد'); window.location.href='/Admin/Project/search';</script>");
            }
        }


    }
}
