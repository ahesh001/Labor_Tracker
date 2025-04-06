using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labor_Tracker.Models
{
    public class TripInfo
    {
        public string TruckId { get; set; }
        public string Driver { get; set; }
        public string Load { get; set; }
        public string StartTime { get; set; } // or DateTime if you prefer
        public string EndTime { get; set; }   // or DateTime
        public string Duration { get; set; }
    }
}
