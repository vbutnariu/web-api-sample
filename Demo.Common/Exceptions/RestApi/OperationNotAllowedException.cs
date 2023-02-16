using Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Exceptions.WEBApi
{
    [Serializable]
    public class OperationNotAllowedException : PmApplicationException
    {
        public OperationNotAllowedException(string message) : base(message, ErrorCodeEnum.OperationNotAllowed)
        {
        }

        public OperationNotAllowedException() : base(ErrorCodeEnum.OperationNotAllowed)
        {
        }

        public OperationNotAllowedException(string message, Exception innerException) : base(message, ErrorCodeEnum.OperationNotAllowed, innerException)
        {
        }

        protected OperationNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }
}
