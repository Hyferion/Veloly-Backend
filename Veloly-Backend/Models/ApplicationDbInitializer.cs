using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Veloly_Backend.Handler;
using Veloly_Backend.JsonModels;

namespace Veloly_Backend.Models
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var me = new ApplicationUser
            {
                UserName = "jan@werren.com",
                Email = "jan@werren.com",
            };
            userManager.Create(me, "Voyager88!");
            context.SaveChanges();
            var bike1 = new Bike
            {
                User = me,
                Price = 10,
                LockId = "4209",
                Description = "It's an awesome Bike",
                StarTime = DateTime.Now,
                EndTime = DateTime.Now
            };
            context.Bikes.Add(bike1);
            context.SaveChanges();
            var handler = new APIHandler
            {
                Action = "group/create/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    name = "Group" + bike1.Id,
                    lockIds = new List<string> { "4211" },
                    userIds = new List<string> { "5804" },
                    groupType = "online"
                })
            };
            var json = new Json { JsonString = Task.Run(async () => { return await handler.RequestPostAsync(); }).Result};
            var bike2 = new Bike
            {
                User = me,
                Price = 10,
                LockId = "4211",
                Description = "It's an awesome Bike",
                StarTime = DateTime.Now,
                EndTime = DateTime.Now
            };
            context.Bikes.Add(bike2);
            context.SaveChanges();
            handler = new APIHandler
            {
                Action = "group/create/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    name = "Group" + bike2.Id,
                    lockIds = new List<string> { "4210" },
                    userIds = new List<string> { "5804" },
                    groupType = "online"
                })
            };
            json = new Json { JsonString = Task.Run(async () => { return await handler.RequestPostAsync(); }).Result };
            var bike3 = new Bike
            {
                User = me,
                Price = 10,
                LockId = "4209",
                Description = "It's an awesome Bike",
                StarTime = DateTime.Now,
                EndTime = DateTime.Now
            };
            context.Bikes.Add(bike3);
            context.SaveChanges();
            handler = new APIHandler
            {
                Action = "group/create/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    name = "Group" + bike3.Id,
                    lockIds = new List<string> { "4209" },
                    userIds = new List<string> { "5804" },
                    groupType = "online"
                })
            };
            json = new Json { JsonString = Task.Run(async () => { return await handler.RequestPostAsync(); }).Result };

            base.Seed(context);
        }
    }
}