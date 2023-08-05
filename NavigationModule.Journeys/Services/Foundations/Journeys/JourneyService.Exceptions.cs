using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NavigationModule.Journeys.Models.Exceptions.Journeys;
using Npgsql;

namespace NavigationModule.Journeys.Services.Foundations.Journeys
{
    public partial class JourneyService
    {
        private delegate ValueTask<Journey> ReturningJourneyFunction();
        private delegate ValueTask<bool> ReturningBoolFunction();
        private delegate ValueTask<(List<Journey>, long count)> ReturningJourneysFunction();
        private delegate ValueTask<List<UserStats>> ReturningUserStatsFunction();

        private async ValueTask<Journey> TryCatch(ReturningJourneyFunction returningJourneyFunction)
        {
            try
            {
                return await returningJourneyFunction();
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

        private async ValueTask<(List<Journey>, long count)> TryCatch(
            ReturningJourneysFunction returningJourneyFunction)
        {
            try
            {
                return await returningJourneyFunction();
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

        private async ValueTask<bool> TryCatch(ReturningBoolFunction returningBoolFunction)
        {
            try
            {
                return await returningBoolFunction();
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

        private async ValueTask<List<UserStats>> TryCatch(
            ReturningUserStatsFunction returningUserStatFunction)
        {
            try
            {
                return await returningUserStatFunction();
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

        private JourneyServiceException CreateAndLogServiceException(Exception exception)
        {
            this.loggingBroker.LogError(exception);

            if (exception.InnerException != null)
                this.loggingBroker.LogError(exception?.InnerException);

            return new JourneyServiceException(exception);
        }
    }
}
