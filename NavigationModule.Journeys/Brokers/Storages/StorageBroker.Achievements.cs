using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NavigationModule.Journeys.Models.Entities.Achievements;
using NavigationModule.Journeys.Models.Entities.Journeys;

namespace NavigationModule.Journeys.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Achievement> Achievements { get; set; }

        public async ValueTask<Achievement> InsertAchievementAsync(Achievement achievement)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Achievement> achievementEntityEntry = await broker.Achievements.AddAsync(achievement);
            await broker.SaveChangesAsync();


            return achievementEntityEntry.Entity;
        }

        public async ValueTask<Achievement> SelectAchievementByUserIdAsync(string userId)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.Achievements.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async ValueTask<Achievement> UpdateAchievementAsync(Achievement achievement)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Achievement> achievementEntityEntry = broker.Achievements.Update(achievement);
            await broker.SaveChangesAsync();

            return achievementEntityEntry.Entity;
        }
    }
}
