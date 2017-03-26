using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Filters;

namespace WebApplication.Controllers
{
    public class SmartphoneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // EXEMPLO:  /Smartphone/LoadProjectCode/?Id="1"&PasswordHash=""&projectID=1
        //Nao vou me preocupar em testar se o usuario tenta acessar projetos de outros usuários (como no Projects/LoadProjectData)
        //pois o aplicativo no smartphone deverá acessar somente projetos recebidos pelo LoadProjectList.
        
        [AcceptVerbs(HttpVerbs.Get)]
        [AuthorizeSmartphoneAccess]
        public JsonResult LoadProjectCode(LoginSmartphone login, int? projectID)
        {

            var response = System.Web.HttpContext.Current.Response;
            response.Headers.Add("Content-type", "application/json");
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            var project = db.Projects.Where(i => i.ProjectId == projectID).SingleOrDefault();
            var myData = new { BlocklyCode = project.BlocklyCode, BlocklyData = project.BlocklyData, LastSaved = project.LastSaved};
            return Json(myData, JsonRequestBehavior.AllowGet);
        }
        // GET: /Smartphone/LoadProjectList
        [AcceptVerbs(HttpVerbs.Get)]
        [AuthorizeSmartphoneAccess]
        public JsonResult LoadProjectList(LoginSmartphone login)
        {
            var response = System.Web.HttpContext.Current.Response;
            response.Headers.Add("Content-type", "application/json");
            response.Headers.Add("Access-Control-Allow-Origin", "*");


            var query = from u in db.Users
                        join up in db.User_Projs on u.Id equals up.UserId
                        where u.UserName == login.UserName
                        join p in db.Projects on up.ProjectId equals p.ProjectId
                        select new { p.ProjectId, p.Name, p.LastSaved, p.CreatedOn, p.Description };
            var projectList = query.ToList();

            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
       
	}
}