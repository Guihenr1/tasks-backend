using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasks.domain.Entities;

namespace tasks.domain.Interfaces
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        IEnumerable<Tarefa> ObterTodos(Guid id, DateTime dataConclusao);
        void Adicionar(Tarefa tarefa);
        void Alternar(Tarefa tarefa);
        Task<Tarefa> ObterPorId(Guid id);
    }
}
