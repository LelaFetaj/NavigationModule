using NavigationModule.Authentication.Models.DTOs.Authentications;
using NavigationModule.Authentication.Models.DTOs.Users;
using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Authentication.Models.Exceptions.Authentications;
using NavigationModule.Authentication.Models.Exceptions.Roles;
using NavigationModule.Authentication.Models.Exceptions.Users;
using NavigationModule.Authentication.Services.Processings.Authentications;
using NavigationModule.Authentication.Services.Processings.Roles;
using NavigationModule.Authentication.Services.Processings.Users;

namespace NavigationModule.Authentication.Services.Orchestrations
{
    public class UserOrchestrationService : IUserOrchestrationService
    {
        private readonly IAuthenticationProcessingService authenticationProcessingService;
        private readonly IUserProcessingService userProcessingService;
        private readonly IRoleProcessingService roleProcessingService;

        public UserOrchestrationService(
            IAuthenticationProcessingService authenticationProcessingService,
            IUserProcessingService userProcessingService,
            IRoleProcessingService roleProcessingService)
        {
            this.authenticationProcessingService = authenticationProcessingService;
            this.userProcessingService = userProcessingService;
            this.roleProcessingService = roleProcessingService;
        }

        public async ValueTask<AuthenticatedResponse> UserLoginAsync(LoginRequest loginRequest)
        {
            var user =
                await this.userProcessingService.RetrieveUserByUsernameAsync(loginRequest?.Username);

            if (user is null)
            {
                throw new InvalidUsernameException();
            }

            await this.authenticationProcessingService.IsPasswordCorrect(user, loginRequest.Password);

            return new AuthenticatedResponse
            {
                AuthenticationToken = this.authenticationProcessingService.GenerateJwtToken(user)
            };
        }

        public async ValueTask<AuthenticatedResponse> UserRegisterAsync(CreateUserRequest registerRequest)
        {
            var storageUser =
                await this.userProcessingService.RetrieveUserByUsernameAsync(registerRequest.Username);

            if (storageUser is not null)
            {
                throw new AlreadyExistsUserException(registerRequest.Username);
            }

            var storageUserByEmail =
                await this.userProcessingService.RetrieveUserByEmailAsync(registerRequest.Email);

            if (storageUserByEmail is not null)
            {
                throw new AlreadyExistsUserException(registerRequest.Email);
            }

            Role role =
                await this.roleProcessingService.RetrieveRoleByNameAsync(registerRequest.RoleName);

            if (role is not null)
            {
                throw new NotFoundRoleException(registerRequest.RoleName);
            }

            var user =
                await this.userProcessingService.CreateUserWithRoleAsync(registerRequest, registerRequest.RoleName);

            return new AuthenticatedResponse
            {
                AuthenticationToken = this.authenticationProcessingService.GenerateJwtToken(user)
            };
        }
    }
}