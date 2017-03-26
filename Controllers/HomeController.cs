using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        
        public ActionResult Tutorial1()
        {
            var item = (from n in db.TutorialAcess
                       where n.id == 1
                        select n).FirstOrDefault(); ;
            item.counter += 1;
            db.SaveChanges();

            string file = "~/Content/Tutorial1.pdf";
            return File(file, "application/pdf");            
        }
        public ActionResult Tutorial2()
        {
            var item = (from n in db.TutorialAcess
                        where n.id == 2
                        select n).FirstOrDefault(); ;
            item.counter += 1;
            db.SaveChanges();

            string file = "~/Content/Tutorial2.pdf";
            return File(file, "application/pdf");     
        }
        public ActionResult Tutorial3()
        {
            var item = (from n in db.TutorialAcess
                        where n.id == 3
                        select n).FirstOrDefault(); ;
            item.counter += 1;
            db.SaveChanges();

            string file = "~/Content/Tutorial3.pdf";
            return File(file, "application/pdf");     
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TutorialAccess()
        {
            if (User.Identity.Name == "ADMIN")
            {
                var query = from tuto in db.TutorialAcess
                            where tuto.id > 0
                            select tuto;
                var result = query.ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return HttpNotFound();
        }
        public ActionResult ResetTutorial()
        {
            if (User.Identity.Name == "ADMIN")
            {
                var tutorials = db.TutorialAcess.Where(u => u.id > 0 ).ToList();
                if (tutorials.Count == 0)
                {
                    TutorialAccess t1 = new TutorialAccess();
                    TutorialAccess t2 = new TutorialAccess();
                    TutorialAccess t3 = new TutorialAccess();
                    t1.id = 1;
                    t2.id = 2;
                    t3.id = 3;

                    db.TutorialAcess.Add(t1);
                    db.TutorialAcess.Add(t2);
                    db.TutorialAcess.Add(t3);

                    db.SaveChanges();
                }
                else
                {
                    var tuto = db.TutorialAcess.Where(u => u.id > 0).ToList();
                    tuto.ForEach(t => t.counter= 0);
                }
                db.SaveChanges();
                

                return RedirectToAction("Index","Home");
            }
            else 
                return HttpNotFound();
        }

    }
}