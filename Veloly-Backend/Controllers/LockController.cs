using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Veloly_Backend.Handler;
using Veloly_Backend.JsonModels;
using Veloly_Backend.Models;

namespace Veloly_Backend.Controllers
{
    public class LockController : Controller
    {
        public async Task<ActionResult> Gps()
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
            var list = new Json{ JsonString = new JavaScriptSerializer().Serialize(JObject.Parse(await handler.RequestPostAsync())["data"]["locks"].Children().Select(lockJson => new Lock {Id = lockJson["id"].Value<string>(), Name = lockJson["name"].Value<string>(), Latitude = lockJson["latitude"].Value<string>(), Longitude = lockJson["longitude"].Value<string>()}).ToList())};
            return View("Json", list);
        }

        public async Task<ActionResult> All()
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
            return View("Json", json);
        }

        /*public async Task<ActionResult> Setup(string mac = "C8:25:0E:3C:76:60", string session = "vcMwOz99oYeFtMuX9W9eP3pMFTwfmWxtnPx5Pdph")
        {
            var handler = new APIHandler
            {
                Action = "/lock/setup/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    mac = mac,
                    session = session
                })
            };
            var json = new Json { JsonString = await handler.RequestPostAsync() };
            return View("Json", json);*/
        }
    }
}