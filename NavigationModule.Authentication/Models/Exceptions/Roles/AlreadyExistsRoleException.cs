using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class AlreadyExistsRoleException : NetXception, IUnprocessableException
    {
        protected AlreadyExistsRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public AlreadyExistsRoleException(string name)
            : base($"Role with name: '{name}' already exists.")
        { }

        public AlreadyExistsRoleException(Exception innerException)
            : base(message: "Role with the same details already exists.", innerException) { }
    }
}