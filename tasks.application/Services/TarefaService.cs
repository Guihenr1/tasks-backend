using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using tasks.application.Interfaces;
using tasks.domain.Entities;
using tasks.domain.Interfaces;
using tasks.domain.ViewModels;
using tasks.domain.ViewModels.Validacao;

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

        public async Task<IEnumerable<TarefaResponseViewModel>> ObterTodos()
        {
            var result = await tarefaRepository.ObterTodos();
            return mapper.ProjectTo<TarefaResponseViewModel>(result.AsQueryable());
        }

        public async Task<bool> Adicionar(TarefaRequestViewModel tarefa, Guid userId)
        {
            if(!tarefa.EhValido())
                return false;

            var tarefaAdicionar = new Tarefa(
                Guid.NewGuid(), 
                tarefa.Descricao, 
                tarefa.Estimado, 
                userId
            );
            
            tarefaAdicionar.Criar();

            tarefaRepository.Adicionar(tarefaAdicionar);
            return await tarefaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Fechar(FecharTarefaRequestViewModel tarefa)
        {
            if(!tarefa.EhValido())
                return false;
                
            var tarefaConcluida = mapper.Map<Tarefa>(tarefa);
            tarefaConcluida.Fechar();

            tarefaRepository.Fechar(tarefaConcluida);
            return await tarefaRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
