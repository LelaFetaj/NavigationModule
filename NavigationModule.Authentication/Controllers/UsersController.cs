using Microsoft.AspNetCore.Mvc;
using NavigationModule.Authentication.Models.DTOs.Authentications;
using NavigationModule.Authentication.Models.DTOs.Users;
using NavigationModule.Authentication.Services.Orchestrations;

namespace NavigationModule.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserOrchestrationService userOrchestrationService;

        public UsersController(IUserOrchestrationService userOrchestrationService)
        {
            this.userOrchestrationService = userOrchestrationService;
        }

        [HttpPut("login")]
        public async ValueTask<ActionResult<AuthenticatedResponse>> Login(LoginRequest loginRequest) =>
            Ok(await this.userOrchestrationService.UserLoginAsync(loginRequest));

        [HttpPost("register")]
        public async ValueTask<ActionResult<AuthenticatedResponse>> Register(CreateUserRequest registerRequest) =>
            Ok(await this.userOrchestrationService.UserRegisterAsync(registerRequest));
    }
}