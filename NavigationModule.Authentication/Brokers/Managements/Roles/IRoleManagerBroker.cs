using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Infrastructure.Models.Filters;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Brokers.Managements.Roles
{
    public interface IRoleManagerBroker
    {
        ValueTask<Role> InsertRoleAsync(Role role);
        ValueTask<List<Role>> SelectAllRolesAsync(
            Expression<Func<Role, bool>> filter,
            Pagination<Role, string> pagination);
        ValueTask<Role> SelectRoleByNameAsync(string roleName);
    }
}
