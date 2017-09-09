using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Veloly_Backend.Models;

namespace Veloly_Backend.JsonModels
{
    public class BikeJson : Json
    {
        public int? Id { get; set; } = null;
        public ApplicationUser User { get; set; }
        public string LockId { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0.00m;
        public DateTime? StarTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public ICollection<Reservation> Reservations { get; set; } = null;
    }
}