using FluentValidation.Results;
using System.Collections.Generic;

namespace Demo.Common.Model.Validators
{
    public class PropertyValidationResult
    {
        public PropertyValidationResult()
        {
            this.Errors = new List<ValidationFailure>();
        }
       
        public bool IsValid { get; set; }
        
        public IList<ValidationFailure> Errors { get; set; }

    }
}
