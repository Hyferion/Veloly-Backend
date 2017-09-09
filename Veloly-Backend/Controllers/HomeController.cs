using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Veloly_Backend.Handler;
using Veloly_Backend.JsonModels;

namespace Veloly_Backend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Login(string username, string password)
        {
            var handler = new APIHandler
            {
                Action = "company/login/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    username = username,
                    password = password,
                    companyDomain = "veloly"
                })
            };
            var json = new Json {JsonString = await handler.RequestPostAsync()};
            return View("Json", json);
        }
    }
}