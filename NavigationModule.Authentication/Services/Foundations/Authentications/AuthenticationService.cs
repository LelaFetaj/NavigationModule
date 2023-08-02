using NetLoggings.Brokers.Loggings;
using NavigationModule.Authentication.Models.Entities.Users;
using NavigationModule.Authentication.Brokers.Managements.Authentications;

namespace NavigationModule.Authentication.Services.Foundations.Authentications
{
    sealed partial class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationManagerBroker authenticationManagerBroker;
        private readonly ILoggingBroker loggingBroker;

        public AuthenticationService(
            IAuthenticationManagerBroker authenticationManagerBroker,
            ILoggingBroker loggingBroker)
        {
            this.authenticationManagerBroker = authenticationManagerBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<bool> IsPasswordCorrect(
            User user,
            string password,
            bool lockoutOnFailure = false) =>
            TryCatch(async () =>
            {
                var result = await this.authenticationManagerBroker.CheckPasswordSignInAsync(
                    user,
                    password,
                    lockoutOnFailure);

                ThrowExceptionIfFailed(result);

                return result.Succeeded;
            });
    }
}