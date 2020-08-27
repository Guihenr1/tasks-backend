using AutoMapper;

namespace tasks.application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {

            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToDTOMappingProfile>();
                cfg.AddProfile<DTOMappingProfileToDomain>();
            });
        }
    }
}