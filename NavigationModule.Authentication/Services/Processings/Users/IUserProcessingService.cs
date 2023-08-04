using NavigationModule.Authentication.Models.DTOs.Users;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask<User> CreateUserWithRoleAsync(CreateUserRequest createUserRequest, string roleName);
        ValueTask<List<User>> RetrieveFilteredUsersAysnc(
            string search,
            int page = 1,
            int pageSize = 10,
            bool orderByDesceding = true);
        ValueTask<User> RetrieveUserByUsernameAsync(string username);
        ValueTask<User> RetrieveUserByEmailAsync(string email);
        ValueTask<string> RetrieveUserRoleAsync(User user);
        ValueTask<User> ModifyUserAsync(ModifyUserRequest modifyUserRequest);
        ValueTask<User> RemoveUserByIdAsync(Guid userId);
    }
}