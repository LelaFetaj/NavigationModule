using System.Runtime.Serialization;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class LockedUserException : NetXception, ILockedException
    {
        protected LockedUserException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public LockedUserException(Exception innerException)
            : base(message: "Locked user record exception, please try again later.", innerException)
        { }
    }
}