using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NavigationModule.Authentication.Models.DTOs.Roles;
using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Authentication.Services.Processings.Roles;
using NavigationModule.Infrastructure.Infrastructures.Authorizations;

namespace NavigationModule.Authentication.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleProcessingService roleProcessingService;

        public RolesController(IRoleProcessingService roleProcessingService)
        {
            this.roleProcessingService = roleProcessingService;
        }

        [Authorization(AuthorizationType.All, "Admin")]
        [HttpPost]
        public async ValueTask<ActionResult<Role>> CreateRole(RoleRequest roleRequest) =>
            Ok(await this.roleProcessingService.AddRoleAsync(roleRequest.Name));

        [HttpGet]
        public async ValueTask<ActionResult<Role>> RetrieveAllRoles(
            string search,
            int page = 1,
            int pageSize = 10,
            bool orderByDesceding = true) =>
            Ok(await this.roleProcessingService.RetrieveAllRolesAsync(search, page, pageSize, orderByDesceding));

        [HttpGet("{roleName}")]
        public async ValueTask<ActionResult<Role>> RetrieveRoleByName(string roleName) =>
            Ok(await this.roleProcessingService.RetrieveRoleByNameAsync(roleName));
    }
}