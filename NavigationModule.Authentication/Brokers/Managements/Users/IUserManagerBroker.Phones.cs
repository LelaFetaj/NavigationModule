using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Managements.Users
{
    public partial interface IUserManagerBroker
    {
        ValueTask<bool> IsPhoneNumberConfirmedAsync(User user);
        ValueTask<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber);
        ValueTask<bool> VerifyChangePhoneNumberTokenAsync(User user, string token, string phoneNumber);
    }
}