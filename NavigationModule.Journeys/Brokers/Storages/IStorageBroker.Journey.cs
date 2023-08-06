using NavigationModule.Infrastructure.Models.Filters;
using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;
using System.Linq.Expressions;

namespace NavigationModule.Journeys.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Journey> InsertJourneyAsync(Journey journey);
        ValueTask<(List<Journey>, long count)> SelectFilteredJourneysAsync(
            Expression<Func<Journey, bool>> searchCondition,
            Pagination<Journey, DateTimeOffset> pagination);
        ValueTask<Journey> SelectJourneyByFilterAsync(
           Expression<Func<Journey, bool>> searchCondition);
        ValueTask<Journey> SelectJourneyByIdAsync(Guid journeyId);
        ValueTask<Journey> UpdateJourneyAsync(Journey journey);
        ValueTask<Journey> DeleteJourneyAsync(Journey journey);
        ValueTask<List<UserStats>> SelectJourneyStatsAsync(
            Expression<Func<Journey, bool>> searchCondition,
            Pagination<UserStats, double> pagination);

    }
}
