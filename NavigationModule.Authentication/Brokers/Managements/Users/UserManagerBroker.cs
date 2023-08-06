using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NavigationModule.Authentication.Models.Entities.Users;
using NavigationModule.Infrastructure.Extentions;
using NavigationModule.Infrastructure.Models.Filters;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Brokers.Managements.Users
{
    sealed partial class UserManagerBroker : IUserManagerBroker
    {
        private readonly UserManager<User> userManager;

        public UserManagerBroker(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async ValueTask<User> InsertUserAsync(User user, string password)
        {
            var broker = new UserManagerBroker(this.userManager);
            await broker.userManager.CreateAsync(user, password);

            return user;
        }

        public async ValueTask<List<User>> SelectAllUsersAsync(
            Expression<Func<User, bool>> filter,
            Pagination<User, DateTimeOffset> pagination)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.Users
                .Where(filter)
                .PageBy(pagination)
                .ToListAsync();
        }

        public async ValueTask<User> SelectUserByIdAsync(Guid userId)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.FindByIdAsync(userId.ToString());
        }

        public async ValueTask<User> SelectUserByUserNameAsync(string username)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.FindByNameAsync(username);
        }

        public async ValueTask<User> SelectUserByEmailAsync(string email)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.FindByEmailAsync(email);
        }

        public async ValueTask<User> UpdateUserAsync(User user)
        {
            var broker = new UserManagerBroker(this.userManager);
            await broker.userManager.UpdateAsync(user);

            return user;
        }

        public async ValueTask<User> DeleteUserAsync(User user)
        {
            var broker = new UserManagerBroker(this.userManager);
            await broker.userManager.DeleteAsync(user);

            return user;
        }
    }
}