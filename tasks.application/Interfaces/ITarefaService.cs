using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasks.domain.ViewModels;
using tasks.domain.ViewModels.Validacao;

namespace tasks.application.Interfaces
{
    public interface ITarefaService : IDisposable
    {
        Task<bool> Adicionar(TarefaRequestViewModel tarefa);  
        Task<bool> Fechar(FecharTarefaRequestViewModel tarefa); 
        Task<IEnumerable<TarefaResponseViewModel>> ObterTodos();
    }
}