using Microsoft.AspNetCore.Identity;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Managements.Users
{
    public partial interface IUserManagerBroker
    {
        ValueTask<bool> IsEmailConfirmedAsync(User user);
        ValueTask<string> GenerateEmailConfirmationTokenAsync(User user);
        ValueTask<string> GenerateChangeEmailTokenAsync(User user, string newEmail);
        ValueTask<IdentityResult> ConfirmEmailAsync(User user, string token);
        ValueTask<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);
    }
}