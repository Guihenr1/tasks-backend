using System.Collections.Generic;
using FluentValidation.Results;

namespace tasks.domain.Entities
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public IList<ValidationFailure> Validation { get; set; }
    }
}