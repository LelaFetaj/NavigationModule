namespace NavigationModule.Journeys.Models.DTOs.Filters
{
    public class JourneyFilter
    {
        public string UserId { get; set; }
        public Months Month { get; set; }
        public int Year { get; set; } = DateTime.UtcNow.Year;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public bool OrderByDesceding { get; set; } = true;
    }
}
