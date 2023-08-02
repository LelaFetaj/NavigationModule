using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class InvalidUserException : NetXception, IValidationException
    {
        protected InvalidUserException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidUserException(string parameterName, object parameterValue)
           : base(message: $"Invalid user, " +
                 $"parameter name: {parameterName}, " +
                 $"parameter value: {parameterValue}.")
        { }

        public InvalidUserException()
            : base(message: "Invalid user. Please fix the errors and try again.") { }
    }
}