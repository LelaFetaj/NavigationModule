using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Managements.Users
{
    sealed partial class UserManagerBroker
    {
        public async ValueTask<bool> IsPhoneNumberConfirmedAsync(User user)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.IsPhoneNumberConfirmedAsync(user);
        }

        public async ValueTask<string> GenerateChangePhoneNumberTokenAsync(
            User user,
            string phoneNumber)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        }

        public async ValueTask<bool> VerifyChangePhoneNumberTokenAsync(
            User user,
            string token,
            string phoneNumber)
        {
            var broker = new UserManagerBroker(this.userManager);

            return await broker.userManager.VerifyChangePhoneNumberTokenAsync(
                user,
                token,
                phoneNumber);
        }
    }
}