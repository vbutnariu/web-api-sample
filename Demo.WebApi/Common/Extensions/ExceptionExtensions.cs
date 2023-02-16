using Pm.Common.Core.Exceptions.WebApi;
using Pm.Common.Exceptions;
using Pm.Common.Transport;
using System;
using System.Net;

namespace Pm.WebApi.Ergodat.Common.Extensions
{
    public static class ExceptionExtensions
    {


        public static HttpResponseException ToHttpResponseException(this Exception ex, HttpStatusCode status)
        {
            return new HttpResponseException(status)
            {
                ErrorInfo = BuildErrorMessage(ex, (int)status)
            };
        }

        public static HttpResponseException ToHttpResponseException(this Exception ex, string message, HttpStatusCode status)
        {
            return new HttpResponseException(status)
            {
                ErrorInfo = BuildErrorMessage(ex, (int)status, message)
            };
        }

        private static TransportErrorMessage BuildErrorMessage(Exception ex, int httpStatusCode, string? message = null)
        {
            var exception = ex as PmApplicationException;

            if (null == exception)
            {
                exception = new PmApplicationException(string.IsNullOrEmpty(message) ? ex.Message : message, ex);
            }

            return new TransportErrorMessage()
            {
                ErrorMessage = exception.Message,
                FullErrorMessage = exception.ToString(),
                ErrorCode = exception.ErrorCode,
                HttpStatusCode = httpStatusCode
            };
        }
    }

    public static class StringExtensions
    {
        public static string RemoveLineBreaks(this string content)
        {
            return content.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public static string ReplaceLineBreaks(this string content, string replacement)
        {
            return content.Replace("\r\n", replacement)
                        .Replace("\r", replacement)
                        .Replace("\n", replacement);
        }
    }
}