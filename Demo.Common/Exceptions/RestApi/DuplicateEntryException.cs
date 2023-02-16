using Demo.Common.Enums;
using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions.WEBApi
{
    [Serializable]
    public class DuplicateEntryException : PmApplicationException
    {
        public DuplicateEntryException(string message) : base(message, ErrorCodeEnum.DuplicateEntry)
        {
        }
        public DuplicateEntryException() : base(ErrorCodeEnum.DuplicateEntry)
        {
        }

        public DuplicateEntryException(string message, Exception innerException) : base(message, ErrorCodeEnum.DuplicateEntry, innerException)
        {
        }

        protected DuplicateEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
