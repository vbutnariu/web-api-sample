using Demo.Common.Enums;
using System;
using System.Runtime.Serialization;

namespace Demo.Common.Exceptions.WEBApi
{
    [Serializable]
    public class ItemCannotBeDeletedException : PmApplicationException
    {
        public ItemCannotBeDeletedException(string message) : base(message, ErrorCodeEnum.ItemCannotBeDeletedItemAlreadyInUse)
        {
        }
        public ItemCannotBeDeletedException() : base(ErrorCodeEnum.ItemCannotBeDeletedItemAlreadyInUse)
        {
        }

        public ItemCannotBeDeletedException(string message, Exception innerException) : base(message, ErrorCodeEnum.ItemCannotBeDeletedItemAlreadyInUse, innerException)
        {
        }

        protected ItemCannotBeDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
