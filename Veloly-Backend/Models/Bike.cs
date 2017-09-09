using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veloly_Backend.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string LockId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set;}
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public DateTime TimeStamp { get; set; } 
    }
}