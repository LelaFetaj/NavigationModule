using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using NetXceptions;
using NetXceptions.Models.Types;

namespace NavigationModule.Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class LockedRoleException : NetXception, ILockedException
    {
        protected LockedRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public LockedRoleException(Exception innerException)
            : base(message: "Locked role record exception, please try again later.", innerException)
        { }
    }
}