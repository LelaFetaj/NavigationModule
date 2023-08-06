using Microsoft.AspNetCore.Identity;
using NavigationModule.Authentication.Brokers.Managements.Users;
using NavigationModule.Authentication.Models.Entities.Users;
using NavigationModule.Infrastructure.Models.Filters;
using NetLoggings.Brokers.Loggings;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Services.Foundations.Users
{
    sealed partial class UserService : IUserService
    {
        private readonly IUserManagerBroker userManagerBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserService(IUserManagerBroker userManagerBroker, ILoggingBroker loggingBroker)
        {
            this.userManagerBroker = userManagerBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<User> RegisterUserAsync(User user, string password) =>
            TryCatch(async () =>
            {
                ValidateUserOnRegister(user, password);

                return await this.userManagerBroker.InsertUserAsync(user, password);
            });

        public ValueTask<List<User>> RetrieveFilteredUsersAsync(
            Expression<Func<User, bool>> filter,
            Pagination<User, DateTimeOffset> pagination) =>
            TryCatch(async () => await this.userManagerBroker.SelectAllUsersAsync(filter, pagination));

        public ValueTask<User> RetrieveUserByIdAsync(Guid userId) =>
            TryCatch(async () =>
            {
                ValidateUserId(userId);
                User maybeUser = await this.userManagerBroker.SelectUserByIdAsync(userId);
                ValidateStorageUser(maybeUser, userId);

                return maybeUser;
            });

        public ValueTask<User> RetreiveUserByEmailAsync(string email) =>
            TryCatch(async () =>
            {
                ValidateUserEmail(email);

                return await this.userManagerBroker.SelectUserByEmailAsync(email);
            });

        public ValueTask<User> RetreiveUserByUserNameAsync(string username) =>
            TryCatch(async () =>
            {
                ValidateUsername(username);

                return await this.userManagerBroker.SelectUserByUserNameAsync(username);
            });

        public ValueTask<User> ModifyUserAsync(User user) =>
            TryCatch(async () =>
            {
                ValidateUserOnModify(user);

                return await this.userManagerBroker.UpdateUserAsync(user);
            });

        public ValueTask<User> RemoveUserByIdAsync(Guid userId) =>
            TryCatch(async () =>
            {
                ValidateUserId(userId);
                User maybeUser = await this.userManagerBroker.SelectUserByIdAsync(userId);
                ValidateStorageUser(maybeUser, userId);

                return await this.userManagerBroker.DeleteUserAsync(maybeUser);
            });

        public ValueTask<bool> AssignUserRole(User user, string roleName) =>
            TryCatch(async () =>
            {
                ValidateUserIsNull(user);
                ValidateRoleName(roleName);

                IdentityResult result =
                    await this.userManagerBroker.AddToRoleAsync(user, roleName);

                return result.Succeeded;
            });

        public ValueTask<string> RetreiveUserRoleAsync(User user) =>
            TryCatch(async () =>
            {
                ValidateUserIsNull(user);

                var roles = await this.userManagerBroker.GetUserRoles(user);

                return roles.FirstOrDefault();
            });
    }
}