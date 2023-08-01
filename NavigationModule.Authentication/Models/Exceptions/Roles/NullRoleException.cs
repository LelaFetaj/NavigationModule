using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class NullRoleException : NetXception, IValidationException
    {
        protected NullRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NullRoleException()
            : base(message: "The role is empty.") { }

        public NullRoleException(string message)
            : base(message) { }
    }
}