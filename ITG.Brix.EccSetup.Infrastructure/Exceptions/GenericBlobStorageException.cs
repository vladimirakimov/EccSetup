using System;
using System.Runtime.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.Exceptions
{
    [Serializable]
    public class GenericBlobStorageException : Exception
    {
        public GenericBlobStorageException(Exception ex) : base(ex.Message, ex) { }
        protected GenericBlobStorageException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
