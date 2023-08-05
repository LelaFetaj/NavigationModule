using NavigationModule.Journeys.Models.Entities.Achievements;

namespace NavigationModule.Journeys.Services.Foundations.Achievements
{
    public interface IAchievementService
    {
        ValueTask<Achievement> RetrieveAchievementAsync(string userId);
        Task GenerateAchievementAsync(Achievement achievement);
        Task UpdateAchievementAsync(Achievement achievement);
    }
}
