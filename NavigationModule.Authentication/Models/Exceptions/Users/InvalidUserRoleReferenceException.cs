using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class InvalidUserRoleReferenceException : NetXception, IValidationException
    {
        protected InvalidUserRoleReferenceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidUserRoleReferenceException(Exception innerException)
            : base(message: "The user role reference is invalid.", innerException) { }
    }
}