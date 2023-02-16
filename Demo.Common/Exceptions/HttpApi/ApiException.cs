using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions.Api
{
    [Serializable]
    public class ApiException : PmApplicationException
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public ApiException(string message, int statusCode, string response, Exception innerException) : base(message + " Status: " + statusCode + " Response: " + response, innerException)
        {
            StatusCode = statusCode;
            Response = response;
        }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            return string.Format("HTTP Response:{0} {1}", Response, base.ToString());
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }


}
