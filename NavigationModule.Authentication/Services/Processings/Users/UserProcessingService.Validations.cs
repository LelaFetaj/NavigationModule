using NavigationModule.Authentication.Models.DTOs.Users;
using NavigationModule.Authentication.Models.Exceptions.Authentications;
using NavigationModule.Authentication.Models.Exceptions.Users;

namespace NavigationModule.Authentication.Services.Processings.Users
{
    sealed partial class UserProcessingService
    {
        private static void ValidateUserAndRole(CreateUserRequest createUserRequest, string roleName)
        {
            ValidateUserIsNull(createUserRequest);
            ValidatePasswordMatch(createUserRequest.Password, createUserRequest.VerifyPassword);
            //ValidateRoleName(roleName);

        }

        private static void ValidatePasswordMatch(string password, string verifyPassword)
        {
            if (password != verifyPassword)
                throw new PasswordMismatchException();
        }

        private static void ValidateUserRequest(ModifyUserRequest modifyUserRequest)
        {
            ValidateUserIsNull(modifyUserRequest);
            ValidateUserId(modifyUserRequest.Id);
        }

        private static void ValidateUserIsNull(CreateUserRequest user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }

        private static void ValidateUserIsNull(ModifyUserRequest user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }

        private static void ValidateUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new InvalidUserException(
                    parameterName: nameof(ModifyUserRequest.Id),
                    parameterValue: userId);
            }
        }

        private static void ValidateRoleName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new InvalidUserException(
                    parameterName: "Role name",
                    parameterValue: roleName);
            }
        }
    }
}