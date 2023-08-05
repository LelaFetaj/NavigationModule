using NetXceptions;
using NetXceptions.Models.Types;
using System.Runtime.Serialization;

namespace NavigationModule.Journeys.Models.Exceptions.Journeys
{
    [Serializable]
    public class NotFoundJourneyException : NetXception, IValidationException
    {
        public NotFoundJourneyException(Guid routeId)
            : base(message: $"Couldn't find journey with id: {routeId}.")
        { }

        protected NotFoundJourneyException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
    }
}
