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
            var bike1 = new Bike
            {
                Price = 10,
                LockId = "4209",
                Description = "It's an awesome Bike"
            };
            context.Bikes.Add(bike1);
            context.SaveChanges();
            bike1.GroupId = "Group" + bike1.Id;
            var handler = new APIHandler
            {
                Action = "group/create/",
                Values = "{'name': 'My Example Group','schedule': [{'startDate': '2017 - 01 - 30T16:00:00Z','endDate': '2017 - 01 - 31T00: 00:00Z','expiration': '3001 - 01 - 01T07: 00:00Z','repeatType': 'weekly','dayOfWeek': 'monday'},{'startDate': '2017 - 01 - 27T16: 00:00Z','endDate': '2017 - 01 - 28T00: 00:00Z','expiration': '3001 - 01 - 01T07: 00:00Z','repeatType': 'weekly','dayOfWeek': 'friday'},{'startDate': '2017 - 01 - 28T16: 00:00Z','endDate': '2017 - 01 - 29T00: 00:00Z','expiration':'3001 - 01 - 01T07: 00:00Z','repeatType': 'weekly','dayOfWeek': 'saturday'}],'lockIds': [123],'userIds': [321],'groupType': 'online'}"
            };
            //var json = new Json { JsonString = Task.Run(async () => { return await handler.RequestPostAsync(); }).Result};
             var bike2 = new Bike
            {
                Price = 10,
                LockId = "4211",
                Description = "It's an awesome Bike"
            };
            context.Bikes.Add(bike2);
            context.SaveChanges();
            bike1.GroupId = "Group" + bike2.Id;
            handler = new APIHandler
            {
                Action = "group/create/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    name = "Group" + bike2.Id,
                    schedule = new List<object> { new
                    {
                        startDate = "2017-09-09T00:00:00Z",
                        endDate = "2017-09-10T00:00:00Z",
                        expiration = "3001-01-01T07:00:00Z",
                        repeatType = "weekly",
                        dayOfWeek = "monday"
                    } },
                    lockIds = new List<string> { "4211" },
                    userIds = new List<string> { "5804" },
                    groupType = "offline"
                })
            };
            //json = new Json { JsonString = Task.Run(async () => { return await handler.RequestPostAsync(); }).Result };
            var bike3 = new Bike
            {
                Price = 10,
                LockId = "4209",
                Description = "It's an awesome Bike"
            };
            context.Bikes.Add(bike3);
            context.SaveChanges();
            bike1.GroupId = "Group" + bike3.Id;
            handler = new APIHandler
            {
                Action = "group/create/",
                Values = new JavaScriptSerializer().Serialize(new
                {
                    name = "Group" + bike3.Id,
                    schedule = new List<object> { new
                    {
                        startDate = "2017-09-09T00:00:00Z",
                        endDate = "2017-09-10T00:00:00Z",
                        expiration = "3001-01-01T07:00:00Z",
                        repeatType = "weekly",
                        dayOfWeek = "monday"
                    } },
                    lockIds = new List<string> { "4211" },
                    userIds = new List<string> { "5804" },
                    groupType = "offline"
                })
            };
            //json = new Json { JsonString = Task.Run(async () => { return await handler.RequestPostAsync(); }).Result };

            base.Seed(context);
        }
    }
}