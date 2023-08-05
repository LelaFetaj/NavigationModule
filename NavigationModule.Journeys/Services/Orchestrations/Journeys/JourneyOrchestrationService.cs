using NavigationModule.Journeys.Models.DTOs.Filters;
using NavigationModule.Journeys.Models.DTOs.Journeys;
using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NavigationModule.Journeys.Services.Processings.Achievements;
using NavigationModule.Journeys.Services.Processings.Journeys;

namespace NavigationModule.Journeys.Services.Orchestrations.Journeys
{
    public class JourneyOrchestrationService : IJourneyOrchestrationService
    {
        private readonly IAchievementProcessingService achievementProcessingService;
        private readonly IJourneyProcessingService journeyProcessingService;

        public JourneyOrchestrationService(
            IAchievementProcessingService achievementProcessingService,
            IJourneyProcessingService journeyProcessingService)
        {
            this.achievementProcessingService = achievementProcessingService;
            this.journeyProcessingService = journeyProcessingService;
        }

        public async ValueTask<Journey> AddJourneyAsync(JourneyRequest journeyRequest)
        {
            var journey = await this.journeyProcessingService.AddJourneyAsync(journeyRequest);

            await this.achievementProcessingService.UpsertAchievementAsync(journeyRequest);

            return journey;
        }

        public async ValueTask<IReadOnlyList<Journey>> RetrieveJourneysAsync(
            int page = 1,
            int pagesize = 0,
            bool orderByDescending = true)
        {
            return await this.journeyProcessingService.RetrieveJourneysAsync(
                page,
                pagesize,
                orderByDescending);
        }

        public async ValueTask<Journey> RetrieveJourneyByIdAsync(Guid journeyId) =>
            await this.journeyProcessingService.RetrieveJourneyByIdAsync(journeyId);

        public async ValueTask<Journey> RemoveJourneyByIdAsync(Guid journeyId) =>
            await this.journeyProcessingService.RemoveJourneyByIdAsync(journeyId);

        public async ValueTask<IReadOnlyList<Journey>> FilterJourneysAsync(
            string userId,
            int page = 1,
            int pagesize = 0,
            bool orderByDescending = true)
        {
            return await this.journeyProcessingService.RetrieveJourneysAsync(
                userId,
                page,
                pagesize,
                orderByDescending);
        }

        public async ValueTask<IReadOnlyList<UserStats>> GetJourneysStatsAsync(JourneyFilter filters)
        {
            return await this.journeyProcessingService.RetrieveJourneyStatsAsync(filters);
        }
    }
}
