using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Authentications
{
    [Serializable]
    public class AuthenticationServiceException : NetXception, IFailedDependencyException
    {
        protected AuthenticationServiceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public AuthenticationServiceException(Exception innerException)
             : base(message: "An error with sign in occurred, contact support.", innerException) { }
    }
}