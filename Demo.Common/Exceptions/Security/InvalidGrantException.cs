using Demo.Common.Enums;
using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions
{
    [Serializable]
    public class InvalidGrantException : PmApplicationException
    {
        public InvalidGrantException(string message) : base(message, ErrorCodeEnum.InvalidGrant)
        {

        }

        public InvalidGrantException() : base()
        {

        }

        public InvalidGrantException(string message, Exception innerException) : base(message, Enums.ErrorCodeEnum.InvalidGrant, innerException)
        {
        }


        protected InvalidGrantException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
