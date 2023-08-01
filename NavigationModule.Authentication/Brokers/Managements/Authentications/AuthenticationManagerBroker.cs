using Microsoft.AspNetCore.Identity;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Managements.Authentications
{
    sealed class AuthenticationManagerBroker : IAuthenticationManagerBroker
    {
        private readonly SignInManager<User> signInManager;

        public AuthenticationManagerBroker(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async ValueTask<SignInResult> CheckPasswordSignInAsync(
            User user,
            string password,
            bool lockoutOnFailure)
        {
            return await this.signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure);
        }
    }
}
