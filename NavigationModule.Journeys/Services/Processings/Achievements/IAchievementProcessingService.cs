using NavigationModule.Journeys.Models.DTOs.Journeys;

namespace NavigationModule.Journeys.Services.Processings.Achievements
{
    public interface IAchievementProcessingService
    {
        Task UpsertAchievementAsync(JourneyRequest journeyRequest);
    }
}
