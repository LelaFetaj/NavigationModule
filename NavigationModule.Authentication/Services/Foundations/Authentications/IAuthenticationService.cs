using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Services.Foundations.Authentications
{
    public interface IAuthenticationService
    {
        ValueTask<bool> IsPasswordCorrect(
            User user,
            string password,
            bool lockoutOnFailure = false);
    }
}