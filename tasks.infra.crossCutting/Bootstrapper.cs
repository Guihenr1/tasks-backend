using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            
        }

        private static void AddRepositories (this IServiceCollection services) {
            
        }
    }
}
