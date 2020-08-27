using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tasks.application.Interfaces;
using tasks.application.Services;
using tasks.domain.Interfaces;
using tasks.infra.data.Repository;

namespace tasks.infra.crossCutting
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddSetupIoC (this IServiceCollection services, IConfiguration config) {
            services.RegisterServices ();
            services.AddRepositories ();
            return services;
        }

        private static void RegisterServices (this IServiceCollection services) {
            services.AddScoped<ITarefaService, TarefaService> ();
        }

        private static void AddRepositories (this IServiceCollection services) {
            services.AddScoped<ITarefaRepository, TarefaRepository> ();
        }
    }
}
