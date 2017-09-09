using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veloly_Backend.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string LockId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public int Price { get; set;}
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}