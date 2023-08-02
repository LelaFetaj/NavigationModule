using Microsoft.AspNetCore.Identity;
using NavigationModule.Authentication.Models.Exceptions.Authentications;

namespace NavigationModule.Authentication.Services.Foundations.Authentications
{
    sealed partial class AuthenticationService
    {
        private static void ThrowExceptionIfFailed(SignInResult result)
        {
            if (!result.Succeeded)
            {
                throw new InvalidPasswordException();
            }
        }
    }
}