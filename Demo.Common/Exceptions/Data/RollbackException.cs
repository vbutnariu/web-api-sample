using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Demo.Common.Exceptions.Data
{
    public class RollbackException : PmApplicationException
    {
        public RollbackException(string message) : base(message)
        {

        }

        public RollbackException() : base()
        {

        }

        public RollbackException(string message, Exception innerException) : base(message, Enums.ErrorCodeEnum.InvalidGrant, innerException)
        {
        }


        protected RollbackException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
