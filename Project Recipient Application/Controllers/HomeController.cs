using Project_Recipient_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Recipient_Application.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            db.projects.ToList();
            return View();
        }
    }
}