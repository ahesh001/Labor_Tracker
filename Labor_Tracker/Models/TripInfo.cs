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
        public string StartDateTime { get; set; } 
        public string EndDateTime { get; set; }   
        public string Duration { get; set; }
    }
}
