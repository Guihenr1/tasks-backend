using System;
using tasks.core.DomainObjects;
using tasks.domain.Entities;
using tasks.domain.Enums;

namespace tasks.application.Services
{
    public class TarefaService
    {
        public Guid Id { get; private set; }
        public DateTime Estimado { get; private set; }
        public DateTime? Concluido { get; private set; }
        public TarefaStatus Status { get; private set; }

        public void Adicionar(Tarefa tarefa){
            Id = Guid.NewGuid();
            Estimado = tarefa.Estimado;
            Status = TarefaStatus.Pendente;
        }

        public void Fechar(Tarefa tarefa){
            if (Id == Guid.Empty) throw new DomainException("Pedido não encontrado");

            Concluido = DateTime.Now;
            Status = TarefaStatus.Concluido;
        }
    }    
}
