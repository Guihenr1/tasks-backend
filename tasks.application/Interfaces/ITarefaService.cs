using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasks.domain.ViewModels;

namespace tasks.application.Interfaces
{
    public interface ITarefaService : IDisposable
    {
        Task<bool> Adicionar(TarefaRequestViewModel tarefa, Guid userId);  
        Task<bool> Alternar(Guid id); 
        IEnumerable<TarefaResponseViewModel> ObterTodos(Guid usuarioId, DateTime dataConclusao);
    }
}