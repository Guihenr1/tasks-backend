using System;
using FluentValidation;

namespace tasks.domain.ViewModels.Validacao
{
    public class TarefaValidacao : AbstractValidator<TarefaRequestViewModel>
    {
        static string DescricaoErroMsg => "A descrição deve ter de 1 a 30 caracteres";
        static string EstimadoErroMsg => "A data estimada deve ter um valor maior que hoje";

        public TarefaValidacao()
        {
            RuleFor(a => a.Descricao)
                .NotEmpty().NotNull()
                .MinimumLength(1).MaximumLength(30).WithMessage(DescricaoErroMsg);

            RuleFor(a => a.Estimado)
                .NotEmpty().NotNull()
                .GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage(EstimadoErroMsg);
        }
    }
}