using System;
using System.Runtime.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.Exceptions
{
    [Serializable]
    public class UniqueFileNameException : Exception
    {
        public UniqueFileNameException(Exception ex) : base(ex.Message, ex) { }
        protected UniqueFileNameException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
