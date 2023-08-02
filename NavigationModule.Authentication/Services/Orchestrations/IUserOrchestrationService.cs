using NavigationModule.Authentication.Models.DTOs.Authentications;
using NavigationModule.Authentication.Models.DTOs.Users;

namespace NavigationModule.Authentication.Services.Orchestrations
{
    public interface IUserOrchestrationService
    {
        ValueTask<AuthenticatedResponse> UserLoginAsync(LoginRequest loginRequest);
        ValueTask<AuthenticatedResponse> UserRegisterAsync(CreateUserRequest registerRequest);
    }
}