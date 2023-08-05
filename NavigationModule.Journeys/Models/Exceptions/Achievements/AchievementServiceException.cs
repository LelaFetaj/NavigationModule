using NetXceptions.Models.Types;
using NetXceptions;
using System.Runtime.Serialization;

namespace NavigationModule.Journeys.Models.Exceptions.Journeys
{
    [Serializable]
    public class AchievementServiceException : NetXception, IFailedDependencyException
    {
        protected AchievementServiceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public AchievementServiceException(Exception innerException)
            : base(message: "Achievement service error occurred, contact support.", innerException) { }
    }
}
