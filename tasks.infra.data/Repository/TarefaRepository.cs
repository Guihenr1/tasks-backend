using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Alternar(Tarefa tarefa)
        {
            context.Tarefas.Update(tarefa);
        }

        public async Task<Tarefa> ObterPorId(Guid id)
        {
            return await context.Tarefas.FindAsync(id);
        }

        public IEnumerable<Tarefa> ObterTodos(Guid id, DateTime dataConclusao)
        {
            return context.Tarefas.Where
            (
                x => x.UsuarioId == id && 
                (x.Estimado.Date <= dataConclusao.Date && x.Estimado.Date >= DateTime.Now.Date)
            );
        }

        public void Dispose()
        {
            context?.Dispose();
        }

        public void Remover(Tarefa tarefa)
        {
            context.Tarefas.Remove(tarefa);
        }

        public IUnitOfWork UnitOfWork => context;
    }
}