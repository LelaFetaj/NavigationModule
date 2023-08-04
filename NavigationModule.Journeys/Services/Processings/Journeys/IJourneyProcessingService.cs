using NavigationModule.Journeys.Models.DTOs.Journeys;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NavigationModule.Journeys.Models.Filters;

namespace NavigationModule.Journeys.Services.Processings.Journeys
{
    public interface IJourneyProcessingService
    {
        ValueTask<Journey> AddJourneyAsync(JourneyRequest journeyRequest);
        ValueTask<FilteredResponse<JourneyResponse>> RetrieveFilteredJourneys(JourneyFilter filters);
        ValueTask<IReadOnlyList<JourneyResponse>> RetrieveJourneysAsync(
            int page = 1,
            int pagesize = 0,
            bool orderByDescending = true);
        ValueTask<JourneyResponse> RetrieveLatestJourneyAsync();
    }
}
