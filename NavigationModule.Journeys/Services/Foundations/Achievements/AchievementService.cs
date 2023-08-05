using NavigationModule.Journeys.Brokers.Storages;
using NavigationModule.Journeys.Models.Entities.Achievements;
using NetLoggings.Brokers.Loggings;

namespace NavigationModule.Journeys.Services.Foundations.Achievements
{
    public partial class AchievementService : IAchievementService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public AchievementService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker=storageBroker;
            this.loggingBroker=loggingBroker;
        }

        public ValueTask<Achievement> RetrieveAchievementAsync(string userId) =>
            TryCatch(async () =>
            {
                ValidateUserId(userId);

                return await this.storageBroker.SelectAchievementByUserIdAsync(userId);
            });

        public Task GenerateAchievementAsync(Achievement achievement) =>
            TryCatch(async () =>
            {
                ValidateAchievement(achievement);

                await this.storageBroker.InsertAchievementAsync(achievement);
            });

        public Task UpdateAchievementAsync(Achievement achievement) =>
            TryCatch(async () =>
            {
                ValidateAchievement(achievement);

                await this.storageBroker.UpdateAchievementAsync(achievement);
            });
    }
}
