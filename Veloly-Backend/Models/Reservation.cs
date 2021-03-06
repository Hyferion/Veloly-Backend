﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veloly_Backend.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Bike Bike { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}