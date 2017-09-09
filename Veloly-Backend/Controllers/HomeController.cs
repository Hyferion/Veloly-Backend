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

        public async Task<ActionResult> Login()
        {
            var handler = new APIHandler
            {
                Action = "company/login/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    username = "jan@werren.com",
                    password = "Voyager88",
                    companyDomain = "veloly"
                })
            };
            var json = new Json {JsonString = await handler.RequestPostAsync()};
            return View("Json", json);
        }

        public async Task<ActionResult> LockGetAll()
        {
            var handler = new APIHandler
            {
                Action = "lock/get/all/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    username = "jan@werren.com",
                    password = "Voyager88",
                    companyDomain = "veloly"
                })
            };
            var json = new Json { JsonString = await handler.RequestPostAsync() };
            return View("Json",json);
        }
    }
}