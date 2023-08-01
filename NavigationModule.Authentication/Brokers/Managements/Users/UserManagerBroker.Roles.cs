using Microsoft.AspNetCore.Identity;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Managements.Users
{
    sealed partial class UserManagerBroker
    {
        public async ValueTask<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.AddToRoleAsync(user, roleName);
        }

        public async ValueTask<IEnumerable<string>> GetUserRoles(User user)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.GetRolesAsync(user);
        }

        public async ValueTask<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.RemoveFromRoleAsync(user, role);
        }

        public async ValueTask<bool> IsInRoleAsync(User user, string role)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.IsInRoleAsync(user, role);
        }

        public async ValueTask<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.GetUsersInRoleAsync(roleName);
        }
    }
}