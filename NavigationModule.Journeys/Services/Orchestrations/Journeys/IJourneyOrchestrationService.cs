using NavigationModule.Journeys.Models.DTOs.Filters;
using NavigationModule.Journeys.Models.DTOs.Journeys;
using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;

namespace NavigationModule.Journeys.Services.Orchestrations.Journeys
{
    public interface IJourneyOrchestrationService
    {
        ValueTask<Journey> AddJourneyAsync(JourneyRequest journeyRequest);
        ValueTask<IReadOnlyList<Journey>> RetrieveJourneysAsync(
            int page = 1,
            int pagesize = 0,
            bool orderByDescending = true);
        ValueTask<Journey> RetrieveJourneyByIdAsync(Guid journeyId);
        ValueTask<Journey> RemoveJourneyByIdAsync(Guid journeyId);
        ValueTask<IReadOnlyList<Journey>> FilterJourneysAsync(
            string userId,
            int page = 1,
            int pagesize = 0,
            bool orderByDescending = true);
        ValueTask<IReadOnlyList<UserStats>> GetJourneysStatsAsync(JourneyFilter filters);
    }
}
