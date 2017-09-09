using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
            var handler = new APIHandler();
            handler.Login();
            var json = new Json {JsonString = await handler.RequestPostAsync()};
            return View(json);
        }

        public async Task<ActionResult> LockGetAll()
        {
            var handler = new APIHandler();
            handler.LockGetAll();
            var json = new Json {JsonString = await handler.RequestPostAsync()};
            return View(json);
        }
    }
}