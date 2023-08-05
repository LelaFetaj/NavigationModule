using NavigationModule.Journeys.Models.Entities.Achievements;

namespace NavigationModule.Journeys.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Achievement> InsertAchievementAsync(Achievement achievement);
        ValueTask<Achievement> SelectAchievementByUserIdAsync(string userId);
        ValueTask<Achievement> UpdateAchievementAsync(Achievement achievement);
    }
}
