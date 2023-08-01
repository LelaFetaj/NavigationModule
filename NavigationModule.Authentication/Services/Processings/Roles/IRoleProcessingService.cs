using NavigationModule.Authentication.Models.Entities.Roles;

namespace NavigationModule.Authentication.Services.Processings.Roles
{
    public interface IRoleProcessingService
    {
        ValueTask<Role> AddRoleAsync(string roleName);
        ValueTask<List<Role>> RetrieveAllRolesAsync(
            string search,
            int page = 1,
            int pageSize = 10,
            bool orderByDesceding = true);
        ValueTask<Role> RetrieveRoleByNameAsync(string roleName);
    }
}