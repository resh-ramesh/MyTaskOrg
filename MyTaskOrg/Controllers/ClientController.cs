using MyTaskOrg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTaskOrg.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult ClientLogin()
        {

            return View();
        }
        public static MyTaskDataEntities1 db = new MyTaskDataEntities1();
        public static string user = "";
        public static string pass = "";
        public ActionResult CLogin(FormCollection form1)
        {
            user = form1["name"];
            pass = form1["pass"];

            ViewBag.message = "";
            ClientTable rec = db.ClientTables.Where(i => i.ClientName == user).FirstOrDefault();
            if (rec == null)
            {
                ViewBag.message = "User Name does not match!";
                return View("ClientLogin");
            }
            else if (rec.password != pass)
            {
                ViewBag.message = "Password does not match!";
                return View("ClientLogin");
            }
            else
            {
                projectTable rec2=db.projectTables.Where(i=>i.ProjectName==rec.ProjectName).FirstOrDefault();
                courseTable rec3 = db.courseTables.Where(i => i.CourseId== rec2.CourseId).FirstOrDefault();
                ViewBag.value = rec.ProjectName;
                ViewBag.course = rec3.CourseName;
                return View();
            }
        }
        public ActionResult CBack()
        {

            return View("CLogin");
        }
        public ActionResult ViewProject()
        {

            return View();
        }
        public ActionResult ViewCourse()
        {

            return View();
        }
    }
}