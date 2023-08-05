using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NavigationModule.Journeys.Infrastructure.CollectionExtentions;
using NavigationModule.Journeys.Models.DTOs.Filters;
using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;
using System.Linq.Expressions;

namespace NavigationModule.Journeys.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Journey> Journeys { get; set; }

        public async ValueTask<Journey> InsertJourneyAsync(Journey journey)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Journey> journeyEntityEntry = await broker.Journeys.AddAsync(journey);
            await broker.SaveChangesAsync();


            return journeyEntityEntry.Entity;
        }

        public async ValueTask<(List<Journey>, long count)> SelectFilteredJourneysAsync(
            Expression<Func<Journey, bool>> searchCondition,
            Pagination<Journey, DateTimeOffset> pagination)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            IQueryable<Journey> journeys = broker.Journeys.Where(searchCondition);

            long count = await journeys?.LongCountAsync();

            return (
                await journeys
                    .PageBy(pagination)
                    .ToListAsync(),
                count);
        }
        
        public async ValueTask<List<UserStats>> SelectJourneyStatsAsync(
            Expression<Func<Journey, bool>> searchCondition,
            Pagination<UserStats, double> pagination)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            IQueryable<Journey> journeys = broker.Journeys.Where(searchCondition);

            return await journeys
                .GroupBy(x => x.UserId)
                .Select(group => new UserStats
                {
                    UserId = group.Key,
                    JourneyCount = group.Count(),
                    TotalDistance = group.Sum(x => x.Distance)
                })
                .PageBy(pagination)
                .ToListAsync();
        }

        public async ValueTask<Journey> SelectJourneyByFilterAsync(
           Expression<Func<Journey, bool>> searchCondition)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.Journeys.FirstOrDefaultAsync(searchCondition);
        }

        public async ValueTask<Journey> SelectJourneyByIdAsync(Guid journeyId)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.Journeys.FindAsync(journeyId);
        }

        public async ValueTask<Journey> UpdateJourneyAsync(Journey journey)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Journey> journeynEntityEntry = broker.Journeys.Update(journey);
            await broker.SaveChangesAsync();

            return journeynEntityEntry.Entity;
        }

        public async ValueTask<Journey> DeleteJourneyAsync(Journey journey)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Journey> journeyEntityEntry = broker.Journeys.Remove(journey);
            await broker.SaveChangesAsync();

            return journeyEntityEntry.Entity;
        }
    }
}
