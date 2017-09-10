﻿using System;
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
            Db.Reservations.Add(model);
            Db.SaveChanges();
            var json = new Json { JsonString = new JavaScriptSerializer().Serialize(model) };
            return View("Json", json);
        }
        public ActionResult Update(int reservationId, string userId, int? bikeId, DateTime? startTime, DateTime? endTime, DateTime? date)
        {
            var model = Db.Reservations.FirstOrDefault(x => x.Id == reservationId);
            Json json;
            if (model == null)
            {
                json = new Json { JsonString = new JavaScriptSerializer().Serialize(new Reservation()) };
                return View("Json", json);
            }

            model.User = userId == null ? model.User : Db.Users.FirstOrDefault(x => x.Id == userId);
            model.Bike = bikeId == null ? model.Bike : Db.Bikes.FirstOrDefault(x => x.Id == bikeId);
            model.StartTime = startTime ?? model.StartTime;
            model.EndTime = endTime ?? model.EndTime;
            Db.SaveChanges();
            json = new Json { JsonString = new JavaScriptSerializer().Serialize(model) };
            return View("Json", json);
        }
        public ActionResult Remove(int reservationId)
        {
            var model = Db.Reservations.FirstOrDefault(x => x.Id == reservationId);
            if (model != null) Db.Reservations.Remove(model);
            Db.SaveChanges();
            return View("Json", new Json { JsonString = new JavaScriptSerializer().Serialize(new Reservation()) });
        }
    }
}