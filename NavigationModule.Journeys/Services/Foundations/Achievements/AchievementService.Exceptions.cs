using NavigationModule.Journeys.Models.Entities.Achievements;
using NavigationModule.Journeys.Models.Exceptions.Journeys;
using Npgsql;

namespace NavigationModule.Journeys.Services.Foundations.Achievements
{
    public partial class AchievementService
    {
        private delegate ValueTask<Achievement> ReturningAchievementFunction();
        private delegate Task TaskFunction();

        private async ValueTask<Achievement> TryCatch(ReturningAchievementFunction returningAchievementFunction)
        {
            try
            {
                return await returningAchievementFunction();
            }
            catch (PostgresException postgresException)
            {
                throw CreateAndLogServiceException(postgresException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private async Task TryCatch(TaskFunction taskFunction)
        {
            try
            {
                 await taskFunction();
            }
            catch (PostgresException postgresException)
            {
                throw CreateAndLogServiceException(postgresException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private AchievementServiceException CreateAndLogServiceException(Exception exception)
        {
            this.loggingBroker.LogError(exception);

            if (exception.InnerException != null)
                this.loggingBroker.LogError(exception?.InnerException);

            return new AchievementServiceException(exception);
        }
    }
}
