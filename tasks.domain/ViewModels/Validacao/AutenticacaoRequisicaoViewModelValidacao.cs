using FluentValidation;

namespace tasks.domain.ViewModels.Validacao
{
    public class AutenticacaoRequisicaoViewModelValidacao : AbstractValidator<AutenticacaoRequisicaoViewModel>
    {
        public AutenticacaoRequisicaoViewModelValidacao()
        {
            RuleFor(a => a.Email)
                .NotEmpty().NotNull()
                .MaximumLength(100).EmailAddress();

            RuleFor(a => a.Senha)
                .NotEmpty().NotNull()
                .MaximumLength(20);
        }
    }
}