﻿using NetXceptions;
using NetXceptions.Models.Types;
using System.Runtime.Serialization;

namespace NavigationModule.Journeys.Models.Exceptions.Journeys
{
    [Serializable]
    public class NullJourneyException : NetXception, IValidationException
    {
        protected NullJourneyException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NullJourneyException()
            : base(message: "The journey is empty.") { }

        public NullJourneyException(string message)
            : base(message) { }
    }
}
