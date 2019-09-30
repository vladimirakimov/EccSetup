using System;

namespace ITG.Brix.EccSetup.Infrastructure.Exceptions
{
    [Serializable]
    public class BlobNotFoundException : Exception
    {
        public BlobNotFoundException(Exception ex) : base(ex.Message, ex) { }
    }
}
