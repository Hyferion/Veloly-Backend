using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Veloly_Backend.JsonModels;
using Veloly_Backend.Models;

namespace Veloly_Backend.Controllers
{
    public class BikeController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(
            string userId,
            string description,
            decimal? price,
            string photoUrl,
            string lockId,
            DateTime? startTime,
            DateTime? endTime)
        {
            var model = new Bike()
            {
                User = db.Users.FirstOrDefault(x => x.Id == userId),
                Description = description,
                Price = (decimal)price,
                PhotoUrl = photoUrl,
                LockId = lockId,
                StarTime = (DateTime)startTime,
                EndTime = (DateTime)endTime,
                TimeStamp = DateTime.Now
            };
            db.Bikes.Add(model);
            db.SaveChanges();
            var json = new Json { JsonString = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(model) };
            return View("Json", json);
        }

        public ActionResult Update(
            int bikeId,
            string userId,
            string photoUrl,
            decimal price,
            string lockId,
            DateTime startTime,
            DateTime endTime,
            string description)
        {
            var model = db.Bikes.FirstOrDefault(x => x.Id == bikeId);
            model.User = userId == null ? model.User : db.Users.FirstOrDefault(x => x.Id == userId);
            model.PhotoUrl = photoUrl == null ? model.PhotoUrl : photoUrl;
            model.Price = price == null ? model.Price : price;
            model.LockId = lockId == null ? model.LockId : lockId;
            model.Description = description == null ? model.Description : description;
            model.StarTime = startTime == null ? model.StarTime : startTime;
            model.EndTime = endTime == null ? model.EndTime : endTime;

            db.SaveChanges();

            var json = new Json { JsonString = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(model) };
            return View("Json", json);

        }

        public ActionResult Delete(int id)
        {
            var bike = db.Bikes.FirstOrDefault(x => x.Id == id);
            db.Bikes.Remove(bike);
            db.SaveChanges();
            return View();
        }
    }
}
