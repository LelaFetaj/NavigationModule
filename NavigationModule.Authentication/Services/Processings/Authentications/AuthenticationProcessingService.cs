using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NavigationModule.Authentication.Models.Entities.Users;
using NavigationModule.Authentication.Services.Foundations.Authentications;

namespace NavigationModule.Authentication.Services.Processings.Authentications
{
    sealed class AuthenticationProcessingService : IAuthenticationProcessingService
    {
        private readonly IAuthenticationService authenticationService;
        private const string privateKey = "ask##dljasl!#5sd#!!!2356477<>lojgjasd!123";
        public AuthenticationProcessingService(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public async ValueTask<bool> IsPasswordCorrect(User user, string password) =>
            await this.authenticationService.IsPasswordCorrect(user, password);

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserName),
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(privateKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}