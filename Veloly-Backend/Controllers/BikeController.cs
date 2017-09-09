using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Veloly_Backend.Models;

namespace Veloly_Backend.Controllers
{
    public class BikeController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bikes
        public ActionResult Create()
        {
            var model = new Bike()
            {
                
            }
        }

    }
}
