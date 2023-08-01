using Microsoft.AspNetCore.Identity;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Managements.Authentications
{
    public interface IAuthenticationManagerBroker
    {
        ValueTask<SignInResult> CheckPasswordSignInAsync(
           User user,
           string password,
           bool lockoutOnFailure);
    }
}
