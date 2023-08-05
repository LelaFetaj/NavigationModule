using Microsoft.IdentityModel.Tokens;
using NavigationModule.Journeys.Models.Entities.Achievements;
using NavigationModule.Journeys.Models.Exceptions.Journeys;
using NetXceptions.Validations;

namespace NavigationModule.Journeys.Services.Foundations.Achievements
{
    public partial class AchievementService
    {
        private static void ValidateAchievement(Achievement achievement)
        {
            ValidateAchievementIsNull(achievement);

            var invalidAchievementException = new InvalidAchievementException();

            invalidAchievementException.Validate(
                (Rule: ModelValidator.IsInvalid(achievement.UserId, nameof(Achievement.UserId)), Parameter: nameof(Achievement.UserId)),
                (Rule: ModelValidator.IsInvalid(achievement.UpdatedDate, nameof(Achievement.UpdatedDate)), Parameter: nameof(Achievement.UpdatedDate))
                );
        }

        private static void ValidateAchievementIsNull(Achievement achievement)
        {
            if (achievement is null)
            {
                throw new NullAchievementException();
            }
        }

        private static void ValidateUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidAchievementException(
                    parameterName: nameof(Achievement.UserId),
                    parameterValue: userId);
            }
        }
    }
}
