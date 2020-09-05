using System;
using FluentValidation.Results;

namespace tasks.domain.ViewModels
{
    public class Validation
    {
        public ValidationResult ValidationResult { get; protected set; }
        public Validation()
        {
            ValidationResult = new ValidationResult();
        }
        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}