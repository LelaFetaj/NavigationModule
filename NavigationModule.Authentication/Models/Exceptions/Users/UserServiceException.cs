using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class UserServiceException : NetXception, IFailedDependencyException
    {
        protected UserServiceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public UserServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }
    }
}