using Demo.Common.Enums;
using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions.RestClient
{
    [Serializable]
    public class RestClientHttpException : PmApplicationException
    {



        public RestClientHttpException() : base()
        {

        }
       
        public RestClientHttpException(string message) : base(message)
        {
        }

        public RestClientHttpException(string message,ErrorCodeEnum errorCode, Exception innerException) : base(message, errorCode, innerException)
        {
        }

        protected RestClientHttpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
