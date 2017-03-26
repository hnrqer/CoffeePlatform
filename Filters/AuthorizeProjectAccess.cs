using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using Microsoft.AspNet.Identity;
using System.Web.Routing;

namespace WebApplication.Filters
{
    public class AuthorizeProjectAccess : AuthorizeAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var rd = httpContext.Request.RequestContext.RouteData;
            int projectID;
            try{
                projectID = Convert.ToInt32(rd.GetRequiredString("id"));
            }catch (Exception e){
                return false;
            }
            

            var userID = httpContext.User.Identity.GetUserId();

            var result = db.User_Projs.Where(i => i.UserId == userID & i.ProjectId == projectID).ToList();
            
            if (result.Count == 0) {
                return false;
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" })); 
        }
    }
    public class AuthorizeLoadProjectAccess : AuthorizeAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var rd = httpContext.Request.RequestContext.RouteData;
            int projectID;
            try
            {
                projectID = Convert.ToInt32(rd.GetRequiredString("id"));
            }
            catch (Exception e)
            {
                return false;
            }


            var userID = httpContext.User.Identity.GetUserId();

            //var result = db.User_Projs.Where(i => i.UserId == userID & i.ProjectId == projectID).ToList();
            var query = from up in db.User_Projs
                        join p in db.Projects on up.ProjectId equals p.ProjectId
                        where up.UserId == userID & up.ProjectId == projectID | p.Public
                        select p.Name;
            var result = query.ToList();

            if (result.Count == 0)
            {
                return false;
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
        }
    }

}