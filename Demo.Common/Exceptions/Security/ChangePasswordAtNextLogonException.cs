using Demo.Common.Enums;
using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions
{
    [Serializable]
    public class ChangePasswordAtNextLogonException : PmApplicationException
    {
        public ChangePasswordAtNextLogonException(string message) : base(message)
        {

        }

        public ChangePasswordAtNextLogonException() : base()
        {

        }

        public ChangePasswordAtNextLogonException(string message, Exception innerException) : base(message, ErrorCodeEnum.ChangePasswordAtNextLogon, innerException)
        {
        }

        protected ChangePasswordAtNextLogonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
