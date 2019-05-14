using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Recipient_Application.Models;

namespace Project_Recipient_Application.Areas.Admin.Controllers
{
    public class StudentController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Admin/Student
        public ActionResult Index()
        {
            return View(db.students.ToList());
        }
        public ActionResult Create()
        {

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                if (!db.students.Any(z => z.stuCode == stu.stuCode))
                {
                    db.students.Add(stu);
                    db.SaveChanges();
                    return Content("<script>alert('دانشجو با موفقیت ثبت شد');</script>");
                }
                else
                {
                    ModelState.AddModelError("stuCode", "کد دانشجویی وارد شده در سیستم موجود می باشد");
                }


            }

            return View(stu);
        }

        public ActionResult Delete(int? id)
        {
            Student s = db.students.Find(id);
            db.students.Remove(s);
            db.SaveChanges();
            return Content("<script>alert('دانشجو با موفقیت حذف شد'); window.location.href='/Admin/Student/Index';</script>");
        }

    }
}