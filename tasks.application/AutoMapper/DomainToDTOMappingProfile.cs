using AutoMapper;
using tasks.domain.Entities;
using tasks.domain.ViewModels;

namespace tasks.application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile {
        public DomainToDTOMappingProfile () {
            CreateMap<Tarefa, TarefaResponseViewModel> ();
        }
    }
}