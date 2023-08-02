using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Authentications
{
    [Serializable]
    public class InvalidPasswordException : NetXception, IUnauthorizedException
    {
        protected InvalidPasswordException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidPasswordException()
            : base("Passowrd is incorrect.")
        {
        }

        public InvalidPasswordException(string message)
            : base(message)
        { }
    }
}