using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Infrastructure.Extentions;
using NavigationModule.Infrastructure.Models.Filters;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Brokers.Managements.Roles
{
    public class RoleManagerBroker : IRoleManagerBroker
    {
        private readonly RoleManager<Role> roleManager;

        public RoleManagerBroker(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async ValueTask<Role> InsertRoleAsync(Role role)
        {
            var broker = new RoleManagerBroker(this.roleManager);
            await broker.roleManager.CreateAsync(role);

            return role;
        }

        public async ValueTask<List<Role>> SelectAllRolesAsync(
            Expression<Func<Role, bool>> filter,
            Pagination<Role, string> pagination)
        {
            var broker = new RoleManagerBroker(this.roleManager);

            return await broker.roleManager.Roles
                .Where(filter)
                .PageBy(pagination)
                .ToListAsync();
        }

        public async ValueTask<Role> SelectRoleByNameAsync(string roleName)
        {
            var broker = new RoleManagerBroker(this.roleManager);

            return await broker.roleManager.FindByNameAsync(roleName);
        }
    }
}
