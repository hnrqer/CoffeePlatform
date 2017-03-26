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
    public class AuthorizeSmartphoneAccess : AuthorizeAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string userName = httpContext.Request.QueryString["UserName"];
            string password = httpContext.Request.QueryString["PasswordHash"];

            var result = db.Users.Where(i => i.UserName == userName & i.PasswordHash == password).ToList();
            if (result.Count == 0)
            {
                return false;
            }

            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            JsonResult UnauthorizedResult = new JsonResult();
            UnauthorizedResult.Data = "{ request : 'unauthorized' }";
            UnauthorizedResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            filterContext.Result = UnauthorizedResult;
        }
    }
}