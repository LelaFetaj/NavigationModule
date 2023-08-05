using Microsoft.AspNetCore.Http;
using NavigationModule.Journeys.Brokers.DateTimes;
using NavigationModule.Journeys.Models.DTOs.Journeys;
using NavigationModule.Journeys.Models.Entities.Achievements;
using NavigationModule.Journeys.Services.Foundations.Achievements;
using System.Security.Claims;

namespace NavigationModule.Journeys.Services.Processings.Achievements
{
    public class AchievementProcessingService : IAchievementProcessingService
    {
        private readonly IAchievementService achievementService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDateTimeBroker dateTimeBroker;

        public AchievementProcessingService(
            IAchievementService achievementService,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeBroker dateTimeBroker)
        {
            this.achievementService = achievementService;
            this.httpContextAccessor = httpContextAccessor;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async Task UpsertAchievementAsync(JourneyRequest journeyRequest)
        {
            string userId =
                this.httpContextAccessor.HttpContext.User.FindFirst(
                    type: ClaimTypes.NameIdentifier)?.Value;

            Achievement maybeAchievement = await this.achievementService.RetrieveAchievementAsync(userId);

            if (maybeAchievement is null)
            {
                var newAchievement = new Achievement
                {
                    DailyDistance = journeyRequest.Distance,
                    IsRewarded = journeyRequest.Distance > 20,
                    UpdatedDate = this.dateTimeBroker.GetCurrentDateTime(),
                    UserId = userId
                };

                await this.achievementService.GenerateAchievementAsync(newAchievement);
            }
            else if(!maybeAchievement.IsRewarded || 
                DateOnly.FromDateTime(maybeAchievement.UpdatedDate.Date) 
                != this.dateTimeBroker.GetDateOnly())
            {
                if (DateOnly.FromDateTime(maybeAchievement.UpdatedDate.Date) 
                    != this.dateTimeBroker.GetDateOnly())
                {
                    maybeAchievement.DailyDistance = journeyRequest.Distance;
                    maybeAchievement.IsRewarded = journeyRequest.Distance > 20;
                    maybeAchievement.UpdatedDate = this.dateTimeBroker.GetCurrentDateTime();
                }
                else 
                {
                    maybeAchievement.DailyDistance += journeyRequest.Distance;
                    maybeAchievement.UpdatedDate = this.dateTimeBroker.GetCurrentDateTime();
                    maybeAchievement.IsRewarded = maybeAchievement.DailyDistance > 20;
                }

                await this.achievementService.UpdateAchievementAsync(maybeAchievement);
            }
        }
    }
}
