using System;
using tasks.domain.Entities;
using tasks.domain.Enums;

namespace tasks.application.Services
{
    public class TarefaService
    {
        public DateTime Estimado { get; private set; }
        public DateTime? Concluido { get; private set; }
        public TarefaStatus Status { get; private set; }

        public void Adicionar(Tarefa tarefa){
            Estimado = tarefa.Estimado;
            Status = TarefaStatus.Pendente;
            Concluido = tarefa.Concluido;
        }
    }
}