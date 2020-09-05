using AutoMapper;
using tasks.domain.Entities;
using tasks.domain.ViewModels.Validacao;

namespace tasks.application.AutoMapper
{
    public class DTOMappingProfileToDomain : Profile {
        public DTOMappingProfileToDomain () 
        {
            CreateMap<FecharTarefaRequestViewModel, Tarefa>()
                .ConstructUsing(t => new Tarefa(t.Id, t.Descricao, t.Estimado));
        }
    }
}