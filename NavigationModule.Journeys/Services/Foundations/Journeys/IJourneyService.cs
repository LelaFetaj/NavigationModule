using NavigationModule.Infrastructure.Models.Filters;
using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;
using System.Linq.Expressions;

namespace NavigationModule.Journeys.Services.Foundations.Journeys
{
    public interface IJourneyService
    {
        ValueTask<Journey> AddJourneyAsync(Journey journey);
        ValueTask<(List<Journey> journeys, long count)> RetrieveFilteredJourneysAsync(
            Expression<Func<Journey, bool>> searchCondition,
            Pagination<Journey, DateTimeOffset> pagination);
        ValueTask<Journey> RetrieveJourneyByIdAsync(Guid journeyId);
        ValueTask<Journey> RemoveJourneyByIdAsync(Guid journeyId);
        ValueTask<List<UserStats>> RetrieveJourneyStatsAsync(
            Expression<Func<Journey, bool>> searchCondition,
            Pagination<UserStats, double> pagination);
    }
}
