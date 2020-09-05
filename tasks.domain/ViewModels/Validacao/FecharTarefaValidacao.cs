using FluentValidation;

namespace tasks.domain.ViewModels.Validacao
{
    public class FecharTarefaValidacao : AbstractValidator<FecharTarefaRequestViewModel>
    {
        public FecharTarefaValidacao()
        {            
            RuleFor(a => a.Id)
                .NotEmpty().NotNull();

            RuleFor(a => a.Descricao)
                .NotEmpty().NotNull();

            RuleFor(a => a.Estimado)
                .NotEmpty().NotNull();
        }
    }
}