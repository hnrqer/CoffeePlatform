using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(returnUrl);
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToAction("DialogVerifyShared", "Projects", new { type = "list" });
                }
                else
                {
                    ModelState.AddModelError("", "Nome de usuário ou senha inválidos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/LoginS
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> LoginS(LoginViewModel model)
        {
            var response = System.Web.HttpContext.Current.Response;
            response.Headers.Add("Content-type", "application/json");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    //var cookie = response.Headers.Get("Set-Cookie");
                    string passwordHash;
                    string id;
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        var userAux = db.Users.Where(i => i.UserName == model.UserName).Select(X => X).SingleOrDefault();
                        passwordHash = userAux.PasswordHash;
                        id = userAux.Id;
                    }
                    return Json(new { success = true, passwordHash });

                }
                else
                {
                    ModelState.AddModelError("", "Nome de usuário ou senha inválidos.");
                }
            }

            return Json(new { success = false });
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email, Name = model.Name };
                var result = await UserManager.CreateAsync(user, model.Password);
                

                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("", "Nome de usuário já existente.");
                    //AddErrors(result);

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
/*
        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }
*/
        //
        // GET: /Account/Manage
        public ActionResult Profile()
        {
            try
            {
                var id = User.Identity.GetUserId();
                var query = from u in db.Users
                            where u.Id == id
                            select u;
                var x = query.SingleOrDefault();
                var query2 = from up in db.User_Projs
                             where up.UserId == id
                             group up by up.UserId into g
                             select  g.Select(Y => Y.ProjectId).Distinct().Count();

                var result = new ProfileViewModel() { Name = x.Name, Email = x.Email, NumProjetos = query2.SingleOrDefault() };
                
                return View(result);
            }
            catch (Exception e) { return RedirectToAction("AccessDenied","Error"); };
        }
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Sua senha foi alterada com sucesso."
                : message == ManageMessageId.SetPasswordSuccess ? "Senha adicionada com sucesso."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocorreu um erro, sua senha não foi modificada.");
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocorreu um erro, sua senha não foi adicionada.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/Manage
        public ActionResult ManageMail( ManageMessageId? message)
        {
            ViewBag.ReturnUrl = Url.Action("ManageMail");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public String ManageMail(ManageMailViewModel model)
        {
            try
            {
                ViewBag.ReturnUrl = Url.Action("ManageMail");
                var userid = User.Identity.GetUserId();

                using (var context = new ApplicationDbContext())
                {
                    var obj = context.Users.Single(x => x.Id == userid);
                    obj.Email = model.NewEmail;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return "Erro ao atualizar e-mail";
            }
            
            return "E-mail atualizado com sucesso";
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");

        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}