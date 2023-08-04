using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Services.Processings.Authentications
{
    public interface IAuthenticationProcessingService
    {
        ValueTask<bool> IsPasswordCorrect(User user, string password);
        public string GenerateJwtToken(User user, string role);
    }
}