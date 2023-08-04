using NavigationModule.Journeys.Models.Entities.Journeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Journeys.Services.Foundations.Journeys
{
    public interface IJourneyService
    {
        ValueTask<bool> AddJourneyAsync(Journey journey);
        ValueTask<(List<Journey>, long count)> RetrieveJourneysAsync(
            Expression<Func<Journey, bool>> searchCondition);
        ValueTask<Journey> RetrieveJourneyByIdAsync(string journeyId);
        ValueTask<Journey> RetrieveLatestJourneyByUserIdAsync(string userId);
        ValueTask<Journey> ModifyJourneyAsync(Journey journey);
    }
}
