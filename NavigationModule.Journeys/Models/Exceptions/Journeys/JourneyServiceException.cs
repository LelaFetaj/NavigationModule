using NetXceptions.Models.Types;
using NetXceptions;
using System.Runtime.Serialization;

namespace NavigationModule.Journeys.Models.Exceptions.Journeys
{
    [Serializable]
    public class JourneyServiceException : NetXception, IFailedDependencyException
    {
        protected JourneyServiceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public JourneyServiceException(Exception innerException)
            : base(message: "Journey service error occurred, contact support.", innerException) { }
    }
}
