using Demo.Common.Enums;
using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions.WEBApi
{
    [Serializable]
    public class NetworkStateException : PmApplicationException
    {
        public NetworkStateException(string message) : base(message, ErrorCodeEnum.NetworkConnectionUnavailable)
        {
        }

        public NetworkStateException() : base(ErrorCodeEnum.NetworkConnectionUnavailable)
        {
        }

        public NetworkStateException(string message, Exception innerException) : base(message, ErrorCodeEnum.NetworkConnectionUnavailable, innerException)
        {
        }

        protected NetworkStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
