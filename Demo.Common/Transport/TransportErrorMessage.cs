using Demo.Common.Enums;

namespace Demo.Common.Transport
{
    public class TransportErrorMessage
    {

        public TransportErrorMessage()
        {
            this.ErrorMessage = string.Empty;
        }
        public ErrorCodeEnum ErrorCode { get; set; }
        public string  ErrorMessage { get; set; }
        public string FullErrorMessage { get; set; }
        public int HttpStatusCode { get; set; }
    }
}
