using AutoMapper;
using tasks.application.ViewModels;
using tasks.domain.Entities;

namespace tasks.application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile {
        public DomainToDTOMappingProfile () {
            CreateMap<Tarefa, TarefaViewModel> ();
        }
    }
}