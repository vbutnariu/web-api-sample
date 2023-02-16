using Demo.Common.Enums;
using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions
{
    [Serializable]
    public class PmApplicationException : Exception 
    {
        private ErrorCodeEnum errorCode;

        public ErrorCodeEnum ErrorCode { get => errorCode; set => errorCode = value; }

        public PmApplicationException() :base()
        {
            this.errorCode = ErrorCodeEnum.UnexpectedException;
        }

        public PmApplicationException(ErrorCodeEnum errorCode) : base()
        {
            this.errorCode = errorCode;
        }
        //
        // Summary:
        //     Initializes a new instance of the System.ApplicationException class with a specified
        //     error message.
        //
        // Parameters:
        //   message:
        //     A message that describes the error.
        public PmApplicationException(string message) : base(message)
        {
            this.errorCode = ErrorCodeEnum.UnexpectedException;
        }

        public PmApplicationException(string message, ErrorCodeEnum errorCode) : base(message)
        {
            this.errorCode =  errorCode;
        }

        public PmApplicationException(string message, ErrorCodeEnum errorCode, Exception innerException) : base(message, innerException)
        {
            this.errorCode = errorCode;
        }

        public PmApplicationException(string message, Exception innerException) : base(message, innerException)
        {
            this.errorCode =  ErrorCodeEnum.UnexpectedException;
        }

        protected PmApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.errorCode = ErrorCodeEnum.UnexpectedException;
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
