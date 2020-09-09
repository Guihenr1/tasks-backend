using System;
using tasks.domain.ViewModels.Validacao;

namespace tasks.domain.ViewModels
{
    public class FecharTarefaRequestViewModel : Validation
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Estimado { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new FecharTarefaValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}