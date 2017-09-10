using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using Veloly_Backend.Handler;
using Veloly_Backend.JsonModels;
using Veloly_Backend.Models;

namespace Veloly_Backend.Controllers
{   
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<ActionResult> Login(string email, string password)
        {
            var result = await SignInManager.PasswordSignInAsync(email, password, false, shouldLockout: false);
            var userJson = new UserJson();
            if (result == SignInStatus.Success)
            {
                var handler = new APIHandler
                {
                    Action = "company/login/",
                    Values = new JavaScriptSerializer().Serialize(new
                    {
                        username = email,
                        password = password,
                        companyDomain = "veloly"
                    })
                };
                var jObject = JObject.Parse(await handler.RequestPostAsync());
                string nokeId = string.Empty;
                if (jObject != null)
                {
                    if (!jObject["result"].Value<string>().Equals("failure"))
                    {
                        nokeId = jObject["user"]["id"].Value<string>();
                    }
                }
                userJson = new UserJson { UserId = (await UserManager.FindByEmailAsync(email)).Id, Email = email, NokeId = nokeId};
            }
            return View("User",userJson);
        }

        public async Task<ActionResult> Register(string email, string password)
        {
            var userJson = new UserJson();
            if (await UserManager.FindByEmailAsync(email) != null)
            {
                return View("User",userJson);
            }
            var user = new ApplicationUser { UserName = email, Email = email };
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var handler = new APIHandler
                {
                    Action = "user/create/",
                    Values = new JavaScriptSerializer().Serialize(new
                    {
                        username = email,
                        name = email,
                        permissions = new List<string> { "appFlag"}
                    })
                };
                //To get the Result about the register from the api, change the given model to the view
                var json = new Json { JsonString = await handler.RequestPostAsync() };
                userJson = new UserJson { UserId = user.Id, Email = email };
            }
            return View("User",userJson);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Hilfsprogramme
        // Wird für XSRF-Schutz beim Hinzufügen externer Anmeldungen verwendet
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}