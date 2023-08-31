using MyTaskOrg.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MyTaskOrg.Controllers
{
    
    public class HomeController : Controller
    {
        public static MyTaskDataEntities1 db = new MyTaskDataEntities1();
        public ActionResult Index()
        {
            
            return View();
        }
       
        public ActionResult Admin()
        {

            return View();
        }
        public ActionResult Client()
        {

            return View();
        }
        public static string user = "";
        public static string pass = "";
        public ActionResult Login(FormCollection form1)
        {
            user = form1["name"];
            pass = form1["pass"];

            ViewBag.message = "";
            adminTable rec = db.adminTables.Where(i => i.name == user).FirstOrDefault();
            if (rec == null)
            {
                ViewBag.message = "User Name does not match!";
                return View("Index");
            }
            else if (rec.password != pass)
            {
                ViewBag.message = "Password does not match!";
                return View("Index");
            }
            else 
         
            return View();
        }
        public ActionResult ProjectDetails()
        {
            return View();
        }
        public ActionResult ProjectView()
        {

            
            return View();
        }
        public ActionResult Back()
        {


            return View("Login");
        }
        public ActionResult ProjectAdd()
        {
            List<SelectListItem> List = new List<SelectListItem> { };
            foreach (courseTable s in db.courseTables)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = s.CourseName,
                    Value = s.CourseId.ToString(),

                };
                List.Add(item);
            }
            ViewBag.listitem = List;
            TempData["lev"] = List;
            return View();
        }
        public ActionResult ProjectDelete()
        {
            List<SelectListItem> List = new List<SelectListItem> { };
            foreach (projectTable s in db.projectTables)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = s.ProjectName,
                    Value = s.ProjectId.ToString(),

                };
                List.Add(item);
            }
            ViewBag.listitem = List;
            return View();
        }
        public ActionResult Pdelete(FormCollection form)
        {
            string sel = form["ListItem"];

            int n = Convert.ToInt32(sel);
            projectTable pt = db.projectTables.Where(i => i.ProjectId == n).FirstOrDefault();
            db.projectTables.Remove(pt);
            db.SaveChanges();
            ViewBag.message = "Record Deleted";

            return View();
        }
        public ActionResult ProjectEdit()
        {
            List<SelectListItem> List = new List<SelectListItem> { };
            foreach (projectTable s in db.projectTables)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = s.ProjectName,
                    Value = s.ProjectId.ToString(),

                };
                List.Add(item);
            }
            ViewBag.listitem = List;
            return View();
        }
        public ActionResult Pedit(FormCollection form)
        {
            string selected = form["ListItem"];

            int n = Convert.ToInt32(selected);
            projectTable pt = db.projectTables.Where(i => i.ProjectId== n).FirstOrDefault();

            ViewBag.Id = pt.ProjectId;
            ViewBag.name = pt.ProjectName;
            ViewBag.course = pt.CourseId;
            List<SelectListItem> List = new List<SelectListItem> { };
            foreach (courseTable s in db.courseTables)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = s.CourseName,
                    Value = s.CourseId.ToString(),

                };
                List.Add(item);
            }
            //ViewBag.listitem = List;
            TempData["lev"] = List;

            return View();
        }
        public ActionResult Updatedone(FormCollection form)
        {

            int pid = Convert.ToInt32(form["text1"]);
            string name = form["text2"];
            string sel = form["courses"];

            int n = Convert.ToInt32(sel);
            projectTable pt = db.projectTables.Where(i => i.ProjectId == pid).FirstOrDefault();
            pt.ProjectName = name;
            pt.CourseId= n;
             db.Entry(pt).State = System.Data.Entity.EntityState.Modified;
             db.SaveChanges();
            return View();
           
        }
        public ActionResult ClientDetails()
        {
            return View();
        }
        public ActionResult ClientAdd()
        {
            List<SelectListItem> List = new List<SelectListItem> { };
            foreach (projectTable s in db.projectTables)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = s.ProjectName,
                    Value = s.ProjectId.ToString(),

                };
                List.Add(item);
            }
            ViewBag.listitem = List;
            TempData["lev"] = List;
            return View();
        }
        public ActionResult ClientInsert(FormCollection form1)
        {
            int cn = Convert.ToInt32(form1["projects"]);
            projectTable pt = db.projectTables.Where(i => i.ProjectId == cn).FirstOrDefault();
            string pname = pt.ProjectName;
            string name = form1["pn"];
            string pass = form1["pass"];
            ClientTable rec = new ClientTable();
            rec.ClientName = name;
            rec.password = pass;
            rec.ProjectName = pname;

            db.ClientTables.Add(rec);
            db.SaveChanges();
            ViewBag.message = "value inserted";
            return View();
        }
        public ActionResult ClientView()
        {
            
            return View();
        }
        public ActionResult Insert(FormCollection form1)
        {


            int cn = Convert.ToInt32(form1["courses"]);
            string name = form1["pn"];
            //  string name = "project1";
            //int pid = 5;
            projectTable rec = new projectTable();
            rec.ProjectName = name;
            rec.CourseId = cn;

            db.projectTables.Add(rec);
            db.SaveChanges();
            ViewBag.message = "value inserted";
            return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult list()
        {
            ViewBag.Message = "Your application description page.";
            var model = db.courseTables.ToList();
            return View(model);
            
        }
        public ActionResult AddNew()
        {
           
            return View();

        }
        public ActionResult Savecourse(FormCollection f1) {
            courseTable cs = new courseTable();
            cs.CourseName = f1["t1"];
            
            db.courseTables.Add(cs);
            db.SaveChanges();
            return RedirectToAction("list");

        }
        public JsonResult Delete(string id)
        {
            int cid = Convert.ToInt32(id);


            courseTable courses = db.courseTables.Where(i => i.CourseId== cid).FirstOrDefault();
            db.courseTables.Remove(courses);
            db.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.courseTables.Find(id);
             return View(model);
        }
        public ActionResult Edit(courseTable course)
        {
            
            courseTable cs = db.courseTables.Where(i => i.CourseId == course.CourseId).FirstOrDefault();
            cs.CourseId = course.CourseId;
            cs.CourseName = course.CourseName;
            
            db.SaveChanges();

            return RedirectToAction("list");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
            }
            return View("About");
        }
       
    }
}