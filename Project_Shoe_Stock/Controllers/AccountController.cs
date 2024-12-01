using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Project_Shoe_Stock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Shoe_Stock.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var store = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(store);
                var user = manager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "You Login Failed, Please Check Username or Password.");
                }
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var store = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(store);
                var user = new IdentityUser { UserName = model.UserName };
                var result = manager.Create(user, model.Password);
                
                if (result.Succeeded)
                {
                    manager.AddToRole(user.Id, "Users");
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed");
                }
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}