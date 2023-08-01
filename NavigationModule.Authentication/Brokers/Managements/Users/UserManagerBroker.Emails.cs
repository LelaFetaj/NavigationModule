using Microsoft.AspNetCore.Identity;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Managements.Users
{
    sealed partial class UserManagerBroker
    {
        public async ValueTask<bool> IsEmailConfirmedAsync(User user)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.IsEmailConfirmedAsync(user);
        }

        public async ValueTask<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async ValueTask<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async ValueTask<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.ConfirmEmailAsync(user, token);
        }

        public async ValueTask<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.ChangeEmailAsync(user, newEmail, token);
        }
    }
}