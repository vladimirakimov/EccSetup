﻿using System;
using System.Runtime.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.Exceptions
{
    [Serializable]
    public class FilterODataException : Exception
    {
        public FilterODataException() : base("FilterOData") { }
        public FilterODataException(Exception ex) : base(ex.Message, ex) { }
        internal FilterODataException(string message) : base(message) { }
        protected FilterODataException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
