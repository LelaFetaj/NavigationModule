using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class NullUserException : NetXception, IValidationException
    {
        protected NullUserException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NullUserException()
            : base(message: "The user is empty.") { }

        public NullUserException(string message)
            : base(message) { }
    }
}