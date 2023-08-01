using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class RoleServiceException : NetXception, IFailedDependencyException
    {
        protected RoleServiceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public RoleServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }

    }
}