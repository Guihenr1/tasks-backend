using tasks.domain.ViewModels.Validacao;

namespace tasks.domain.ViewModels
{
    public class AdicionarUsuarioViewModel : Validation
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarUsuarioViewModelValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}