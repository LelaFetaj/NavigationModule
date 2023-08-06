using NavigationModule.Authentication.Brokers.DateTimes;
using NavigationModule.Authentication.Models.DTOs.Users;
using NavigationModule.Authentication.Models.Entities.Users;
using NavigationModule.Authentication.Services.Foundations.Users;
using NavigationModule.Infrastructure.Models.Filters;
using System.Linq.Expressions;

namespace NavigationModule.Authentication.Services.Processings.Users
{
    sealed partial class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;

        public UserProcessingService(IUserService userService, IDateTimeBroker dateTimeBroker)
        {
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async ValueTask<User> CreateUserWithRoleAsync(CreateUserRequest createUserRequest, string roleName)
        {
            ValidateUserAndRole(createUserRequest, roleName);

            var user = new User
            {
                Email = createUserRequest.Email,
                UserName = createUserRequest.Username,
                FirstName = createUserRequest.Firstname,
                LastName = createUserRequest.Lastname,
                PhoneNumber = createUserRequest.PhoneNumber,
                Gender = createUserRequest.Gender,
                BirthDate = createUserRequest.BirthDate?.ToDateTime(TimeOnly.MinValue).ToUniversalTime(),
                CreatedDate = this.dateTimeBroker.GetCurrentDateTime(),
                UpdatedDate = this.dateTimeBroker.GetCurrentDateTime()
            };

            _ = await this.userService.RegisterUserAsync(user, createUserRequest.Password);

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                await this.userService.AssignUserRole(user, roleName);
            }

            return user;
        }

        public async ValueTask<List<User>> RetrieveFilteredUsersAysnc(
            string search,
            int page = 1,
            int pageSize = 10,
            bool orderByDesceding = true)
        {
            var pagination = new Pagination<User, DateTimeOffset>()
            {
                Page = page,
                PageSize = pageSize,
                OrderByDescending = orderByDesceding,
                OrderBy = user => user.UpdatedDate
            };

            Expression<Func<User, bool>> userFilter =
                user => string.IsNullOrWhiteSpace(search)
                    || user.UserName.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                    || user.Email.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                    || user.FirstName.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                    || user.LastName.Contains(search, StringComparison.InvariantCultureIgnoreCase);

            return await this.userService.RetrieveFilteredUsersAsync(
                filter: userFilter,
                pagination: pagination);
        }

        public async ValueTask<User> RetrieveUserByUsernameAsync(string username) =>
            await this.userService.RetreiveUserByUserNameAsync(username);

        public async ValueTask<User> RetrieveUserByEmailAsync(string email) =>
            await this.userService.RetreiveUserByEmailAsync(email);

        public async ValueTask<string> RetrieveUserRoleAsync(User user) =>
            await this.userService.RetreiveUserRoleAsync(user);

        public async ValueTask<User> ModifyUserAsync(ModifyUserRequest modifyUserRequest)
        {
            ValidateUserRequest(modifyUserRequest);
            User storageUser = await this.userService.RetrieveUserByIdAsync(modifyUserRequest.Id);

            storageUser.Email = string.IsNullOrWhiteSpace(modifyUserRequest.Email)
                ? storageUser.Email
                : modifyUserRequest.Email;

            storageUser.UserName = string.IsNullOrWhiteSpace(modifyUserRequest.Username)
                ? storageUser.UserName
                : modifyUserRequest.Username;

            storageUser.FirstName = string.IsNullOrWhiteSpace(modifyUserRequest.Firstname)
                ? storageUser.FirstName
                : modifyUserRequest.Firstname;

            storageUser.LastName = string.IsNullOrWhiteSpace(modifyUserRequest.Lastname)
                ? storageUser.LastName
                : modifyUserRequest.Lastname;

            storageUser.PhoneNumber = string.IsNullOrWhiteSpace(modifyUserRequest.PhoneNumber)
                ? storageUser.PhoneNumber
                : modifyUserRequest.PhoneNumber;

            storageUser.Gender = modifyUserRequest.Gender == Gender.Other
                ? storageUser.Gender
                : modifyUserRequest.Gender;

            storageUser.BirthDate = modifyUserRequest.BirthDate == default
                ? storageUser.BirthDate
                : modifyUserRequest.BirthDate;

            storageUser.UpdatedDate = this.dateTimeBroker.GetCurrentDateTime();

            return await this.userService.ModifyUserAsync(storageUser);
        }

        public async ValueTask<User> RemoveUserByIdAsync(Guid userId) =>
            await this.userService.RemoveUserByIdAsync(userId);
    }
}