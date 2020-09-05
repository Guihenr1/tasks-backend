using System;
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

        public Tarefa(Guid id, string descricao, DateTime estimado)
        {
            Id = id;
            Descricao = descricao;
            Estimado = estimado;
            Concluido = null;
        }

        protected Tarefa() {  }

        public void Fechar(){
            Status = TarefaStatus.Concluido;
            Concluido = DateTime.Now;
        } 
        public void Criar() => Status = TarefaStatus.Pendente;
    }
}
