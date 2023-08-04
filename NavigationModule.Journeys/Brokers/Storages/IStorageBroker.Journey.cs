using NavigationModule.Journeys.Models.DTOs.Filters;
using NavigationModule.Journeys.Models.Entities.Journeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

    }
}
