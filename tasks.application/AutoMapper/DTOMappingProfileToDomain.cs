using System;
using AutoMapper;
using tasks.domain.Entities;
using tasks.domain.ViewModels;

namespace tasks.application.AutoMapper
{
    public class DTOMappingProfileToDomain : Profile {
        public DTOMappingProfileToDomain () 
        {
            CreateMap<FecharTarefaRequestViewModel, Tarefa>()
                .ConstructUsing(t => new Tarefa(t.Id, t.Descricao, t.Estimado, Guid.NewGuid()));
            CreateMap<AutenticacaoRequisicaoViewModel, Usuario>()
                .ConstructUsing(t => new Usuario(Guid.Empty, string.Empty, t.Email, t.Senha));
            CreateMap<AdicionarUsuarioViewModel, Usuario>()
                .ConstructUsing(t => new Usuario(Guid.NewGuid(), t.Nome, t.Email, t.Senha));
        }
    }
}