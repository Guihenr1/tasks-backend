using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using tasks.application.Interfaces;
using tasks.application.ViewModels;
using tasks.domain.Entities;
using tasks.domain.Interfaces;

namespace tasks.application.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly IMapper mapper;
        private readonly ITarefaRepository tarefaRepository;
        public TarefaService(
            ITarefaRepository tarefaRepository
            , IMapper mapper)
        {
            this.mapper = mapper;
            this.tarefaRepository = tarefaRepository;
        }

        public Guid Id { get; private set; }
        public DateTime Estimado { get; private set; }
        public DateTime? Concluido { get; private set; }

        public async Task<IEnumerable<TarefaViewModel>> ObterTodos()
        {
            var result = await tarefaRepository.ObterTodos();
            return mapper.ProjectTo<TarefaViewModel>(result.AsQueryable());
        }

        public async Task<bool> Adicionar(TarefaViewModel tarefa)
        {
            Id = Guid.NewGuid();
            Estimado = tarefa.Estimado;

            var tarefaAdicionar = new Tarefa(
                Id, 
                tarefa.Descricao, 
                Estimado,
                null
            );
            
            tarefaAdicionar.Criar();

            tarefaRepository.Adicionar(tarefaAdicionar);
            return await tarefaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Fechar(TarefaViewModel tarefa)
        {
            var tarefaFechar = new Tarefa(
                Id,
                tarefa.Descricao,
                tarefa.Estimado,
                Concluido = DateTime.Now                
            );

            tarefaFechar.Fechar();

            tarefaRepository.Atualizar(tarefaFechar);
            return await tarefaRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
