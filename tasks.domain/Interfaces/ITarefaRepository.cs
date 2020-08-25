using System.Collections.Generic;
using System.Threading.Tasks;
using tasks.domain.Entities;

namespace tasks.domain.Interfaces
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Task<IEnumerable<Tarefa>> ObterTodos();
        void Adicionar(Tarefa tarefa);
        void Atualizar(Tarefa tarefa);
    }
}
