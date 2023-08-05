using Microsoft.EntityFrameworkCore;
using NavigationModule.Journeys.Models.Entities.Journeys;

namespace NavigationModule.Journeys.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SetJourneyProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journey>(journey =>
            {
                journey.HasIndex(x => x.UserId);
                journey.HasIndex(x => x.Id);
                journey.HasIndex(x => x.ArrivalDate);

                journey.Property(l => l.StartingPoint)
                    .HasColumnType("jsonb");

                journey.Property(l => l.ArrivalPoint)
                    .HasColumnType("jsonb");
            });
        }
    }
}
