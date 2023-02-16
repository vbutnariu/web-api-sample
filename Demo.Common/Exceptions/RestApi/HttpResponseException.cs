using Demo.Common.Transport;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Demo.Common.Core.Exceptions.WebApi
{
    [Serializable]
    public class HttpResponseException : Exception
    {
        public HttpResponseException()
        {

        }

        public HttpResponseException(HttpStatusCode status)
        {
            this.Status = status;
        }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

        public TransportErrorMessage ErrorInfo { get; set; }

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }


    }
}
