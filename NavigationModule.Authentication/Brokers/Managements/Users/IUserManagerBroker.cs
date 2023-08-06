using NavigationModule.Authentication.Models.Entities.Users;
using NavigationModule.Infrastructure.Models.Filters;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Brokers.Managements.Users
{
    public partial interface IUserManagerBroker
    {
        ValueTask<User> InsertUserAsync(User user, string password);
        ValueTask<List<User>> SelectAllUsersAsync(
            Expression<Func<User, bool>> filter,
            Pagination<User, DateTimeOffset> pagination);
        ValueTask<User> SelectUserByIdAsync(Guid userId);
        ValueTask<User> SelectUserByUserNameAsync(string username);
        ValueTask<User> SelectUserByEmailAsync(string email);
        ValueTask<User> UpdateUserAsync(User user);
        ValueTask<User> DeleteUserAsync(User user);
    }
}