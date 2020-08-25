using System;
using tasks.domain.Entities.Validacao;
using tasks.domain.Enums;
using tasks.domain.Interfaces;

namespace tasks.domain.Entities
{
    public class Tarefa : Entity, IAggregateRoot
    {
        public string Descricao { get; private set; }
        public TarefaStatus Status { get; private set; }
        public DateTime Estimado { get; private set; }
        public DateTime? Concluido { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Tarefa(string descricao, DateTime estimado)
        {
            Descricao = descricao;
            Estimado = estimado;
        }

        protected Tarefa() {  }

        public void Concluir() => Status = TarefaStatus.Concluido;

        public override bool EhValido()
        {
            ValidationResult = new TarefaValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
