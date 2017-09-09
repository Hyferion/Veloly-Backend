using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Veloly_Backend.JsonModels;
using Veloly_Backend.Models;

namespace Veloly_Backend.Controllers
{
    public class ReservationController : Controller
    {
        public ApplicationDbContext Db { get; set; } = new ApplicationDbContext();
        // GET: Reservation
        public ActionResult Create(string userId, int? bikeId, DateTime startTime, DateTime endTime, DateTime date)
        {
            var model = new Reservation
            {
                User = Db.Users.FirstOrDefault(x => x.Id == userId),
                Bike = bikeId == null ? null : Db.Bikes.FirstOrDefault(x => x.Id == bikeId),
                StartTime = startTime,
                EndTime = endTime,
            };
            var bike = Db.Bikes.FirstOrDefault(x => x.Id == model.Bike.Id);
            if(model.Bike == null) return RedirectToAction("Index", "Home");
            bike.FreeTime.Add(new Tuple<DateTime, int>(startTime, -1));
            bike.FreeTime.Add(new Tuple<DateTime, int>(endTime, 1));
            Db.Reservations.Add(model);
            Db.SaveChanges();
            var json = new Json { JsonString = new JavaScriptSerializer().Serialize(model) };
            return View("Json", json);
        }
        public ActionResult Update(int reservationId, string userId, int? bikeId, DateTime? startTime, DateTime? endTime, DateTime? date)
        {
            var model = Db.Reservations.FirstOrDefault(x =>  x.Id == reservationId);
            var bike = Db.Bikes.FirstOrDefault(x => x.Id == model.Bike.Id);
            bike.FreeTime.Remove(new Tuple<DateTime, int>(model.StartTime, -1));
            bike.FreeTime.Remove(new Tuple<DateTime, int>(model.EndTime, 1));
            model.User = userId == null ? model.User : Db.Users.FirstOrDefault(x => x.Id == userId);
            model.Bike = bikeId == null ? model.Bike :  Db.Bikes.FirstOrDefault(x => x.Id == bikeId);
            model.StartTime = startTime == null ? model.StartTime : (DateTime)startTime;
            model.EndTime = endTime == null ? model.EndTime : (DateTime)endTime;
            bike = Db.Bikes.FirstOrDefault(x => x.Id == model.Bike.Id);
            bike.FreeTime.Add(new Tuple<DateTime, int>(model.StartTime, -1));
            bike.FreeTime.Add(new Tuple<DateTime, int>(model.EndTime, 1));
            Db.SaveChanges();
            var json = new Json { JsonString = new JavaScriptSerializer().Serialize(model) };
            return View("Json", json);
        }
        public ActionResult Remove(int reservationId)
        {
            var model = Db.Reservations.FirstOrDefault(x => x.Id == reservationId);
            var bike = Db.Bikes.FirstOrDefault(x => x.Id == model.Bike.Id);
            bike.FreeTime.Remove(new Tuple<DateTime, int>(model.StartTime, -1));
            bike.FreeTime.Remove(new Tuple<DateTime, int>(model.EndTime, 1));
            Db.Reservations.Remove(model);
            Db.SaveChanges();
            return View("Json", new Reservation { });
        }
        public ActionResult Schedule(int bikeId)
        {
            List<Tuple<DateTime, int>> model = Db.Bikes.FirstOrDefault(x => x.Id == bikeId).FreeTime;
            model.Add(new Tuple<DateTime, int>(Db.Bikes.FirstOrDefault(x => x.Id == bikeId).StartTime, 1));
            model.Add(new Tuple<DateTime, int>(Db.Bikes.FirstOrDefault(x => x.Id == bikeId).EndTime, -1));
            var json = new Json { JsonString = new JavaScriptSerializer().Serialize(model) };
            return View("Json", json);
        }
    }
}