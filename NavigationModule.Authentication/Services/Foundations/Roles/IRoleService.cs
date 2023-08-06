using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Infrastructure.Models.Filters;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Services.Foundations.Roles
{
    public interface IRoleService
    {
        ValueTask<Role> AddRoleAsync(Role role);
        ValueTask<List<Role>> RetrieveAllRolesAsync(
            Expression<Func<Role, bool>> filter,
            Pagination<Role, string> pagination);
        ValueTask<Role> RetrieveRoleByNameAsync(string roleName);
    }
}