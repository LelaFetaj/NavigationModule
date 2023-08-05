using NetXceptions.Models.Types;
using NetXceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Journeys.Models.Exceptions.Journeys
{
    [Serializable]
    public class NullAchievementException : NetXception, IValidationException
    {
        protected NullAchievementException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NullAchievementException()
            : base(message: "The achievement is empty.") { }

        public NullAchievementException(string message)
            : base(message) { }
    }
}
