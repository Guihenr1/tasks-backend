using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasks.application.Interfaces;
using tasks.domain.DomainException;
using tasks.domain.Entities;
using tasks.domain.Enums;
using tasks.domain.Interfaces;

namespace tasks.application.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository tarefaRepository;
        public TarefaService(ITarefaRepository tarefaRepository)
        {
            this.tarefaRepository = tarefaRepository;
        }

        public Guid Id { get; private set; }
        public DateTime Estimado { get; private set; }
        public DateTime? Concluido { get; private set; }
        public TarefaStatus Status { get; private set; }

        public void Adicionar(Tarefa tarefa)
        {
            Id = Guid.NewGuid();
            Estimado = tarefa.Estimado;
            Status = TarefaStatus.Pendente;
        }

        public void Atualizar(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }

        public void Fechar(Tarefa tarefa)
        {
            VerificarSeTarefaExiste();

            Concluido = DateTime.Now;
            Status = TarefaStatus.Concluido;
        }

        public Task<IEnumerable<Tarefa>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public void Remover(Tarefa tarefa)
        {
            VerificarSeTarefaExiste();
        }

        void VerificarSeTarefaExiste()
        {
            if (Id == Guid.Empty) throw new DomainException("Tarefa não encontrado");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
