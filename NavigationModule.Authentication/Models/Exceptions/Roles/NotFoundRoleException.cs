using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Roles
{
    public class NotFoundRoleException : NetXception, INotFoundException
    {
        protected NotFoundRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NotFoundRoleException(Guid roleId)
            : base(message: $"Role with ID: {roleId} does not exist")
        { }

        public NotFoundRoleException(string roleName)
            : base(message: $"Role with name: {roleName} does not exist")
        { }
    }
}