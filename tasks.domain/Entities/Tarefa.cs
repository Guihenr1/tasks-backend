using System;
using FluentValidation;
using tasks.domain.Enums;

namespace tasks.domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public TarefaStatus Status { get; set; }
        public DateTime Estimado { get; set; }
        public DateTime? Concluido { get; set; }

        public bool EhValido()
        {
            return new AdicionarTarefaValidation().Validate(this).IsValid;
        }
    }

    public class AdicionarTarefaValidation : AbstractValidator<Tarefa>
    {
        static string DescricaoErroMsg => "A descrição deve ter de 1 a 30 caracteres";
        static string EstimadoErroMsg => "A data estimada deve ter um valor maior que hoje";

        public AdicionarTarefaValidation()
        {
            RuleFor(a => a.Descricao)
                .NotEmpty().NotNull()
                .MinimumLength(1).MaximumLength(30)
                .WithMessage(DescricaoErroMsg);

            RuleFor(a => a.Estimado)
                .NotEmpty().NotNull()
                .GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage(EstimadoErroMsg);
        }
    }
}
