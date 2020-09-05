using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tasks.domain.Entities;
using tasks.domain.Interfaces;

namespace tasks.infra.data.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly TarefaContext context;

        public TarefaRepository(TarefaContext context)
        {
            this.context = context;
        }

        public void Adicionar(Tarefa tarefa)
        {
            context.Tarefas.AddAsync(tarefa);
        }

        public void Fechar(Tarefa tarefa)
        {
            context.Tarefas.Update(tarefa);
        }

        public async Task<IEnumerable<Tarefa>> ObterTodos()
        {
            return await context.Tarefas.AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            context?.Dispose();
        }

        public IUnitOfWork UnitOfWork => context;
    }
}