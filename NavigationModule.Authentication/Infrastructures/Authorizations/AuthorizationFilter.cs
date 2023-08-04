﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace NavigationModule.Authentication.Infrastructures.Authorizations
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly AuthorizationType authorizationType;
        private readonly IConfiguration configuration;
        private readonly IEnumerable<string> roleClaims;

        public AuthorizationFilter(
            AuthorizationType authorizationType,
            params string[] roleClaims)
        {
            this.authorizationType = authorizationType;
            this.roleClaims = roleClaims;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            HttpContext httpContext = context.HttpContext;

            IEnumerable<Claim> claims = GetClaimsFromToken(httpContext);

            string[] providedRoles = DeserializeValueFromType(
                claims,
                claimType: ClaimTypes.Role);

            if (providedRoles is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            bool isAuthorized = (authorizationType == AuthorizationType.Any)
                ? roleClaims.Any(role => providedRoles.Contains(role))
                : roleClaims.All(role => providedRoles.Contains(role));

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private static IEnumerable<Claim> GetClaimsFromToken(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers[HeaderNames.Authorization].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token))
            {
                return default;
            }

            var handler = new JsonWebTokenHandler();

            token = token.Replace("Bearer ", "");

            return handler.ReadJsonWebToken(token)?.Claims;
        }

        private static string[] DeserializeValueFromType(IEnumerable<Claim> claims, string claimType)
        {
            string tokenClaimJson = claims.FirstOrDefault(x => x.Type == claimType)?.Value;

            if (string.IsNullOrWhiteSpace(tokenClaimJson))
            {
                return default;
            }

            return JsonSerializer.Deserialize<string[]>(
                json: tokenClaimJson,
                options: new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
