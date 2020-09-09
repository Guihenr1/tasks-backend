using System;
using tasks.domain.ViewModels;
using tasks.domain.ViewModels.Validacao;

namespace tasks.domain.ViewModels
{
    public class TarefaRequestViewModel : Validation
    {
        public string Descricao { get; set; }
        public DateTime Estimado { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new TarefaValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}