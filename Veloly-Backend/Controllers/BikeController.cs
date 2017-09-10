using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
            if (price == null && startTime == null && endTime == null)
            {
                return View("Json", new Json {JsonString = new JavaScriptSerializer().Serialize(new Bike())});
            }

            var model = new Bike
            {
                UserId = userId,
                Description = description,
                Price = (decimal)price,
                PhotoUrl = photoUrl,
                LockId = lockId,
                StartTime = (DateTime)startTime,
                EndTime = (DateTime)endTime,
                FreeTime = new List<Tuple<DateTime, int>>(),
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
            decimal? price,
            string lockId,
            DateTime? startTime,
            DateTime? endTime,
            string description)
        {
            if (price == null && startTime == null && endTime == null)
            {
                return View("Json", new Json { JsonString = new JavaScriptSerializer().Serialize(new Bike()) });
            }

            var model = db.Bikes.FirstOrDefault(x => x.Id == bikeId);
            if (model == null) return RedirectToAction("Index", "Home");
            model.UserId = userId ?? model.UserId;
            model.PhotoUrl = photoUrl ?? model.PhotoUrl;
            model.Price = price ?? model.Price;
            model.LockId = lockId ?? model.LockId;
            model.Description = description ?? model.Description;
            model.StartTime = (DateTime)startTime;
            model.EndTime = (DateTime)endTime;
            db.SaveChanges();
            var json = new Json { JsonString = new JavaScriptSerializer().Serialize(model) };
            return View("Json", json);

        }

        public ActionResult Delete(int? bikeId)
        {
            var bike = db.Bikes.FirstOrDefault(x => x.Id == bikeId);
            if (bike == null) return RedirectToAction("Index", "Home");
            db.Bikes.Remove(bike);
            db.SaveChanges();
            return View("Json", new Json());
        }
    }
}
