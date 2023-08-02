using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Authentications
{
    public class InvalidUsernameException : NetXception, IUnauthorizedException
    {
        protected InvalidUsernameException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidUsernameException()
            : base("The username does not exist.")
        {
        }

        public InvalidUsernameException(string message)
            : base(message)
        { }
    }
}