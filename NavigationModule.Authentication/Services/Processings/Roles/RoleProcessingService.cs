using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Authentication.Models.Exceptions.Roles;
using NavigationModule.Authentication.Services.Foundations.Roles;
using NavigationModule.Infrastructure.Models.Filters;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Services.Processings.Roles
{
    sealed class RoleProcessingService : IRoleProcessingService
    {
        private readonly IRoleService roleService;

        public RoleProcessingService(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async ValueTask<Role> AddRoleAsync(string roleName)
        {
            var role = await this.roleService.RetrieveRoleByNameAsync(roleName);

            if (role is not null)
            {
                throw new AlreadyExistsRoleException(roleName);
            }

            Role newRole = new() { Name = roleName };

            return await this.roleService.AddRoleAsync(newRole);
        }

        public async ValueTask<List<Role>> RetrieveAllRolesAsync(
            string search,
            int page = 1,
            int pageSize = 10,
            bool orderByDesceding = true)
        {
            var pagination = new Pagination<Role, string>()
            {
                Page = page,
                PageSize = pageSize,
                OrderByDescending = orderByDesceding,
                OrderBy = role => role.Name
            };

            Expression<Func<Role, bool>> userFilter =
                user => string.IsNullOrWhiteSpace(search)
                    || user.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase);

            return await this.roleService.RetrieveAllRolesAsync(
                filter: userFilter,
                pagination: pagination);
        }

        public ValueTask<Role> RetrieveRoleByNameAsync(string roleName) =>
            this.roleService.RetrieveRoleByNameAsync(roleName);
    }
}