using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ActionResult> GetGpsByName(string lockName)
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
            var list = JObject.Parse(await handler.RequestPostAsync())["data"]["locks"].Children();
            var json = new Json();
            foreach (var lockObject in list)
            {
                if (lockObject["serial"].Value<string>() == lockName)
                {
                    json = new Json {JsonString = new JavaScriptSerializer().Serialize(new Lock {Id = lockObject["id"].Value<string>(), Name = lockObject["name"].Value<string>(), Latitude = lockObject["latitude"].Value<string>(), Longitude = lockObject["longitude"].Value<string>() })};
                }
            }
            return View("Json",json);
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

        public async Task<ActionResult> AddUser(string userId, string groupId)
        {
            var handler = new APIHandler
            {
                Action = "group/user/add",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    id = groupId,
                    userId = userId
                })
            };
            var json = new Json {JsonString = await handler.RequestPostAsync()};
            return View("Json",json);
        }

        public async Task<ActionResult> RemoveUser(string userId, string groupId)
        {
            var handler = new APIHandler
            {
                Action = "group/user/remove",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    id = groupId,
                    userId = userId
                })
            };
            var json = new Json { JsonString = await handler.RequestPostAsync() };
            return View("Json", json);
        }

        //Session String need to be set, more information about session strings dedicated to this problem could be found in the mobile documentation
        //public async Task<ActionResult> Unlock(string mac = "C8:25:0E:3C:76:60",string session = " ")
        //{
        //    var handler = new APIHandler
        //    {
        //        Action = "lock/unlock/",
        //        Values = new JavaScriptSerializer().Serialize(new
        //        {
        //            mac = mac,
        //            session = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
        //        })
        //    };
        //    var json = new Json { JsonString = await handler.RequestPostAsync() };
        //    return View("Json", json);
        //}
    }
}