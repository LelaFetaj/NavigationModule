using Journeys.API.Models.Entities.Waypoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Journeys.Models.DTOs.Journeys
{
    public class JourneyRequest
    {
        public double Distance { get; set; }
        public DateTimeOffset StartingDate { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public StartingPoint StartingPoint { get; set; }
        public ArrivalPoint ArrivalPoint { get; set; }
    }
}
