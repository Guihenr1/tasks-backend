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
        public Guid UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; set; }

        public Tarefa(Guid id, string descricao, DateTime estimado, Guid usuarioId)
        {
            Id = id;
            Descricao = descricao;
            Estimado = estimado;
            Concluido = null;
            UsuarioId = usuarioId;
        }

        protected Tarefa() {  }

        public void Alternar(){
            if (Equals(TarefaStatus.Pendente, Status)){
                Status = TarefaStatus.Concluido;
                Concluido = DateTime.Now;
            } else {
                Status = TarefaStatus.Pendente;
                Concluido = null;
            }
        } 
        public void Criar() => Status = TarefaStatus.Pendente;
    }
}
