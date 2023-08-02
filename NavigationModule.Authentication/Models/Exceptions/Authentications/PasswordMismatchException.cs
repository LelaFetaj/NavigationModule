using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Authentications
{
    [Serializable]
    public class PasswordMismatchException : NetXception, IUnauthorizedException
    {
        protected PasswordMismatchException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public PasswordMismatchException()
            : base("Passowrds do not match.")
        {
        }

        public PasswordMismatchException(string message)
            : base(message)
        { }
    }
}