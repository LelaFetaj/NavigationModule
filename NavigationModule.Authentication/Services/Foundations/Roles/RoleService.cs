using NavigationModule.Authentication.Brokers.Managements.Roles;
using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Infrastructure.Models.Filters;
using NetLoggings.Brokers.Loggings;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Services.Foundations.Roles
{
    sealed partial class RoleService : IRoleService
    {
        private readonly IRoleManagerBroker roleManagerBroker;
        private readonly ILoggingBroker loggingBroker;

        public RoleService(IRoleManagerBroker roleManagerBroker, ILoggingBroker loggingBroker)
        {
            this.roleManagerBroker = roleManagerBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Role> AddRoleAsync(Role role) =>
            TryCatch(async () =>
            {
                ValidateRoleOnCreate(role);

                return await this.roleManagerBroker.InsertRoleAsync(role);
            });

        public ValueTask<List<Role>> RetrieveAllRolesAsync(
            Expression<Func<Role, bool>> filter,
            Pagination<Role, string> pagination) =>
            TryCatch(async () => await this.roleManagerBroker.SelectAllRolesAsync(filter, pagination));

        public ValueTask<Role> RetrieveRoleByNameAsync(string roleName) =>
            TryCatch(async () =>
            {
                ValidateRoleName(roleName);

                return await this.roleManagerBroker.SelectRoleByNameAsync(roleName);
            });
    }
}