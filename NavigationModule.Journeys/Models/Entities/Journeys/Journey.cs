using Journeys.API.Models.Entities.Waypoints;

namespace NavigationModule.Journeys.Models.Entities.Journeys
{
    public class Journey
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public double Distance { get; set; }
        public DateTimeOffset StartingDate { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public StartingPoint StartingPoint { get; set; }
        public ArrivalPoint ArrivalPoint { get; set; }
    }
}
