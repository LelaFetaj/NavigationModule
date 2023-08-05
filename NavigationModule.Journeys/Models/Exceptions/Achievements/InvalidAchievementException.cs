using NetXceptions;
using NetXceptions.Models.Types;
using System.Runtime.Serialization;

namespace NavigationModule.Journeys.Models.Exceptions.Journeys
{
    [Serializable]
    public class InvalidAchievementException : NetXception, IValidationException
    {
        protected InvalidAchievementException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidAchievementException(string parameterName, object parameterValue)
           : base(message: $"Invalid achievement, " +
                 $"parameter name: {parameterName}, " +
                 $"parameter value: {parameterValue}.")
        { }

        public InvalidAchievementException()
            : base(message: "Invalid achievement. Please fix the errors and try again.") { }

    }
}
