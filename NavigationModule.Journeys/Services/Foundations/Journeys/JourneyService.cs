using NavigationModule.Journeys.Brokers.Storages;
using NavigationModule.Journeys.Models.DTOs.Filters;
using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NetLoggings.Brokers.Loggings;
using System.Linq.Expressions;

namespace NavigationModule.Journeys.Services.Foundations.Journeys
{
    public partial class JourneyService : IJourneyService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public JourneyService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Journey> AddJourneyAsync(Journey journey) =>
            TryCatch(async () =>
            {
                ValidateJourneyInput(journey);
                return await this.storageBroker.InsertJourneyAsync(journey);
            });

        public ValueTask<(List<Journey> journeys, long count)> RetrieveFilteredJourneysAsync(
            Expression<Func<Journey, bool>> searchCondition,
            Pagination<Journey, DateTimeOffset> pagination) =>
            TryCatch(async () =>
            {
                return await this.storageBroker.SelectFilteredJourneysAsync(
                    searchCondition,
                    pagination);
            });

        public ValueTask<Journey> RetrieveJourneyByIdAsync(Guid journeyId) =>
            TryCatch(async () =>
            {
                ValidateJourneyId(journeyId);

                Journey maybeJourney =
                    await this.storageBroker.SelectJourneyByIdAsync(journeyId);

                ValidateStorageJourney(maybeJourney, journeyId);

                return maybeJourney;
            });

        public ValueTask<Journey> RemoveJourneyByIdAsync(Guid journeyId) =>
            TryCatch(async () =>
            {
                ValidateJourneyId(journeyId);
                Journey maybeJourney = await this.storageBroker.SelectJourneyByIdAsync(journeyId);
                ValidateStorageJourney(maybeJourney, journeyId);

                return await this.storageBroker.DeleteJourneyAsync(maybeJourney);
            });

        public ValueTask<List<UserStats>> RetrieveJourneyStatsAsync(
            Expression<Func<Journey, bool>> searchCondition,
            Pagination<UserStats, double> pagination) =>
            TryCatch(async () =>
            {
                return await this.storageBroker.SelectJourneyStatsAsync(
                    searchCondition,
                    pagination);
            });
    }
}
