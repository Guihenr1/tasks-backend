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
            VerificarSeTarefaExiste();

            Concluido = DateTime.Now;
            Status = TarefaStatus.Concluido;
        }

        public void Remover(Tarefa tarefa){
            VerificarSeTarefaExiste();
        }

        void VerificarSeTarefaExiste(){
            if (Id == Guid.Empty) throw new DomainException("Tarefa não encontrado");
        }
    }    
}
