using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veloly_Backend.Models
{
    public class Lock
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}