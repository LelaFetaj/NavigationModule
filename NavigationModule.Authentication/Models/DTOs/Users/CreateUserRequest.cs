using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Models.DTOs.Users
{
    public class CreateUserRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string VerifyPassword { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Other;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
    }
}