using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication.Filters;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Projects/
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult PublicProjects()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult LoadPublicProjects(string searchKey, string campo, string orderBy, int index)
        {
            var range = 4;
            if (searchKey == null || searchKey == "")
            {
                if (orderBy == "CreatedOn")
                {
                    var query = from p in db.Projects
                                where p.Public
                                orderby p.CreatedOn
                                select new ProjectListViewModel() { Name = p.Name, ProjectId = p.ProjectId, Description = p.Description };
                    var result = query.ToList();
                    return Json(result.Skip(index).Take(range), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var query = from p in db.Projects
                                where p.Public
                                orderby p.LastSaved
                                select new ProjectListViewModel() { Name = p.Name, ProjectId = p.ProjectId, Description = p.Description };
                    var result = query.ToList();
                    return Json(result.Skip(index).Take(range), JsonRequestBehavior.AllowGet);
                }
                //return PartialView(result);
            }
            else
            {
                if (campo == "UserName")
                {
                    if (orderBy == "CreatedOn")
                    {
                        var query = from up in db.User_Projs
                                    join p in db.Projects on up.ProjectId equals p.ProjectId
                                    join users in db.Users on up.UserId equals users.Id
                                    orderby p.CreatedOn
                                    where users.UserName == searchKey & p.Public
                                    select new ProjectListViewModel() { Name = p.Name, ProjectId = p.ProjectId };
                        var result = query.ToList();
                        return Json(result.Skip(index).Take(range), JsonRequestBehavior.AllowGet);
                       
                    }
                    else
                    {
                        var query = from up in db.User_Projs
                                    join p in db.Projects on up.ProjectId equals p.ProjectId
                                    join users in db.Users on up.UserId equals users.Id
                                    orderby p.LastSaved
                                    where users.UserName == searchKey & p.Public
                                    select new ProjectListViewModel() { Name = p.Name, ProjectId = p.ProjectId };
                        var result = query.ToList();
                        return Json(result.Skip(index).Take(range), JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    if (orderBy == "CreatedOn")
                    {
                        IEnumerable<object> query;
                        query = from p in db.Projects
                                orderby p.CreatedOn
                                where p.Public & (p.Name == searchKey)
                                select new ProjectListViewModel() { Name = p.Name, ProjectId = p.ProjectId, Description = p.Description };
                        var result = query.ToList();
                        return Json(result.Skip(index).Take(range), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        IEnumerable<object> query;
                        query = from p in db.Projects
                                orderby p.LastSaved
                                where p.Public & (p.Name == searchKey)
                                select new ProjectListViewModel() { Name = p.Name, ProjectId = p.ProjectId, Description = p.Description };
                        var result = query.ToList();
                        return Json(result.Skip(index).Take(range), JsonRequestBehavior.AllowGet);
                    }


                }
            }
        }
        public JsonResult VerifyCopyProject(int? id, int? userId)
        {
            var userID = User.Identity.GetUserId(); 
            try
            {
                var project = db.Projects.Where(i => i.ProjectId == id).ToList();
                var p2 = project[0];

                var query = from p in db.Projects
                            join up in db.User_Projs on p.ProjectId equals up.ProjectId
                            join u in db.Users on up.UserId equals u.Id
                            where p.Name == p2.Name & u.Id == userID
                            select new ProjectListViewModel() { Name = p.Name };
                var result = query.DefaultIfEmpty().ToList();
                if (result[0] != null)
                {
                    return Json(new { answer = true });
                }
                else
                {
                    return Json(new { answer = false });
                }
            }
            catch (Exception e) { return Json(new { answer = false }); }

        }
        public ActionResult CopyProject(int? id)
        {
            try { 
            var project = db.Projects.Where(i => i.ProjectId == id).ToList();
            var p2 = project[0];
            Project p = new Project();
            p.Name = p2.Name;
            p.BlocklyData = p2.BlocklyData;
            p.BlocklyCode = p2.BlocklyCode;
            p.CreatedOn = DateTime.Now;
            p.Description = p2.Description;
            p.Public = false;

            User_Proj up = new User_Proj();
            up.ProjectId = p.ProjectId;
            up.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Projects.Add(p);
                db.User_Projs.Add(up);
                db.SaveChanges();
            }
            }
            catch (Exception e) { return RedirectToAction("AccessDenied", "Error"); }

            return Json(new {});
        }

        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult Blockly(int id, bool isEditable)
        {
            ViewBag.isEditable = isEditable;
            ProjectConfigViewModel pcvm = new ProjectConfigViewModel();
            pcvm.ID = id;
            return View(pcvm);
        }
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BlocklyJavaScript(int? id)
        {
            var code = db.Projects.Where(i => i.ProjectId == id).ToList();
            return Json(code[0].BlocklyCode, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult PublicProjectData(int? id)
        {
            ViewBag.isEditable = false;
           // ViewBag.ID = id;

            var query = from p in db.Projects
                        join up in db.User_Projs on p.ProjectId equals up.ProjectId
                        join u in db.Users on up.UserId equals u.Id
                        where p.ProjectId == id
                        select new ProjectPublicView() { ProjectName = p.Name, ProjectID = p.ProjectId, ProjectDescription = p.Description, UserName=u.UserName};
            

        
            return View(query.First());
        }
         //GET: /Projects/LoadProjectData/
        [AuthorizeLoadProjectAccess]
        public ActionResult LoadProjectData(int? id, bool isEditable)
        {
            ViewBag.isEditable = isEditable;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /* var project = db.Projects.Where(i => i.ID == id).ToList();
            if (project == null)
            {
                return HttpNotFound();
            }*/

            ViewBag.ID = id;
            return PartialView();
        }
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadProjectBlocklyJson(int? id)
        {
            var project = db.Projects.Where(i => i.ProjectId == id).ToList();           
            var myData = new { BlocklyData = project[0].BlocklyData };
            return Json(myData, JsonRequestBehavior.AllowGet);
        }

        // GET: /Projects/LoadProjectConfig/
        [AuthorizeProjectAccess]
        public ActionResult LoadProjectConfig(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*
            var query = from  up in db.User_Projs
                        join p in db.Projects on up.ProjectId equals p.ProjectId 
                        join sp in db.SharedProjects.DefaultIfEmpty() on p.ProjectId equals sp.ProjectId into group1
                        from sp in group1.DefaultIfEmpty()
                        where up.UserId == temp
                        select new ProjectListViewModel(){ Name = p.Name, ProjectId = p.ProjectId, Verified = sp==null? true:false };
            */
            var query = from p in db.Projects
                        join sp in db.SharedProjects on p.ProjectId equals sp.ProjectId into group1
                        from g1 in group1.DefaultIfEmpty()
                        join u in db.Users on g1.SharedById equals u.Id into group2
                        from g2 in group2.DefaultIfEmpty()
                        where p.ProjectId == id
                        select new ProjectConfigViewModel() { Name = p.Name, ID = p.ProjectId, Description = p.Description, Date = p.CreatedOn, Public = p.Public, SharedBy = (g2 == null ? String.Empty : g2.Name) };

            var project = query.ToList();

            if (project == null)
            {
                return HttpNotFound();
            }

            ProjectConfigViewModel project2 = project[0];
            /* project2.ID = project[0].ID;
            project2.Name = project[0].Name;
            project2.Description = project[0].Description;
            project2.Date = project[0].Date;
            project2.Public = project[0].Public;
            */


            return PartialView(project2);
        }

        public ActionResult DialogVerifyShared(string type)
        {
            ViewBag.type = type;
            string temp = User.Identity.GetUserId();

            var query = from p in db.Projects
                        join sp in db.SharedProjects on p.ProjectId equals sp.ProjectId
                        join u in db.Users on sp.SharedById equals u.Id
                        where sp.SharedToId == temp & sp.Verified == false
                        select new ProjectConfigViewModel() { Name = p.Name, ID = p.ProjectId, Description = p.Description, Date = p.CreatedOn, SharedBy = u.UserName };

            var result = query.ToList();

            if (result.Count() == 0)
            {
                if (type == "Manage")
                {
                    return RedirectToAction("Manage");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            return View(result);
        }

        // POST: /Projects/VerifyShared/
        [HttpPost]
        public ActionResult VerifyShared(List<ProjectCheckedListViewModel> data)
        {
            var user = User.Identity.GetUserId();
            foreach (var item in data)
            {
                if (item.Checked)
                {
                    User_Proj up = new User_Proj();
                    up.ProjectId = item.ProjectId;
                    up.UserId = User.Identity.GetUserId();
                    db.User_Projs.Add(up);

                    var temp = db.SharedProjects.Where(i => i.SharedToId == user & i.ProjectId == item.ProjectId).ToList();
                    SharedProject sp = temp[0];
                    sp.Verified = true;
                }
                else
                {
                    var temp = db.SharedProjects.Where(i => i.SharedToId == user & i.ProjectId == item.ProjectId).ToList();
                    SharedProject sp = temp[0];
                    db.SharedProjects.Remove(sp);
                }
            }
            db.SaveChanges();
            return Json(true);
        }

        public ActionResult LoadProjectList(string listType)
        {
            ViewBag.listType = listType;
            string temp = User.Identity.GetUserId();

            var query = from up in db.User_Projs
                        join p in db.Projects on up.ProjectId equals p.ProjectId
                        where up.UserId == temp
                        select new ProjectListViewModel() { Name = p.Name, ProjectId = p.ProjectId };
            /*
            var query = from  up in db.User_Projs
                        join p in db.Projects on up.ProjectId equals p.ProjectId 
                        join sp in db.SharedProjects.DefaultIfEmpty() on p.ProjectId equals sp.ProjectId into group1
                        from sp in group1.DefaultIfEmpty()
                        where up.UserId == temp
                        select new ProjectListViewModel(){ Name = p.Name, ProjectId = p.ProjectId, Verified = sp==null? true:false };
            */
            var result = query.ToList();
            return PartialView(result);
        }

        // POST: /Projects/SaveProjectConfig/
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveProjectConfig(ProjectConfigViewModel projectConfig)
        {
            if (ModelState.IsValid)
            {
                if (projectConfig == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Project project = db.Projects.Find(projectConfig.ID);
                if (project == null)
                {
                    return HttpNotFound();
                }
                project.Name = projectConfig.Name;
                project.Description = projectConfig.Description;
                project.Public = projectConfig.Public;

                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("LoadProjectList", "Projects", "manage");
                return Json("Operação realizada com sucesso!");
            }
            string messages = string.Join(" ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            return Json(messages);

        }
        
        // POST: /Projects/SaveData/
       

        [HttpPost]
        public ActionResult SaveProjectData(ProjectDataViewModel projectData)
        {
            if (projectData == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.Find(projectData.ID);
            if (project == null)
            {
                return HttpNotFound();
            }
            project.BlocklyData = projectData.BlocklyData;
            project.BlocklyCode = projectData.BlocklyCode;
            project.LastSaved = DateTime.Now;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true });
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        // GET: /Projects/ShareProject
        public ActionResult ShareProject(ProjectConfigViewModel projectConfig)
        {
            //Verifica se o usuario existe.
            //Da insert na tabela projeto com userdID SharedTo.ID, com SharedBy this user e Verified = false.
            var users = db.Users.Where(i => i.UserName == projectConfig.ShareTo).ToList();
            if (users.Count() == 0)
                return Json("Usuario não existente no sistema.");

            //Verifica se o convite já foi enviado para esse usuário.
            var temp2 = users[0].Id;
            var temp = db.SharedProjects.Where(i => i.SharedToId == temp2 & i.ProjectId == projectConfig.ID).ToList();
            if (temp.Count() != 0)
                return Json("Convite de projeto já enviado para este usuário.");

            //Verifica se o usuario SharedTo ja possui esse projeto na sua lista de projetos.
            var temp3 = db.User_Projs.Where(i => i.UserId == temp2 & i.ProjectId == projectConfig.ID).ToList();
            if (temp3.Count() != 0)
                return Json("Usuário já está compartilhando esse projeto");

            SharedProject sp = new SharedProject();
            sp.ProjectId = projectConfig.ID;
            sp.SharedById = User.Identity.GetUserId();
            sp.SharedToId = users[0].Id;
            sp.Verified = false;
            

            if (ModelState.IsValid)
            {
                db.SharedProjects.Add(sp);
                db.SaveChanges();
                return Json("Convite enviado com sucesso.");
            }

            return Json("Ops, ocorreu um erro.");
        }


        public ActionResult ShareProject(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Where(i => i.ProjectId == id).ToList();
            if (project == null)
            {
                return HttpNotFound();
            }

            ProjectConfigViewModel project2 = new ProjectConfigViewModel();
            project2.ID = project[0].ProjectId;
            project2.Name = project[0].Name;
            project2.Description = project[0].Description;
            project2.Date = project[0].CreatedOn;
            project2.Public = project[0].Public;

            return View(project2);
        }
        
        // GET: /Projects/Create
        public ActionResult Create()
        {
            CreateProjectViewModel model = new CreateProjectViewModel();
            return View(model);
        }

        // GET: /Projects/Manage
        public ActionResult Manage()
        {
            return View();
        }


        // POST: /Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {


            Project project = new Project();
            project.CreatedOn = DateTime.Now;
            project.Name = model.Name;
            project.Description = model.Description;
            project.Public = false;

            User_Proj up = new User_Proj();
            up.ProjectId = project.ProjectId;
            up.UserId = User.Identity.GetUserId();

                db.Projects.Add(project);
                db.User_Projs.Add(up);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return Json(new {success=true});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
