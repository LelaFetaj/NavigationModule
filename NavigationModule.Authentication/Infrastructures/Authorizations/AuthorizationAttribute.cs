using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Authentication.Infrastructures.Authorizations
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute()
            : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { AuthorizationType.Any, Array.Empty<string>() };
        }

        public AuthorizationAttribute(AuthorizationType authorizationType)
            : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { authorizationType, Array.Empty<string>() };
        }

        public AuthorizationAttribute(AuthorizationType authorizationType, params string[] roles)
           : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { authorizationType, roles };
        }
    }
}
