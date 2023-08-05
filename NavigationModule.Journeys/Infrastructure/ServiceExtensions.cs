using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NavigationModule.Journeys.Brokers.DateTimes;
using NavigationModule.Journeys.Brokers.Storages;
using NavigationModule.Journeys.Services.Foundations.Achievements;
using NavigationModule.Journeys.Services.Foundations.Journeys;
using NavigationModule.Journeys.Services.Orchestrations.Journeys;
using NavigationModule.Journeys.Services.Processings.Achievements;
using NavigationModule.Journeys.Services.Processings.Journeys;

namespace NavigationModule.Extensions
{
    public static partial class ServiceExtensions
    {
        public static void AddJourneyContext(this IServiceCollection services)
        {
            services.AddDbContext<StorageBroker>();
        }

        public static void AddJourneyBrokers(this IServiceCollection services)
        {
            services.AddScoped<IStorageBroker, StorageBroker>();
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
        }

        public static void AddJourneyServices(this IServiceCollection services)
        {
            services.AddTransient<IJourneyService, JourneyService>();
            services.AddTransient<IJourneyProcessingService, JourneyProcessingService>();
            services.AddTransient<IJourneyOrchestrationService, JourneyOrchestrationService>();
            services.AddTransient<IAchievementService, AchievementService>();
            services.AddTransient<IAchievementProcessingService, AchievementProcessingService>();
        }
    }
}
