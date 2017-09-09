using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veloly_Backend.Models;

namespace Veloly_Backend.Controllers
{
    public class ReservationController : Controller
    {
        public ApplicationDbContext Db { get; set; } = new ApplicationDbContext();
        // GET: Reservation
        public ActionResult Create(string userId)
        {
            var model = new Reservation
            {
                User = Db.Users.FirstOrDefault(x => x.Id == userId),
            };
            Db.Reservations.Add(model);
            Db.SaveChanges();
            return View();
        }
    }
}