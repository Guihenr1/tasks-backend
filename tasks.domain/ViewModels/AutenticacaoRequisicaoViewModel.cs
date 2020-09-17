using tasks.domain.ViewModels.Validacao;

namespace tasks.domain.ViewModels
{
    public class AutenticacaoRequisicaoViewModel : Validation
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AutenticacaoRequisicaoViewModelValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}