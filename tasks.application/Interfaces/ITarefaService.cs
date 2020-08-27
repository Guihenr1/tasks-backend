using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasks.application.ViewModels;

namespace tasks.application.Interfaces
{
    public interface ITarefaService : IDisposable
    {
        void Adicionar(TarefaViewModel tarefa);  
        void Fechar(TarefaViewModel tarefa); 
        Task<IEnumerable<TarefaViewModel>> ObterTodos();
    }
}