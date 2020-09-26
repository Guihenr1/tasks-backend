using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using tasks.application.Interfaces;
using tasks.domain.Entities;
using tasks.domain.Interfaces;
using tasks.domain.ViewModels;

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

        public IEnumerable<TarefaResponseViewModel> ObterTodos(Guid id, DateTime dataConclusao)
        {
            var result = tarefaRepository.ObterTodos(id, dataConclusao);
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

        public async Task<bool> Alternar(Guid id)
        {
            var tarefa = await ObterPorId(id);
            if (tarefa == null) throw new Exception("Tarefa não encontrada");

            tarefa.Alternar();

            tarefaRepository.Alternar(tarefa);
            return await tarefaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Remover(Guid id, Guid usuarioId)
        {
            var tarefa = await ObterPorId(id);
            if (tarefa == null || !tarefa.UsuarioId.Equals(usuarioId)) 
                throw new Exception("Tarefa não encontrada");

            tarefaRepository.Remover(tarefa);
            return await tarefaRepository.UnitOfWork.Commit();
        }

        private async Task<Tarefa> ObterPorId(Guid id) => 
            await tarefaRepository.ObterPorId(id);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
