using Microsoft.AspNetCore.Identity;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Managements.Users
{
    public partial interface IUserManagerBroker
    {
        ValueTask<IdentityResult> AddToRoleAsync(User user, string roleName);
        ValueTask<IEnumerable<string>> GetUserRoles(User user);
        ValueTask<IdentityResult> RemoveFromRoleAsync(User user, string role);
        ValueTask<bool> IsInRoleAsync(User user, string role);
        ValueTask<IList<User>> GetUsersInRoleAsync(string roleName);
    }
}