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
            if (price != null && startTime != null && endTime != null)
            {
                var model = new Bike()
                {
                    UserId = userId,
                    Description = description,
                    Price = (decimal)price,
                    PhotoUrl = photoUrl,
                    LockId = lockId,
                    FreeTime = new List<Tuple<DateTime, DateTime>> { new Tuple<DateTime, DateTime>((DateTime)startTime, (DateTime)endTime) },
                };
                db.Bikes.Add(model);
                db.SaveChanges();
                var json = new Json { JsonString = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(model) };
                return View("Json", json);
            }
        }

        public ActionResult Update(
            int bikeId,
            string userId,
            string photoUrl,
            decimal? price,
            string lockId,
            DateTime? startTime,
            DateTime? endTime,
            string description)
        {
            //TODO: *Fiabano* Comment why 
            //var tmodel = db.Bikes.FirstOrDefault(t => t.Id == bikeId);
            var model = db.Bikes.FirstOrDefault(x => x.Id == bikeId);
            model.UserId = userId == null ? model.UserId : userId;
            model.PhotoUrl = photoUrl == null ? model.PhotoUrl : photoUrl;
            model.Price = price == null ? model.Price : (decimal)price;
            model.LockId = lockId == null ? model.LockId : lockId;
            model.Description = description == null ? model.Description : description;
            model.FreeTime[0] = new Tuple<DateTime, DateTime>((DateTime)startTime, model.FreeTime.ElementAt(0).Item2);
            model.FreeTime[model.FreeTime.Count() - 1] = new Tuple<DateTime, DateTime>(model.FreeTime.ElementAt(model.FreeTime.Count() - 1).Item1, (DateTime)endTime);
            db.SaveChanges();
            var json = new Json { JsonString = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(model) };
            return View("Json", json);

        }

        public ActionResult Delete(int? id)
        {
            var bike = db.Bikes.FirstOrDefault(x => x.Id == id);
            db.Bikes.Remove(bike);
            db.SaveChanges();
            return View("Json", new Json());
        }
    }
}
