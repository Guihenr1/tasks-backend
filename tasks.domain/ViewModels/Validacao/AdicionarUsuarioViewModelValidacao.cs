using FluentValidation;

namespace tasks.domain.ViewModels.Validacao
{
    public class AdicionarUsuarioViewModelValidacao : AbstractValidator<AdicionarUsuarioViewModel>
    {
        public AdicionarUsuarioViewModelValidacao()
        {
            RuleFor(a => a.Email)
                .NotEmpty().NotNull()
                .MaximumLength(100).EmailAddress();
            
            RuleFor(a => a.Nome)
                .NotEmpty().NotNull()
                .MaximumLength(50);
            
            RuleFor(a => a.Senha)
                .NotEmpty().NotNull()
                .MaximumLength(20);
        }
    }
}
