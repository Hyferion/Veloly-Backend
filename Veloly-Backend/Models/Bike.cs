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
        public string GroupId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;
        public List<Tuple<DateTime, int>> FreeTime{ get; set; }
    }
}