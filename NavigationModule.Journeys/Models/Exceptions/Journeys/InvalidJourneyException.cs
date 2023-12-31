﻿using NetXceptions;
using NetXceptions.Models.Types;
using System.Runtime.Serialization;

namespace NavigationModule.Journeys.Models.Exceptions.Journeys
{
    [Serializable]
    public class InvalidJourneyException : NetXception, IValidationException
    {
        protected InvalidJourneyException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidJourneyException(string parameterName, object parameterValue)
           : base(message: $"Invalid journey, " +
                 $"parameter name: {parameterName}, " +
                 $"parameter value: {parameterValue}.")
        { }

        public InvalidJourneyException()
            : base(message: "Invalid journey. Please fix the errors and try again.") { }

    }
}
