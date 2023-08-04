using Journeys.API.Models.Entities.Waypoints;

namespace NavigationModule.Journeys.Models.Entities.Journeys
{
    public class Journey
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        public double Distance { get; set; }
        public DateTimeOffset StartingDate { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public StartingPoint StartingPoint { get; set; }
        public ArrivalPoint ArrivalPoint { get; set; }
    }
}
