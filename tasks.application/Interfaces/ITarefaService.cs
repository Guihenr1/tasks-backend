using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasks.domain.Entities;

namespace tasks.application.Interfaces
{
    public interface ITarefaService : IDisposable
    {
        void Adicionar(Tarefa tarefa);   
        void Atualizar(Tarefa tarefa);   
        Task<IEnumerable<Tarefa>> ObterTodos();
    }
}