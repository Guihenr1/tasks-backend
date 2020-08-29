using Microsoft.Extensions.DependencyInjection;
using tasks.application.Interfaces;
using tasks.application.Services;
using tasks.domain.Interfaces;
using tasks.infra.data.Repository;
using tasks.application.AutoMapper;
using AutoMapper;
using tasks.infra.data;

namespace tasks.infra.crossCutting
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddSetupIoC (this IServiceCollection services) {
            services.ConfigureAutoMapper ();
            services.RegisterServices ();
            services.AddRepositories ();
            return services;
        }
        
        private static void ConfigureAutoMapper (this IServiceCollection services) {
            var mapperConfig = AutoMapperConfig.RegisterMappings ();
            services.AddAutoMapper (typeof (DomainToDTOMappingProfile), typeof (DTOMappingProfileToDomain));
        }

        private static void RegisterServices (this IServiceCollection services) {
            services.AddScoped<ITarefaService, TarefaService> ();
        }

        private static void AddRepositories (this IServiceCollection services) {
            services.AddScoped<ITarefaRepository, TarefaRepository> ();
            services.AddScoped<TarefaContext>(c => c.GetService<TarefaContext>());
        }
    }
}
