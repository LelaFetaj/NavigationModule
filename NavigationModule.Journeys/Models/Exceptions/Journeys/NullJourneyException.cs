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
    public class NullJourneyException : NetXception, IValidationException
    {
        protected NullJourneyException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NullJourneyException()
            : base(message: "The journey is empty.") { }

        public NullJourneyException(string message)
            : base(message) { }
    }
}
