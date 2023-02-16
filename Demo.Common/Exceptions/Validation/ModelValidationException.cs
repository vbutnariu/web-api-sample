using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions.Validation
{
    [Serializable]
    public class ModelValidationException : PmApplicationException
    {
        public ModelValidationException(string message) : base(message, Enums.ErrorCodeEnum.InvalidModel)
        {

        }

        public ModelValidationException() : base(Enums.ErrorCodeEnum.InvalidModel)
        {

        }

        public ModelValidationException(string message, Exception innerException) : base(message, Enums.ErrorCodeEnum.InvalidModel, innerException)
        {
        }

        protected ModelValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
