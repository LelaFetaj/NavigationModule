using NetXceptions.Validations;
using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Authentication.Models.Exceptions.Roles;

namespace NavigationModule.Authentication.Services.Foundations.Roles
{
    sealed partial class RoleService
    {
        private static void ValidateRoleOnCreate(Role role)
        {
            ValidateRoleIsNull(role);

            var invalidRoleException = new InvalidRoleException();

            invalidRoleException.Validate(
                (Rule: ModelValidator.IsInvalid(role.Name, nameof(Role.Name)), Parameter: nameof(Role.Name)));
        }

        private static void ValidateRoleIsNull(Role role)
        {
            if (role is null)
            {
                throw new NullRoleException();
            }
        }

        private static void ValidateRoleName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new InvalidRoleException(
                    parameterName: "Role name",
                    parameterValue: roleName);
            }
        }

    }
}