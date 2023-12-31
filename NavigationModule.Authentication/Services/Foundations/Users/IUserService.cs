using NavigationModule.Authentication.Models.Entities.Users;
using NavigationModule.Infrastructure.Models.Filters;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> RegisterUserAsync(User user, string password);
        ValueTask<List<User>> RetrieveFilteredUsersAsync(
            Expression<Func<User, bool>> filter,
            Pagination<User, DateTimeOffset> pagination);
        ValueTask<User> RetrieveUserByIdAsync(Guid userId);
        ValueTask<User> RetreiveUserByEmailAsync(string email);
        ValueTask<User> RetreiveUserByUserNameAsync(string username);
        ValueTask<User> ModifyUserAsync(User user);
        ValueTask<User> RemoveUserByIdAsync(Guid userId);
        ValueTask<bool> AssignUserRole(User user, string roleName);
        ValueTask<string> RetreiveUserRoleAsync(User user);
    }
}