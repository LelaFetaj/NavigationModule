using NetXceptions;
using Npgsql;
using NavigationModule.Authentication.Models.Exceptions.Authentications;

namespace NavigationModule.Authentication.Services.Foundations.Authentications
{
    sealed partial class AuthenticationService
    {
        private delegate ValueTask<bool> ReturningBooleanFunction();

        private async ValueTask<bool> TryCatch(ReturningBooleanFunction returningBooleanFunction)
        {
            try
            {
                return await returningBooleanFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                throw CreateAndLogServiceException(npgsqlException);
            }
            catch (Exception exception)
                when (exception is not NetXception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private AuthenticationServiceException CreateAndLogServiceException(Exception exception)
        {
            this.loggingBroker.LogError(exception);

            return new AuthenticationServiceException(exception);
        }
    }
}