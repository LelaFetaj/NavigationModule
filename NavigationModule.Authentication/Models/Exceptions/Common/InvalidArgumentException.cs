using NetXceptions;
using System.Runtime.Serialization;

namespace NavigationModule.Authentication.Models.Exceptions.Common
{
    [Serializable]
    public class InvalidArgumentException : NetXception
    {
        protected InvalidArgumentException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidArgumentException()
            : base(message: "Invalid argument. Please fix the errors and try again.")
        { }

        public InvalidArgumentException(string parameterName)
            : base(message: $"Invalid argument, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: empty.")
        { }

        public InvalidArgumentException(string parameterName, object parameterValue)
            : base(message: $"Invalid argument, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }
    }
}
