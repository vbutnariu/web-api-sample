using System;
using System.Collections.Generic;

namespace Demo.Common.Exceptions
{
    [Serializable]
    public class ImportException : PmApplicationException
    {
        public List<ImportExceptionInfo> ImportErrors { get; set; }

        public ImportException(List<ImportExceptionInfo> importErrors)
        {
            ImportErrors = importErrors;
        }
    }
}
